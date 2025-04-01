using BusinessObject.Contracts;
using BusinessObject.Models.Member;
using DataAccess.Entities;
using DataAccess;
using DataAccess.Interfaces;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BusinessObject.Services;

public class MemberService : IMemberService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly ILogger<MemberService> _logger;

	public MemberService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MemberService> logger)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<List<MemberViewDTO>> GetMembersAsync()
	{
		var members = await _unitOfWork.MemberRepository.GetAllAsync();
		return _mapper.Map<List<MemberViewDTO>>(members);
	}

	public async Task<Member?> GetMemberByIdAsync(int id)
	{
		return await _unitOfWork.MemberRepository.GetByIdAsync(id);
	}

	public async Task AddMemberAsync(CreateMemberDTO memberDto)
	{
		var member = _mapper.Map<Member>(memberDto);
		await _unitOfWork.MemberRepository.InsertAsync(member);
		await _unitOfWork.Complete();
	}

	public async Task UpdateMemberAsync(UpdateMemberDTO memberDto)
	{
		var member = await GetMemberByIdAsync(memberDto.MemberId)
			?? throw new Exception($"Thành viên với ID {memberDto.MemberId} không tồn tại.");

		if (member.Orders != null && member.Orders.Any())
		{
			throw new InvalidOperationException($"Không thể cập nhật thành viên với ID {memberDto.MemberId} vì đã có đơn hàng liên quan.");
		}

		_mapper.Map(memberDto, member);
		await _unitOfWork.MemberRepository.UpdateAsync(member);
		await _unitOfWork.Complete();
	}

	public async Task DeleteMemberAsync(int id)
	{
		var member = await GetMemberByIdAsync(id)
			?? throw new Exception($"Thành viên với ID {id} không tồn tại.");

		if (member.Orders != null && member.Orders.Any())
		{
			throw new InvalidOperationException($"Không thể xóa thành viên với ID {id} vì đã có đơn hàng liên quan.");
		}

		await _unitOfWork.MemberRepository.DeleteAsync(id);
		await _unitOfWork.Complete();
	}

	public async Task<PaginatedList<MemberViewDTO>> GetPagedMembersAsync(int pageIndex, int pageSize)
	{
		var query = _unitOfWork.MemberRepository.Entities;
		var paginatedMembers = await _unitOfWork.MemberRepository.GetPagging(query, pageIndex, pageSize);
		return new PaginatedList<MemberViewDTO>(
			_mapper.Map<List<MemberViewDTO>>(paginatedMembers.Items),
			paginatedMembers.TotalItems,
			pageIndex,
			pageSize
		);
	}

	public async Task<PaginatedList<MemberViewDTO>> SearchMembersAsync(string searchTerm, int index, int pageSize)
	{
		var query = _unitOfWork.MemberRepository.Entities;
		Expression<Func<Member, bool>> filter = m =>
			string.IsNullOrWhiteSpace(searchTerm) ||
			m.Email.Contains(searchTerm) ||
			m.CompanyName.Contains(searchTerm) ||
			m.City.Contains(searchTerm) ||
			m.Country.Contains(searchTerm);

		var paginatedMembers = await _unitOfWork.MemberRepository.SearchAsync(query, index, pageSize, filter);
		return new PaginatedList<MemberViewDTO>(
			_mapper.Map<List<MemberViewDTO>>(paginatedMembers.Items),
			paginatedMembers.TotalItems,
			index,
			pageSize
		);
	}
}
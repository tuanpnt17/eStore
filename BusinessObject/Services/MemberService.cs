using System.Linq.Expressions;
using AutoMapper;
using BusinessObject.Contracts;
using BusinessObject.Models;
using BusinessObject.Models.Member;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BusinessObject.Services;

public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<MemberService> _logger;
    private readonly IConfiguration _configuration;

    public MemberService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<MemberService> logger,
        IConfiguration configuration
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
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
        if (_unitOfWork.MemberRepository.CheckExistingEmail(memberDto.Email))
        {
            throw new Exception("Email đã tồn tại");
        }
        var member = _mapper.Map<Member>(memberDto);
        await _unitOfWork.MemberRepository.InsertAsync(member);
        await _unitOfWork.Complete();
    }

    public async Task UpdateMemberAsync(UpdateMemberDTO memberDto)
    {
        var member =
            await GetMemberByIdAsync(memberDto.MemberId)
            ?? throw new Exception($"Thành viên với ID {memberDto.MemberId} không tồn tại.");

        _mapper.Map(memberDto, member);
        await _unitOfWork.MemberRepository.UpdateAsync(member);
        await _unitOfWork.Complete();
    }

    public async Task DeleteMemberAsync(int id)
    {
        var member =
            await GetMemberByIdAsync(id)
            ?? throw new Exception($"Thành viên với ID {id} không tồn tại.");

        if (member.Orders != null && member.Orders.Any())
        {
            throw new InvalidOperationException(
                $"Không thể xóa thành viên với ID {id} vì đã có đơn hàng liên quan."
            );
        }

        await _unitOfWork.MemberRepository.DeleteAsync(id);
        await _unitOfWork.Complete();
    }

    public async Task<PaginatedList<MemberViewDTO>> GetPagedMembersAsync(
        int pageIndex,
        int pageSize
    )
    {
        var query = _unitOfWork.MemberRepository.Entities;
        var paginatedMembers = await _unitOfWork.MemberRepository.GetPagging(
            query,
            pageIndex,
            pageSize
        );
        return new PaginatedList<MemberViewDTO>(
            _mapper.Map<List<MemberViewDTO>>(paginatedMembers.Items),
            paginatedMembers.TotalItems,
            pageIndex,
            pageSize
        );
    }

    public async Task<PaginatedList<MemberViewDTO>> SearchMembersAsync(
        string searchTerm,
        int index,
        int pageSize
    )
    {
        var query = _unitOfWork.MemberRepository.Entities;
        Expression<Func<Member, bool>> filter = m =>
            string.IsNullOrWhiteSpace(searchTerm)
            || m.Email.Contains(searchTerm)
            || m.CompanyName.Contains(searchTerm)
            || m.City.Contains(searchTerm)
            || m.Country.Contains(searchTerm);

        var paginatedMembers = await _unitOfWork.MemberRepository.SearchAsync(
            query,
            index,
            pageSize,
            filter
        );
        return new PaginatedList<MemberViewDTO>(
            _mapper.Map<List<MemberViewDTO>>(paginatedMembers.Items),
            paginatedMembers.TotalItems,
            index,
            pageSize
        );
    }

    public async Task<MemberDTO> GetMember(int id)
    {
        var member = await _unitOfWork.MemberRepository.GetMemberById(id);
        if (member == null)
        {
            return null;
        }

        return new MemberDTO
        {
            MemberId = member.MemberId,
            Email = member.Email,
            Password = member.Password,
            City = member.City,
            Country = member.Country,
            CompanyName = member.CompanyName,
        };
    }

    public async Task<MemberDTO> Login(string email, string password)
    {
        var adminEmail = _configuration["AdminAccount:Email"];
        var adminPassword = _configuration["AdminAccount:Password"];
        if (email == adminEmail && password == adminPassword)
        {
            return new MemberDTO
            {
                MemberId = 0,
                Email = adminEmail,
                Password = adminPassword,
                Role = "Admin",
            };
        }
        var member = await _unitOfWork.MemberRepository.Login(email, password);
        if (member == null)
        {
            return null;
        }
        return new MemberDTO
        {
            Email = member.Email,
            Password = member.Password,
            City = member.City,
            Country = member.Country,
            CompanyName = member.CompanyName,
            Role = "Member",
            MemberId = member.MemberId,
        };
    }

    public async Task Register(MemberDTO memberDTO)
    {
        if (_unitOfWork.MemberRepository.CheckExistingEmail(memberDTO.Email))
        {
            throw new Exception("Email already exists");
        }
        Member member = new Member
        {
            Email = memberDTO.Email,
            Password = memberDTO.Password,
            City = memberDTO.City,
            Country = memberDTO.Country,
            CompanyName = memberDTO.CompanyName,
        };
        _unitOfWork.MemberRepository.Register(member);
        await _unitOfWork.Complete();
    }

    public async Task UpdateProfile(MemberDTO memberDTO)
    {
        var existingMember = await _unitOfWork.MemberRepository.GetMemberById(memberDTO.MemberId);

        //existingMember.Email = memberDTO.Email;
        existingMember.Password = memberDTO.Password;
        existingMember.City = memberDTO.City;
        existingMember.Country = memberDTO.Country;
        existingMember.CompanyName = memberDTO.CompanyName;

        _unitOfWork.MemberRepository.UpdateProfile(existingMember);
        await _unitOfWork.Complete();
    }
}

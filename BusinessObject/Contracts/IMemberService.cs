using BusinessObject.Models.Member;
using DataAccess;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace BusinessObject.Contracts;

public interface IMemberService
{
	Task<List<MemberViewDTO>> GetMembersAsync();
	Task<Member?> GetMemberByIdAsync(int id);
	Task AddMemberAsync(CreateMemberDTO memberDto);
	Task UpdateMemberAsync(UpdateMemberDTO memberDto);
	Task DeleteMemberAsync(int id);
	Task<PaginatedList<MemberViewDTO>> GetPagedMembersAsync(int pageIndex, int pageSize);
	Task<PaginatedList<MemberViewDTO>> SearchMembersAsync(string searchTerm, int index, int pageSize);
}
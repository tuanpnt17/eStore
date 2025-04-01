using BusinessObject.Models;
using BusinessObject.Models.Member;
using DataAccess;
using DataAccess.Entities;

namespace BusinessObject.Contracts;

public interface IMemberService
{
    public Task<MemberDTO> Login(string email, string password);
    public Task<MemberDTO> GetMember(int id);
    public Task UpdateProfile(MemberDTO memberDTO);
    public Task Register(MemberDTO memberDTO);
    Task<List<MemberViewDTO>> GetMembersAsync();
    Task<Member?> GetMemberByIdAsync(int id);
    Task AddMemberAsync(CreateMemberDTO memberDto);
    Task UpdateMemberAsync(UpdateMemberDTO memberDto);
    Task DeleteMemberAsync(int id);
    Task<PaginatedList<MemberViewDTO>> GetPagedMembersAsync(int pageIndex, int pageSize);
    Task<PaginatedList<MemberViewDTO>> SearchMembersAsync(
        string searchTerm,
        int index,
        int pageSize
    );
}

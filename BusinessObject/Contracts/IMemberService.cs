using BusinessObject.Models;

namespace BusinessObject.Contracts;

public interface IMemberService 
{
    public Task<MemberDTO> Login(string email, string password);
    public Task<MemberDTO> GetMember(int id);
    public Task UpdateProfile(MemberDTO memberDTO);
    public Task Register(MemberDTO memberDTO);
}

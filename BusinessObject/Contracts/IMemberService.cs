using BusinessObject.Models;

namespace BusinessObject.Contracts;

public interface IMemberService 
{
    public Task<MemberDTO> Login(string email, string password);
}

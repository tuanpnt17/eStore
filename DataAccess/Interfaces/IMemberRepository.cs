using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IMemberRepository
{
    public Task<Member> Login(string username, string password);
    public void Register(Member member);
    public void UpdateProfile(Member member);

    public Task<Member> GetMemberById(int memberId);
}

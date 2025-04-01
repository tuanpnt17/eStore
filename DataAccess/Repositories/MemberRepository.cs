using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public bool CheckExistingEmail(string email)
    {
        return context.Members.Any(x => x.Email == email);
    }

    public async Task<Member?> GetMemberById(int memberId)
    {
        return await context.Members.FindAsync(memberId);
        ;
    }

    public async Task<Member> Login(string username, string password)
    {
        return await context.Members.FirstOrDefaultAsync(x => x.Password == password && x.Email == username);
    }

    public void Register(Member member)
    {
        context.Members.Add(member);
    }

    public void UpdateProfile(Member member)
    {
        context.Members.Update(member);
    }
}

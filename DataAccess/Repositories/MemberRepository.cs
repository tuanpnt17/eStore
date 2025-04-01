using System.Linq.Expressions;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class MemberRepository : IMemberRepository
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<Member> _dbSet;

    public MemberRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = _context.Set<Member>();
    }

    public IQueryable<Member> Entities => _context.Set<Member>();

    public async Task<IList<Member>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int memberId)
    {
        return await _dbSet.Include(m => m.Orders).FirstOrDefaultAsync(m => m.MemberId == memberId);
    }

    public async Task<PaginatedList<Member>> GetPagging(
        IQueryable<Member> query,
        int index,
        int pageSize
    )
    {
        query = query.AsNoTracking();
        int count = await query.CountAsync();
        IReadOnlyCollection<Member> items = await query
            .Skip((index - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedList<Member>(items, count, index, pageSize);
    }

    public async Task InsertAsync(Member member)
    {
        await _dbSet.AddAsync(member);
    }

    public Task UpdateAsync(Member member)
    {
        return Task.FromResult(_dbSet.Update(member));
    }

    public async Task DeleteAsync(int memberId)
    {
        var member =
            await _dbSet.FindAsync(memberId) ?? throw new Exception("Thành viên không tồn tại.");
        _dbSet.Remove(member);
    }

    public async Task<PaginatedList<Member>> SearchAsync(
        IQueryable<Member> query,
        int index,
        int pageSize,
        Expression<Func<Member, bool>> filter
    )
    {
        query = query.AsNoTracking();
        int count = query.Where(filter).Count();
        IReadOnlyCollection<Member> items = await query
            .Where(filter)
            .Skip((index - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedList<Member>(items, count, index, pageSize);
    }

    public bool CheckExistingEmail(string email)
    {
        return _context.Members.Any(x => x.Email == email);
    }

    public async Task<Member?> GetMemberById(int memberId)
    {
        return await _context.Members.FindAsync(memberId);
        ;
    }

    public async Task<Member> Login(string username, string password)
    {
        return await _context.Members.FirstOrDefaultAsync(x =>
            x.Password == password && x.Email == username
        );
    }

    public void Register(Member member)
    {
        _context.Members.Add(member);
    }

    public void UpdateProfile(Member member)
    {
        _context.Members.Update(member);
    }
}

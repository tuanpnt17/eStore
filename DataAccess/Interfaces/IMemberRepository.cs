using DataAccess.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Interfaces;

public interface IMemberRepository
{
	IQueryable<Member> Entities { get; }
	Task<IList<Member>> GetAllAsync();
	Task<Member?> GetByIdAsync(int memberId);
	Task<PaginatedList<Member>> GetPagging(IQueryable<Member> query, int index, int pageSize);
	Task InsertAsync(Member member);
	Task UpdateAsync(Member member);
	Task DeleteAsync(int memberId);
	Task<PaginatedList<Member>> SearchAsync(IQueryable<Member> query, int index, int pageSize, Expression<Func<Member, bool>> filter);
}
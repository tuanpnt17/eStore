using DataAccess.Data;

namespace DataAccess.Repositories;

public class MemberRepository(AppDbContext context) : IMemberRepository { }

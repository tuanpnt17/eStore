using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Data;

public class UnitOfWork(
    AppDbContext context,
    IMemberRepository memberRepository,
    ICategoryRepository categoryRepository,
    IOrderDetailRepository orderDetailRepository,
    IOrderRepository orderRepository,
    IProductRepository productRepository
) : IUnitOfWork
{
    public IProductRepository ProductRepository { get; } = productRepository;
    public IOrderDetailRepository OrderDetailRepository { get; } = orderDetailRepository;
    public ICategoryRepository CategoryRepository { get; } = categoryRepository;
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IMemberRepository MemberRepository { get; } = memberRepository;

    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            Dispose();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            Dispose();
        }
    }
}

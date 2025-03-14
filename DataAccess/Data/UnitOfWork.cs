using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace DataAccess.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private Hashtable? _repositories;

    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public IGenericRepository<T> Repository<T>()
        where T : class
    {
        _repositories ??= new Hashtable();

        var entityType = typeof(T).Name;
        if (_repositories.ContainsKey(entityType))
            return (IGenericRepository<T>)_repositories[entityType]!;
        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(T)),
            context
        );
        _repositories.Add(entityType, repositoryInstance);

        return (IGenericRepository<T>)_repositories[entityType]!;
    }

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
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}

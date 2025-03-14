﻿namespace DataAccess.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T>? Repository<T>()
        where T : class;

    Task<int> Complete();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}

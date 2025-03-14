﻿using DataAccess.Specifications;

namespace DataAccess.Interfaces;

public interface IGenericRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

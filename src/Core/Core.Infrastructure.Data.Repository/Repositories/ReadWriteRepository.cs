﻿using Core.Domain.Interfaces;
using Core.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.Data.Repository.Repositories
{
    public class ReadWriteRepository<T> : IReadWriteRepository<T> where T : class
    {
        private readonly BaseContext _contexto;

        public ReadWriteRepository(BaseContext context)
        {
            _contexto = context;
        }

        public virtual Task<T> FindAsync(CancellationToken cancellationToken, params object[] keys)
        {
            var entity = _contexto.Set<T>().FindAsync(keys, cancellationToken);
            return entity;
        }

        public virtual Task<List<T>> SearchAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = specification.Prepare(_contexto.Set<T>().AsQueryable())
                                      .ToListAsync(cancellationToken);
            return result;
        }

        public virtual Task<List<T>> SearchAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = specification.Prepare(_contexto.Set<T>().AsQueryable())
                                     .Skip(pageNumber)
                                     .Take(pageSize);

            var entities = query.ToListAsync(cancellationToken);
            return entities;
        }

        public virtual IQueryable<T> Where(ISpecification<T> specification)
        {
            var query = _contexto.Set<T>().Where(specification.Predicate);
            return query;
        }

        public virtual Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                                  .CountAsync(specification.Predicate, cancellationToken);
            return result;
        }

        public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                                  .AnyAsync(specification.Predicate, cancellationToken);
            return result;
        }

        public virtual Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                                  .FirstOrDefaultAsync(specification.Predicate, cancellationToken);
            return result;
        }

        public virtual IQueryable<T> AsQuerable()
        {
            return _contexto.Set<T>()
                            .AsQueryable();
        }

        public virtual T Add(T entity)
        {
            var result = _contexto.Set<T>().Add(entity);
            return result.Entity;
        }

        public virtual Task AddRange(IList<T> entity, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>().AddRangeAsync(entity, cancellationToken);
            return result;
        }

        public virtual void Remove(T entity)
        {
            _contexto.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
        }

        public void Attach(T entity)
        {
            if (_contexto.Entry(entity).State == EntityState.Detached)
            {
                _contexto.Set<T>().Attach(entity);
            }
        }

        public virtual Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _contexto.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> AllAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<T>()
                                  .AllAsync(specification.Predicate, cancellationToken);
            return result;
        }

        public virtual string GetProviderName()
        {
            return _contexto.Database.ProviderName;
        }

        public virtual async Task<T> LastOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await _contexto.Set<T>()
                                  .LastOrDefaultAsync(specification.Predicate, cancellationToken).ConfigureAwait(false);
        }
    }
}
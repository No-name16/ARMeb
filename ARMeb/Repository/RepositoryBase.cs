using ARMeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ARMeb.Models;


namespace ARMeb.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ARMebContext RepositoryContext;
        public RepositoryBase(ARMebContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

        public void Update(T entity) {
            RepositoryContext.Set<T>().Add(entity);
            RepositoryContext.Set<T>().Attach(entity);
            RepositoryContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            RepositoryContext.Set<T>()
            .AsNoTracking() :
            RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            RepositoryContext.Set<T>().
            Where(expression)
            .AsNoTracking() :
            RepositoryContext.Set<T>()
            .Where(expression);




        //public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    }
}

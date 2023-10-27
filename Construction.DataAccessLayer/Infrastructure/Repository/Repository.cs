using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Construction.DataAccessLayer.Data;
using Construction.DataAccessLayer.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Construction.DataAccessLayer.Infrastructure.Repository
{
	

	public class Repository<T> : IRepository<T> where T : class
	{

		private ApplicationDbContext _context;
		private DbSet<T> _dbset;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbset = _context.Set<T>();
		}


		public void Add(T entity)
		{
			_dbset.Add(entity);
		}

		public void Delete(T entity)
		{
			_dbset.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			_dbset.RemoveRange(entities);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = _dbset;
			if(includeProperties != null)
			{
				foreach(var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}

			return query.ToList();
		}

		public T GetById(Expression<Func<T, bool>> predicate, string? includeProperties = null)
		{

			IQueryable<T> query = _dbset;
			query = query.Where(predicate);
			if(includeProperties != null)
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}

			return query.FirstOrDefault();
		}
	}
}

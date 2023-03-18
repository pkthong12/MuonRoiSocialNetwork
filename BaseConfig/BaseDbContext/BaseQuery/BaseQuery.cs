using BaseConfig.BaseDbContext.Common;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Infrashtructure;
using Microsoft.EntityFrameworkCore;

namespace BaseConfig.BaseDbContext.BaseQuery
{
    public class BaseQuery<T> : IQueries<T> where T : Entity
    {
        protected readonly BaseDbContext _dbBaseContext;

        private readonly AuthContext _authContext;

        protected readonly DbSet<T> _dbSet;

        public string CurrentUserId => _authContext.CurrentUserId;

        public string CurrentUsername => _authContext.CurrentUsername;

        public IQueryable<T> _queryable => from m in _dbSet.AsNoTracking()
                                           where !m.IsDeleted
                                           select m;

        public BaseQuery(BaseDbContext dbContext, AuthContext authContext)
        {
            _dbBaseContext = dbContext;
            _authContext = authContext;
            _dbSet = _dbBaseContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id, int? siteId = null)
        {
            try
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync((T c) => c.Id == id).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> GetByGuidAsync(Guid guid, int? siteId = null)
        {
            try
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync((T c) => c.Guid == guid && !c.IsDeleted).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<List<T>> GetAllAsync(int? siteId = null)
        {
            try
            {
                return await (from c in _dbSet.AsNoTracking()
                              where !c.IsDeleted
                              select c).ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<PagingItemsDTO<T>> GetAllAsync(int page, int pageSize, int? siteId = null)
        {
            try
            {
                IOrderedQueryable<T> query = from c in _dbSet.AsNoTracking()
                                             where !c.IsDeleted
                                             select c into m
                                             orderby m.Id descending
                                             select m;
                return await GetListPaging(query, page, pageSize);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagingItemsDTO<T>> GetListPaging(IQueryable<T> query, int page, int pageSize)
        {
            PagingItemsDTO<T> pagingItemsDTO = new PagingItemsDTO<T>();
            PagingItemsDTO<T> pagingItemsDTO2 = pagingItemsDTO;
            pagingItemsDTO2.Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
            PagingItemsDTO<T> pagingItemsDTO3 = pagingItemsDTO;
            PagingInfoDTO pagingInfoDTO = new PagingInfoDTO
            {
                Page = page,
                PageSize = pageSize
            };
            PagingInfoDTO pagingInfoDTO2 = pagingInfoDTO;
            pagingInfoDTO2.TotalItems = await query.CountAsync().ConfigureAwait(continueOnCapturedContext: false);
            pagingItemsDTO3.PagingInfo = pagingInfoDTO;
            return pagingItemsDTO;
        }
    }
}

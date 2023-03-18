using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using Microsoft.AspNetCore.Identity;

namespace BaseConfig.BaseDbContext.Common
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> GetByIdAsync(int id, int? siteId = null);

        Task<T> GetByGuidAsync(Guid guid, int? siteId = null);

        Task<bool> AnyAsync(int id, int? siteId = null);

        Task<bool> AnyGuidAsync(Guid guid, int? siteId = null);

        T Add(T newEntity);

        T Update(T updateEntity);

        Task<bool> DeleteAsync(T deleteEntity);

        Task ExecuteTransactionAsync(Func<Task<VoidMethodResult>> action);
    }
}

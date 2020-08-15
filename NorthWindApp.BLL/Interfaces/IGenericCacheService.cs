using System.Threading.Tasks;

namespace NorthWindApp.BLL.Interfaces
{
    public interface IGenericCacheService<TEntity>
    {
        Task<TEntity> GetEntityAsync(string key);
        Task SetEntityAsync(string key, TEntity entity);
        Task ClearAsync();
    }
}

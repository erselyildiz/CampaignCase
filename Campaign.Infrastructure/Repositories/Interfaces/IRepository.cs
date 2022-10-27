using MongoDB.Bson;
using System.Threading.Tasks;

namespace Campaign.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(ObjectId objectId);
    }
}
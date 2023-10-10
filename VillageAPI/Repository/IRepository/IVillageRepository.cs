using VillageAPI.Models;

namespace VillageAPI.Repository.IRepository
{
    public interface IVillageRepository : IBaseRepository<Village>
    {
        Task<Village> UpdateAsync(Village entity);
    }
}

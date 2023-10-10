using VillageAPI.DataAccess;
using VillageAPI.Models;
using VillageAPI.Repository.IRepository;

namespace VillageAPI.Repository
{
    public class VillageRepository : BaseRepository<Village>, IVillageRepository
    {
        private readonly AppDbContext _db;

        public VillageRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Village> UpdateAsync(Village entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villages.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

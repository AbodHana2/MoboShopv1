
using MoboShopv1.Areas.Identity.Data;
using MoboShopv1.Models.Interfaces;

namespace MoboShopv1.Data.Repo
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MoboShopContext _db;
        public IProduct product { get; }
        public UnitofWork(MoboShopContext db)
        {
            _db = db;
            product = new ProductDAO(db);
        }
    }
}

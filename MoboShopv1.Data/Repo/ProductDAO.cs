

using MoboShopv1.Areas.Identity.Data;
using MoboShopv1.Models;
using MoboShopv1.Models.Interfaces;

namespace MoboShopv1.Data.Repo
{
    public class ProductDAO : IProduct
    {
        private readonly MoboShopContext _db;
        public ProductDAO(MoboShopContext db)
        {
            _db = db;
        }

        public int Add(Product product)
        {
            if (product != null)
            {
                using (var dbTrans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        _db.Products.Add(product);
                        _db.SaveChanges();
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                    }
                }
                return product.ID;
            }
            return 0;
        }

        public bool Edit(Product product)
        {
            if (product != null)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public List<Product> GetAllByType(ProductType type)
        {
            return _db.Products.Where(p => p.Category == type).ToList();
        }

        public Product? GetOne(int? id)
        {
            return _db.Products.FirstOrDefault(p => p.ID == id);
        }

        public bool Remove(int id)
        {
            var product = GetOne(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

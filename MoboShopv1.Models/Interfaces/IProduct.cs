

namespace MoboShopv1.Models.Interfaces
{
    public interface IProduct : IDTO<Product>
    {
        List<Product> GetAllByType(ProductType type);
    }
}

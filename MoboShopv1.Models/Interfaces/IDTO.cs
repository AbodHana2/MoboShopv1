

namespace MoboShopv1.Models.Interfaces
{
    public interface IDTO<T>
    {
        int Add(T Object);
        T? GetOne(int? id);
        List<T> GetAll();
        bool Remove(int id);
        bool Edit(T Object);
    }
}

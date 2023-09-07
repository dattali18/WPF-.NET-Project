using DO;

namespace DalApi;

public interface IOrder: ICRUD<Order>
{
    public int Create(string? customer, string? email, string? address, DateTime? createTime, DateTime? shipping, DateTime? delivery);

    void Update(int ID, Order ord);

    void Delete(int ID);
}

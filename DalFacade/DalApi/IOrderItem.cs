using DO;

namespace DalApi;

public interface IOrderItem: ICRUD<OrderItem>
{

    int Create(Products prod, Order ord, int amount);

    void Update(int prodId, int ordId, OrderItem ordItm);

    void Delete(int prodId, int ordId);
}

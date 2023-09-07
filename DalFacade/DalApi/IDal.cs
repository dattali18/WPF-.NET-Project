namespace DalApi;

public interface IDal
{
    public IProducts Products { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
}


namespace BlApi;

public interface IBl
{
    public ICart Cart { get;}

    public IOrder Order { get;}

    public IOrderTracking OrderTracking { get;}

    public IOrderItem OrderItem { get;}

    public IOrderForList OrderForList { get;}

    public IProduct Product { get;}

    public IProductItem ProductItem { get;}

    public IProductForList ProductForList { get;}
}

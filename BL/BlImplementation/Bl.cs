using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public ICart Cart { get; }

    public IOrder Order { get; }

    public IOrderTracking OrderTracking { get; }

    public IOrderItem OrderItem { get; }

    public IOrderForList OrderForList { get; }

    public IProduct Product { get; }

    public IProductItem ProductItem { get; }

    public IProductForList ProductForList { get; }

    internal Bl()
    {
        Cart = new BlCart();
        Order = new BlOrder();
        OrderForList = new BlOrderForList();
        OrderTracking = new BlOrderTracking();
        OrderItem = new BlOrderItem();
        Product = new BlProduct();
        ProductForList = new BlProductForList();
        ProductItem = new BlProductItem();
    }

}

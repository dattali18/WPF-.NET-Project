namespace DO;

public struct OrderItem
{
    /// <summary>
    ///  ProductID: identify a unique Products
    /// </summary>
    public int ProductID { get; set; }


    /// <summary>
    ///  ProductID: identify a unique Order
    /// </summary>
    public int OrederID { get; set; }

    /// <summary>
    /// Price: the price of Products
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///  Amount: of the Products in the Order
    /// </summary>
    public int Amount { get; set; }


    /// <summary>
    /// return a string represention of the structure Products
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $@"
Product ID:       {ProductID}
Order ID:         {OrederID}
Price:            {Price}
Amount:           {Amount}";
    }

    // copy ctor
    public OrderItem(OrderItem oreitm)
    {
        ProductID = oreitm.ProductID;
        OrederID = oreitm.OrederID;
        Price = oreitm.Price;
        Amount = oreitm.Amount;
    }
    public OrderItem(int prodId, int orederId, double price, int amount)
    {
        ProductID = prodId;
        OrederID = orederId;
        Price = price;
        Amount = amount;
    }

}

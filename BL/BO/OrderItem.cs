namespace BO;

public class OrderItem
{
    /// <summary>
    /// ProductId: the id of the Products in the orderitem
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// OrderId: the id of the Order
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Name: the name of the costumer
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price: the price of the individual Products
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Amount: the amount of the individual Products in this OrderItem
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// TotalPrice: the total price of the OrderItem (Amount * Price)
    /// </summary>
    public double TotalPrice { get; set; }

    // Methods

    public override string ToString()
    {
        return $@"
ProductId:        {ProductId}
OrderId:          {OrderId}
Name:             {Name}
Price:            {Price}
Amount:           {Amount}
TotalPrice:       {TotalPrice}";
    }
}

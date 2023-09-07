
namespace BO;

public class OrderForList
{
    /// <summary>
    /// ID: the id of the Order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name: the name of the costumer
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Status: the statuc of the Order
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// Amount: the amount of OrderItem in the Order
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// TotalPrice: the total price of the Order
    /// </summary>
    public double TotalPrice { get; set; }

    // Methods

    public override string ToString()
    {
        return $@"
ID:           {ID}
Name:         {Name}
Status:       {Status}
Amount:       {Amount}
TotalPrice:   {TotalPrice}";
    }
}

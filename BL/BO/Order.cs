using DO;

namespace BO;

public class Order
{
    /// <summary>
    /// ID: the number identifying the Order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// CostumerName: the name of the costumer
    /// </summary>
    public string? CostumerName { get; set; }

    /// <summary>
    /// CostumerEmail: the email of the costumer
    /// </summary>
    public string? CostumerEmail { get; set;}

    /// <summary>
    /// CostumerAddress: the address of the costumer
    /// </summary>
    public string? CostumerAddress { get; set; }

    /// <summary>
    /// Status: the status of the Order such as OrderPlaced, Shipped
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// OrderPlaced : the date in wich the Order was placed
    /// </summary>
    public DateTime? OrderPlaced { get; set; }

    /// <summary>
    /// Shipped: the date in wich the Order will be (as been) shipped
    /// </summary>
    public DateTime? Shipped { get; set; }

    /// <summary>
    /// Delivered: the date in wich the Order will be (as been) deliverd to the costumer
    /// </summary>
    public DateTime? Delivered { get; set; }

    /// <summary>
    /// Items: a list of OrderItem (Data Entity) of the Order
    /// </summary>
    public List<BO.OrderItem?>? Items { get; set;}


    // Methods

    public override string ToString()
    {
        string s1 =  $@"
ID:               {ID}
CostumerName:     {CostumerName}
CostumerEmail:    {CostumerEmail}
CostumerAddress:  {CostumerAddress}
Status:           {Status}
OrderPlaced:      {OrderPlaced}
Delivered:        {Delivered}
Shipped:          {Shipped}
Items:
";
        foreach (var item in Items ?? new())
        {
            s1 = s1 + $"{item}\n";
        }
        return s1;
    }
}

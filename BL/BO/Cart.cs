using DO;


namespace BO;

public class Cart
{
    /// <summary>
    /// CostumerName: the name of the costumer who own's the cart
    /// </summary>
    public string? CostumerName { get; set; }

    /// <summary>
    /// CostumerEmail: the email of the costumer
    /// </summary>
    public string? CostumerEmail { get; set; }

    /// <summary>
    /// CostumerAddress: the address of the costumer
    /// </summary>
    public string? CostumerAddress { get; set; }

    /// <summary>
    /// Items: a list of all the s_Products in the cart
    /// </summary>
    public List<DO.OrderItem?>? Items { get; set; }


    /// <summary>
    /// TotalPrice: the total price of the cart
    /// </summary>
    public double TotalPrice { get; set; }

    // Methods

    public override string ToString()
    {
        string s1 =  $@"
CostumerName:         {CostumerName}
CostumerEmail:        {CostumerEmail}
CostumerAddress:      {CostumerAddress}
TotalPrice:           {TotalPrice}
Items [{((Items != null) ? Items.Count : 0)}]:
";
        foreach (var item in Items)
        {
            s1 += $"{item}\n";
        }
        return s1;
    }
}

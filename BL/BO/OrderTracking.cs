
namespace BO;

public class OrderTracking
{
    /// <summary>
    /// ID: the id of the Order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Status: the status of the Order such as Processing etc...
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// Values: a list of tuple of (Date, status)
    /// </summary>
    public List<(DateTime?, OrderStatus?)?>? Values { get; set; }

    // Methods

    public override string ToString()
    {
        string s1 = @$"
ID:          {ID}
Status:      {Status}
Values:
";
        if (Values == null)
            return s1;
        foreach (var item in Values)
        {
            s1 += $"{item}\n";
        }
        return s1;
    }
}

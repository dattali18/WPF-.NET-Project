using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Order Class: this is the class to represent an Order
/// </summary>
public struct Order
{
    /// <summary>
    ///  ID: identify a unique Order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// CustomerName: the name of the customer
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// CustomerEmail: the email of the customer
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// CustomerAdress: the address of the customer
    /// </summary>
    public string? CustomerAdress { get; set; }


    /// <summary>
    /// OrderDate: the date in which the Order was ordered
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// ShipingDate: the date in which the Order was shipped
    /// </summary>
    public DateTime? ShipingDate { get; set; }

    /// <summary>
    /// DeliveryDate: the date in which the Order was delivered
    /// </summary>
    public DateTime? DeliveryDate { get; set; }




    /// <summary>
    /// return a string represention of the structure Order
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $@"
Order ID:         {ID}
CustomerName:     {CustomerName}
CustomerEmail:    {CustomerEmail}
CustomerAdress:   {CustomerAdress}
OrederDate:       {OrderDate}
ShipingDate:      {ShipingDate}
DeliveryDate:     {DeliveryDate}";
    }

    // copy ctor
    public Order(Order ord)
    {
        ID = ord.ID;
        CustomerName = new(ord.CustomerName);
        CustomerEmail = new(ord.CustomerEmail);
        CustomerAdress = new(ord.CustomerAdress);
        OrderDate = ord.OrderDate;
        ShipingDate = ord.ShipingDate;
        DeliveryDate = ord.DeliveryDate;
    }

    public Order(int id, string customer, string email, string address,
        DateTime? createTime, DateTime? shipping, DateTime? delivery)
    {
        ID = id;
        CustomerName = new(customer);
        CustomerEmail = new(email);
        CustomerAdress = new(address);
        OrderDate = createTime;
        ShipingDate = shipping;
        DeliveryDate = delivery;
    }

}

using DO;
using System.Xml.Linq;

namespace Dal.Utils;

internal static class Convertor
{

    public static DO.Products? XmlToProduct(XElement element)
    {
        DO.Products? prod = new()
        {
            ID = int.Parse(element.Element("ID")?.Value),
            Name = element.Element("Name").Value,
            Categorie = FromStringCategorie(element.Element("Categorie").Value),
            Price = double.Parse(element.Element("Price").Value),
            InStock = int.Parse(element?.Element("InStock")?.Value)
        };

        return prod;
    }

    public static DO.Order? XmlToOrder(XElement element)
    {
        DateTime? orderDate, shipingDate, deliveryDate;
        if (int.TryParse(element.Element("ID")?.Value, out int id))
        {
            // do something
        }
        if (element.Element("OrderDate")?.Value == "")
        {
            orderDate = null;
        } else
        {
            orderDate = DateTime.Parse(element.Element("OrderDate")?.Value);
        }
        if (element.Element("ShipingDate")?.Value == "") {
            shipingDate = null;
        } else {
            shipingDate = DateTime.Parse(element.Element("ShipingDate")?.Value);
        }
        if (element.Element("DeliveryDate")?.Value == "") {
            deliveryDate = null;
        } else
        {
            deliveryDate = DateTime.Parse(element.Element("DeliveryDate")?.Value);
        }
        DO.Order? order = new()
        {
            ID = id,
            CustomerName = element.Element("CustomerName")?.Value,
            CustomerEmail = element.Element("CustomerEmail")?.Value,
            CustomerAdress = element.Element("CustomerAdress")?.Value,
            OrderDate = orderDate,
            ShipingDate = shipingDate,
            DeliveryDate = deliveryDate
        };
        return order;
    }

    public static DO.OrderItem? XmltoOrderItem(XElement element) 
    {
        DO.OrderItem? order = new()
        {
            ProductID = int.Parse(element.Element("ProductID")?.Value),
            OrederID = int.Parse(element.Element("OrderID")?.Value),
            Price = double.Parse(element.Element("Price")?.Value),
            Amount = int.Parse(element.Element("Amount")?.Value)
        };

        return order;
    }

    public static Categories? FromStringCategorie(string cat) => cat switch
    {
        nameof(Categories.Laptop) => Categories.Laptop,
        nameof(Categories.Desktop) => Categories.Desktop,
        nameof(Categories.Accessories) => Categories.Accessories,
        nameof(Categories.Server) => Categories.Server,
        nameof(Categories.Phone) => Categories.Phone,
        _ => Categories.All
    };
}



using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalOrder : IOrder
{
    private void save() => root?.Save(@"..\xml\Orders.xml");
    private XElement load() => root = XElement.Load(@"..\xml\Orders.xml");

    private XElement root = new("Order");

    private XElement? Find(int id)
    {
        load();

        List<XElement> elements = new(root.Elements());

        return elements.Find(item => item?.Element("ID")?.Value == id.ToString());
    }

    public int Add(Order t)
    {
        load();
        root?.Add(
            new XElement("Order",
                new XElement("ID", t.ID),
                new XElement("CustomerName", t.CustomerName),
                new XElement("CustomerEmail", t.CustomerEmail),
                new XElement("CustomerAdress", t.CustomerAdress),
                new XElement("OrderDate", t.OrderDate),
                new XElement("ShipingDate", t.ShipingDate),
                new XElement("DeliveryDate", t.DeliveryDate)
            )
        );
        save();
        return t.ID;
    }

    public int Create(string? customer, string? email, string? address, DateTime? createTime, DateTime? shipping, DateTime? delivery)
    {
        load();
        int id = config.LastOrederID;

        root?.Add(
            new XElement("Order",
                new XElement("ID", id),
                new XElement("CustomerName", customer),
                new XElement("CustomerEmail", email),
                new XElement("CustomerAdress", address),
                new XElement("OrderDate", createTime),
                new XElement("ShipingDate", shipping),
                new XElement("DeliveryDate", delivery)
            )
        );
        save();
        return id;
    }

    public void Delete(int ID)
    {
        load();
        // removing the element from the root
        var elem = Find(ID);
        if (elem != null)
        {
            elem.Remove();
            save();
        }
        else
            throw new ObjectNotFoundException("from Delete xml Product");

    }

    public bool Delete(Order t)
    {
        load();
        // removing the element from the root
        var elem = Find(t.ID);
        if (elem != null)
        {
            elem.Remove();
            save();
            return true;
        }
        else
            throw new ObjectNotFoundException("from Delete xml Product");
    }

    public Order? Get(Predicate<Order?> func)
    {
        load();
        List<Order?>? element = new(
                                    from elem in root.Elements()
                                    where elem != null
                                    select Utils.Convertor.XmlToOrder(elem)
                                    );
        return element.Find(func);
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        load();
        if (func != null)
            return from elem in root.Elements()
                   where elem != null && func(Utils.Convertor.XmlToOrder(elem))
                   select Utils.Convertor.XmlToOrder(elem);
        else
            return from elem in root.Elements()
                   where elem != null
                   select Utils.Convertor.XmlToOrder(elem);
    }

    public void Update(int ID, Order ord)
    {
        load();

        Delete(ID);
        Add(ord);

        save();
    }

    public void Update(Order t1, Order t2)
    {
        load();

        Delete(t1.ID);
        Add(t2);

        save();
    }

    public void Update(Order t)
    {
        load();

        Delete(t.ID);
        Add(t);

        save();
    }
}

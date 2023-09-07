using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    private void save() => root?.Save(@"..\xml\OrderItems.xml");
    private XElement load() => root = XElement.Load(@"..\xml\OrderItems.xml");

    private XElement root = new("OrderItems");

    private XElement? Find(int prodID, int ordID)
    {
        load();

        List<XElement> elements = new(root.Elements());

        return elements.Find(item => item?.Element("ProductID")?.Value == prodID.ToString() &&
            item?.Element("OrderID")?.Value == ordID.ToString());
    }

    public int Add(OrderItem t)
    {
        load();
        root?.Add(
            new XElement("OrderItem",
                new XElement("ProductID", t.ProductID),
                new XElement("OrderID", t.OrederID),
                new XElement("Price", t.Price),
                new XElement("Amount", t.Amount)
                )
        );
        save();
        return t.OrederID;
    }

    public int Create(Products prod, Order ord, int amount)
    {
        load();

        root?.Add(
            new XElement("OrderItem",
                new XElement("ProductID", prod.ID),
                new XElement("OrderID", ord.ID),
                new XElement("Price", prod.Price * amount),
                new XElement("Amount", amount)
            )
        );

        save();

        return ord.ID;
    }

    public void Delete(int prodId, int ordId)
    {
        load();

        var elem = Find(prodId, ordId);

        if (elem != null)
        {
            elem.Remove();
            save();
        }
        else
            throw new ObjectNotFoundException("from Delete xml OrderItem");
    }

    public bool Delete(OrderItem t)
    {
        try
        {
            Delete(t.ProductID, t.OrederID);
            return true;
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    public OrderItem? Get(Predicate<OrderItem?> func)
    {
        load();

        List<OrderItem?>? orderItems = new(
            from elem in root.Elements()
            where elem != null
            select Utils.Convertor.XmltoOrderItem(elem));

        return orderItems.Find(func);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        load();
        if (func != null)
        {
            return from elem in root.Elements()
                   where elem != null && func(Utils.Convertor.XmltoOrderItem(elem))
                   select Utils.Convertor.XmltoOrderItem(elem);
        }
        else
        {
            return from elem in root.Elements()
                   where elem != null
                   select Utils.Convertor.XmltoOrderItem(elem);
        }
    }

    public void Update(int prodId, int ordId, OrderItem ordItm)
    {
        load();

        Delete(prodId, ordId);
        Add(ordItm);

        save();
    }

    public void Update(OrderItem t1, OrderItem t2)
    {
        load();

        Delete(t1);
        Add(t2);

        save();
    }

    public void Update(OrderItem t)
    {
        load();

        Delete(t.ProductID, t.OrederID);
        Add(t);

        save();
    }
}

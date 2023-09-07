using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProducts
{
    private void save() => root?.Save(@"..\xml\Products.xml");
    private XElement load() => root = XElement.Load(@"..\xml\Products.xml");

    private XElement root = new("Products");

    private XElement? Find(int id)
    {
        load();

        List<XElement> elements = new(root.Elements());

        return elements.Find(item => item?.Element("ID")?.Value == id.ToString());
    }

    public int Add(Products t)
    {
        load();
        root?.Add(
            new XElement("Product",
                new XElement("ID", t.ID),
                new XElement("Name", t.Name),
                new XElement("Categorie", t.Categorie),
                new XElement("Price", t.Price),
                new XElement("InStock", t.InStock)
                )
            );
        save();
        return t.ID;
    }

    public int Create(string? name, Categories categorie, double price, int inStock)
    {
        load();

        int id = config.LastProductID;

        root?.Add(
            new XElement("Product",
                new XElement("ID", id),
                new XElement("Name", name),
                new XElement("Categorie", categorie),
                new XElement("Price", price),
                new XElement("InStock", inStock)
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
        if (elem != null) { 
            elem.Remove();
            save();
        }
        else
            throw new ObjectNotFoundException("from Delete xml Product");
        
    }

    public bool Delete(Products t)
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

    public Products? Get(Predicate<Products?> func)
    {
        load();
        List<Products?>? element = new(
                                    from elem in root.Elements()
                                    where elem != null
                                    select Utils.Convertor.XmlToProduct(elem)
                                    );
        return element.Find(func);
    }

    public IEnumerable<Products?> GetAll(Func<Products?, bool>? func = null)
    {
        load();
        if (func != null)
            return from elem in root.Elements()
                   where elem != null && func(Utils.Convertor.XmlToProduct(elem))
                   select Utils.Convertor.XmlToProduct(elem);
        else
            return  from elem in root.Elements()
                    where elem != null
                    select Utils.Convertor.XmlToProduct(elem);
    }

    public void Update(int ID, Products prod)
    {
        load();

        Delete(ID);
        Add(prod);

        save();
    }

    public void Update(Products t1, Products t2)
    {
        load();

        Delete(t1.ID);
        Add(t2);

        save();
    }

    public void Update(Products t)
    {
        load();

        Delete(t.ID);
        Add(t);

        save();
    }
}
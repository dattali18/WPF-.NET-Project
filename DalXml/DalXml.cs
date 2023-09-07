using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{
    /// <summary>
    /// return an instence of the class DALList
    /// </summary>
    public static IDal Instance { get { return lazy.Value; } }

    /// <summary>
    /// return an accsses to DALProduct
    /// </summary>
    public IProducts Products { get; } = new DalProduct();

    /// <summary>
    /// return an accsses to DALOrder
    /// </summary>
    public IOrder Order { get; } = new DalOrder();

    /// <summary>
    /// return an accsses to DALOrderItem
    /// </summary>
    public IOrderItem OrderItem { get; } = new DalOrderItem();

    // making the singelton thread safe and lasy init using the built in lazy class in c#
    // explainetion:
    // on of the withdraw of a singelton class is that it can reduce the potential for parallelism (aka using thread) in a program by locking
    // wich means that because the singelton has only one instance of the class what can happend with threading the if one
    // thread uses the class it's lock for the other thread
    // and by using lasy init (wich is already implemented by c#) is that we pass a deleget to the ctor
    // in our case a labmda expresion "() => new DALList()"
    // and the Lasy class will check for us wheather or not the Instance was created with the isValueCreated property
    private static readonly Lazy<DalXml> lazy = new(() => new DalXml());


    private DalXml()
    {
        var root1 = XElement.Load(@"..\xml\Products.xml");
        List<Products?>? element1 = new(
                            from elem in root1.Elements()
                            where elem != null
                            orderby elem.Element("ID")?.Value.ToString()
                            select Utils.Convertor.XmlToProduct(elem)
                            );

        var root2 = XElement.Load(@"..\xml\Orders.xml");
        List<Order?>? element2 = new(
                            from elem in root2.Elements()
                            where elem != null
                            orderby elem.Element("ID")?.Value.ToString()
                            select Utils.Convertor.XmlToOrder(elem)
                            );


        config.LastProductID = (int)element1.Last()?.ID;
        config.LastOrederID = (int)element2.Last()?.ID;
    }
}

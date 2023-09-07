using DalApi;

namespace Dal;

internal sealed class DALList : IDal
{
    /// <summary>
    /// return an instence of the class DALList
    /// </summary>
    public static IDal Instance { get { return lazy.Value; } }

    /// <summary>
    /// return an accsses to DALProduct
    /// </summary>
    public IProducts Products { get; }

    /// <summary>
    /// return an accsses to DALOrder
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// return an accsses to DALOrderItem
    /// </summary>
    public IOrderItem OrderItem { get; }

    // making the singelton thread safe and lasy init using the built in lazy class in c#
    // explainetion:
    // on of the withdraw of a singelton class is that it can reduce the potential for parallelism (aka using thread) in a program by locking
    // wich means that because the singelton has only one instance of the class what can happend with threading the if one
    // thread uses the class it's lock for the other thread
    // and by using lasy init (wich is already implemented by c#) is that we pass a deleget to the ctor
    // in our case a labmda expresion "() => new DALList()"
    // and the Lasy class will check for us wheather or not the Instance was created with the isValueCreated property
    private static readonly Lazy<DALList> lazy = new(() => new DALList());

    public int GetLastId()
    {
        return DataSource.Confige.LastProductID;
    }

    private DALList() {
        // init the data
        DataSource.Initialize();

        Products = new DALProducts();
        Order = new DALOrder();
        OrderItem = new DALOrderItem();
    }
}

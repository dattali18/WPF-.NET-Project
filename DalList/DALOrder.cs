using DalApi;
using DO;


namespace Dal;

public class DALOrder : IOrder
{
    /// <summary>
    /// Create: create a DO.Order obejct in the DAL
    /// </summary>
    /// <param name="customer">the name of the costumer</param>
    /// <param name="email">the email of the costumer</param>
    /// <param name="address">the address of the costumer</param>
    /// <param name="createTime">the date the Order was placed</param>
    /// <param name="shipping">the date the Order would be /was  shiped</param>
    /// <param name="delivery">the date the Order will/has arrived</param>
    /// <returns>the id of the Products</returns>
    public int Create(string? customer, string? email, string? address, 
        DateTime? createTime, DateTime? shipping, DateTime? delivery)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        else if (email == null)
            throw new ArgumentNullException(nameof(email));
        else if (address == null)
            throw new ArgumentNullException(nameof(address));
        //else if (createTime == null)
        //    throw new ArgumentNullException(nameof(createTime));
        //else if (shipping == null)
        //    throw new ArgumentNullException(nameof(shipping));
        //else if (delivery == null)
        //    throw new ArgumentNullException(nameof(delivery));
        int ID = DataSource.Confige.LastOrederID;
        Order order = new(ID, customer, email, address, createTime, shipping, delivery);
        DataSource.AddOrder(order);
        return ID;
    }

    /// <summary>
    /// Update: update the DO.Order with ID = id with the date of ord
    /// </summary>
    /// <param name="ID">id of the DO.Order you want to update</param>
    /// <param name="ord">the DO.Order with the new data</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(int ID, Order ord)
    {
        if (DataSource.SearchOrder(ID) == null)
            throw new ObjectNotFoundException("order was not found");
        DataSource.s_Orders.RemoveAll(order => order?.ID == ID);
        DataSource.s_Orders.Add(ord);
        //int index = DataSource.SearchOrder(ID);
        //if (index != -1)
        //{
        //    DataSource.s_Orders[index] = ord;
        //}
        //else
        //{
        //    throw new ObjectNotFoundException("from DALOrder.Update");
        //}
    }

    /// <summary>
    /// Delete: try to delete the DO.Order with ID = id
    /// </summary>
    /// <param name="ID">id of the DO.Order you want to delete</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Delete(int ID)
    {
        if (DataSource.SearchOrder(ID) == null)
            throw new ObjectNotFoundException("from DALOrder.Delete");
        DataSource.s_Orders.Remove(DataSource.SearchOrder(ID));
        //int index = DataSource.SearchOrder(ID);
        //if (index == -1)
        //{
        //    throw new ObjectNotFoundException("from DALOrder.Delete");
        //}
        //else
        //{
        //    DataSource.s_Orders.RemoveAt(index);
        //}
    }

    /// <summary>
    /// Add: add the DO.Order to the list of Order in DAL
    /// </summary>
    /// <param name="t">the DO.Order you want to add</param>
    /// <returns>the id of the Order</returns>
    /// <exception cref="DuplicateIDException"></exception>
    public int Add(Order t)
    {
        if(DataSource.SearchOrder(t.ID) != null)
        {
            throw new DuplicateIDException("from DALOrder.Add");
        }
        DataSource.s_Orders.Add(t);
        return t.ID;  
    }

    /// <summary>
    /// Update: recive two DO.Order object and take the info from t2 and put's it into t1 (wich should be in DAL)
    /// </summary>
    /// <param name="t1">the Order to update</param>
    /// <param name="t2">the Order containing the data</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(Order t1, Order t2)
    {
        if (DataSource.SearchOrder(t1.ID) == null)
            throw new ObjectNotFoundException("product was not found");
        DataSource.s_Orders.RemoveAll(prod => prod?.ID == t1.ID);
        DataSource.s_Orders.Add(t2);
        //int i = DataSource.SearchOrder(t1.ID);
        //if (i == -1)
        //{
        //    throw new ObjectNotFoundException("from DALOrder.Update");
        //}
        //t2.ID = t1.ID;
        //DataSource.s_Orders[i] = t2;
    }

    /// <summary>
    /// Delte: recive an object t and try to dalete it from DAL
    /// </summary>
    /// <param name="t">the object to delete</param>
    /// <returns>return true if the deletion was successful and flase if the Products didn't exist or the deletiong didn't work</returns>
    public bool Delete(Order t) => DataSource.s_Orders.Remove(t);
    //{
    //    return DataSource.s_Orders.Remove(t);
    //}

    public Order? Get(Predicate<Order?> func) => DataSource.s_Orders?.FirstOrDefault(order => func(order));
    //{
    //    foreach (var item in DataSource.s_Orders)
    //    {
    //        if(item == null) continue;
    //        if (func(item) == true)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}

    /// <summary>
    /// GetAll: return all the DO.Order in DAL
    /// </summary>
    /// <returns>a list of all the Order in DAL</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null) => (func == null ? DataSource.s_Orders?.Select(order => order) :
        DataSource.s_Orders?.Where(order => func(order)) ?? throw new ObjectNotFoundException("order not found"))
        ?? throw new ObjectNotFoundException("the order list wasn't found");

    //{
    //    if (func == null)
    //        return DataSource.s_Orders;
    //    List<Order?> list = new();
    //    foreach (var item in DataSource.s_Orders)
    //    {
    //        if (item == null) continue;
    //        if (func(item) == true)
    //        {
    //            list.Add(item);
    //        }
    //    }
    //    return list;
    //}


    public void Update(Order t)
    {
        if (DataSource.SearchOrder(t.ID) == null)
            throw new ObjectNotFoundException("order was not found");
        DataSource.s_Orders.RemoveAll(order => order?.ID == t.ID);
        DataSource.s_Orders.Add(t);
    }
}

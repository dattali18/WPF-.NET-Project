using DalApi;
using DO;
using System.Security.Cryptography;

namespace Dal;

public class DALOrderItem : IOrderItem
{
    /// <summary>
    /// Create: create a new DO.OrderItem in the Data accsess layer (DAL)
    /// </summary>
    /// <param name="prod">the DO.Products in the OrderItem</param>
    /// <param name="ord">the DO.Order in the OrderItem</param>
    /// <param name="price">the price of the all OrderItem</param>
    /// <param name="amount">the Amount of the Products in the OrderItem</param>
    /// <returns>the id of the DO.Order</returns>
    public static int Create(Products prod, Order ord, double price, int amount)
    {
        if(amount <= 0) return 0;
        OrderItem item = new(prod.ID, ord.ID, price, amount);
        DataSource.AddOrderItem(item);
        // don't know which id to return so we will return the Order id for now
        return ord.ID;
    }

    /// <summary>
    /// Create: create a new DO.OrderItem in the Data accsess layer (DAL)
    /// </summary>
    /// <param name="prod">the DO.Products in the OrderItem</param>
    /// <param name="ord">the DO.Order in the OrderItem</param>
    /// <param name="amount">the Amount of the Products in the OrderItem</param>
    /// <returns>the id of the DO.Order</returns>
    public int Create(Products prod, Order ord, int amount)
    {
        return Create(prod, ord, prod.Price * amount, amount);
    }


    /// <summary>
    /// Update: update the OrderItem with both id's matching with the given id's and update it with the date from the OrderItem obj
    /// </summary>
    /// <param name="prodId">id of the DO.Products</param>
    /// <param name="ordId">id of the DO.Order</param>
    /// <param name="ordItm">the DO.OrderItem with the new data</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(int prodId, int ordId, OrderItem ordItm)
    {
        if (DataSource.SearchOrderItem(prodId, ordId) == null)
            throw new ObjectNotFoundException("product was not found");
        DataSource.s_OrderItems.RemoveAll(orderItem => orderItem?.ProductID == prodId && orderItem?.OrederID == ordId);
        DataSource.s_OrderItems.Add(ordItm);
    }

    /// <summary>
    /// Delete: delete the OrderItem with the id's matching the given id's
    /// </summary>
    /// <param name="prodId">the id of DO.Products</param>
    /// <param name="ordId">the id of DO.Order</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Delete(int prodId, int ordId)
    {
        if (DataSource.SearchOrderItem(prodId, ordId) == null)
            throw new ObjectNotFoundException("from DALOrderItem.Delete");
        DataSource.s_OrderItems.Remove(DataSource.SearchOrderItem(prodId, ordId));
    }

    /// <summary>
    /// Add: add the DO.Order item t to the list
    /// </summary>
    /// <param name="t"></param>
    /// <returns>the id of the OrderItem</returns>
    public int Add(OrderItem t)
    {
       if (DataSource.SearchOrderItem(t.ProductID, t.OrederID) != null)
            throw new DuplicateIDException("from DALOrderItem.Add");
        DataSource.AddOrderItem(t);
        return t.OrederID;
    }

    /// <summary>
    /// Update: recive two DO.OrderItem object and take the info from t2 and put's it into t1 (wich should be in DAL)
    /// </summary>
    /// <param name="t1">the DO.OrderItem to update</param>
    /// <param name="t2">the DO.OrderItem containing the date</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(OrderItem t1, OrderItem t2)
    {
        if (DataSource.SearchOrderItem(t1.ProductID, t1.OrederID) == null)
        {
            throw new ObjectNotFoundException("from DALOrderItem.Update");
        }
        DataSource.s_OrderItems.RemoveAll(orderItem => orderItem?.ProductID == t1.ProductID && orderItem?.OrederID == t1.OrederID);
        DataSource.s_OrderItems.Add(t2);
        //t2.OrederID = t1.OrederID;
        //t2.ProductID = t1.ProductID;
        //DataSource.s_OrderItems[i] = t2;
    }

    /// <summary>
    /// Delete: recive an object t and try to dalete it from DAL
    /// </summary>
    /// <param name="t">the object to delete</param>
    /// <returns>return true if the deletion was successful and flase if the Products didn't exist or the deletiong didn't work</returns>
    public bool Delete(OrderItem t) => DataSource.s_OrderItems.Remove(t);
    //{
    //    return DataSource.s_OrderItems.Remove(t);
    //}

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null) => (func == null ? DataSource.s_OrderItems?.Select(order => order) :
        DataSource.s_OrderItems?.Where(order => func(order)) ?? throw new ObjectNotFoundException("order not found"))
        ?? throw new ObjectNotFoundException("the order list wasn't found");
    //{
    //    if (func == null)
    //        return DataSource.s_OrderItems;
    //    List<OrderItem?> list = new();
    //    foreach (var item in DataSource.s_OrderItems)
    //    {
    //        if (item == null) continue;
    //        if (func(item) == true)
    //        {
    //            list.Add(item);
    //        }
    //    }
    //    return list;
    //}

    public OrderItem? Get(Predicate<OrderItem?> func) => DataSource.s_OrderItems?.FirstOrDefault(orderitem => func(orderitem));

    //{
    //    foreach (var item in DataSource.s_OrderItems)
    //    {
    //        if (item == null) continue;
    //        if (func(item) == true)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}


    public void Update(OrderItem t)
    {
        if (DataSource.SearchOrderItem(t.ProductID, t.OrederID) == null)
            throw new ObjectNotFoundException("order item was not found");
        DataSource.s_OrderItems.RemoveAll(orderItem => orderItem?.ProductID == t.ProductID && orderItem?.OrederID == t.OrederID);
        DataSource.s_OrderItems.Add(t);
    }
}

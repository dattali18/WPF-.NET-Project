using BO;
using DalApi;
using System;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    readonly IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");


    /// <summary>
    /// GetOrder: get the id of an Order (DO) and return the Order in the BO format
    /// </summary>
    /// <param name="id">the id of DO.Order you want</param>
    /// <returns>BO.Order format of the Order you want</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? GetOrder(int id)
    {
        DO.Order ord = dal.Order.Get(ord => ord?.ID == id) ?? throw new ObjectNotFoundException("from GetOrder");
        return BlUtils.Convertor.ToBoOrder(ord);
    }

    /// <summary>
    /// GetOrdersListForAdmin: return a list of all the Order in the OrderForlist format
    /// </summary>
    /// <returns>a list of all the DO.Order in the OrderForlist format</returns>
    public IEnumerable<OrderForList?> GetOrdersListForAdmin(Func<DO.Order?, bool>? func = null)
    {
        List<DO.Order?> ordlist = new(dal.Order.GetAll(func));
        List<BO.OrderForList?> lst = new(
                                        from ord in ordlist
                                        select BlUtils.Convertor.ToBoOrderForList(ord)
                                        );
                                     
        //foreach (var item in ordlist)
        //{
        //    lst.Add(BlUtils.Convertor.ToBoOrderForList(item));
        //}
        return lst;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? SetSendOrderDeliveredForAdmin(int id)
    {
        DO.Order ord1 = dal.Order.Get(ord => ord?.ID == id) ?? throw new ObjectNotFoundException("from SetSendOrderDeliveredForAdmin");
        ord1.DeliveryDate = DateTime.Today;
        dal.Order.Update(id, ord1);
        BO.Order ord2 =BlUtils.Convertor.ToBoOrder(ord1) ?? throw new NullReferenceException("");
        return ord2;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? SetSendOrderForAdmin(int id)
    {
        try
        {
            DO.Order ord1 = dal.Order.Get(ord => ord?.ID == id) ?? throw new NullReferenceException("");
            //ord1.ShipingDate = DateTime.Today;
            ord1.ShipingDate = DateTime.Now;
            dal.Order.Update(id, ord1);
            BO.Order ord2 = BlUtils.Convertor.ToBoOrder(ord1) ?? throw new NullReferenceException("");
            return ord2;
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    public OrderTracking? GetOrderTrackingForAdmin(int id)
    {
        try
        {
            DO.Order order = dal.Order.Get(ord => ord?.ID == id) ?? throw new NullReferenceException("");
            OrderTracking? orderTracking = BlUtils.Convertor.ToBoOrderTracking(order);
            return orderTracking;
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool SetUpdateOrderForAdmin(BO.OrderItem orderItem, int newAmount)
    {
        try
        {
            DO.OrderItem orderItem1 = dal.OrderItem.Get(ordItem =>
                ordItem?.ProductID == orderItem.ProductId &&
                ordItem?.OrederID == orderItem.OrderId) ?? throw new NullReferenceException("");

            if (orderItem.Amount > 0)
            {
                orderItem1.Price = orderItem.Price;
                orderItem1.Amount = newAmount;
                dal.OrderItem.Update(orderItem1);
            }
            else if (orderItem.Amount == 0)
            {
                dal.OrderItem.Delete(orderItem1);
            }
            else
            {
                throw new BlApi.NegativeAmountException("from BlOrder.SetUpdateOrderForAdmin");
            }

            return true;
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? GenerateNextOrderToProccess()
    {
        List<BO.Order?> lst = new(
                                from ord in dal.Order.GetAll()
                                where ord != null
                                select BlUtils.Convertor.ToBoOrder(ord)
                                );
        var OrderQueue = new PriorityQueue<BO.Order?, BO.Order?>(new Utils.OrderComparer());

        lst.ForEach(ord => OrderQueue.Enqueue(ord, ord));

        if(OrderQueue.TryDequeue(out BO.Order? order, out BO.Order? _))
        {
            if (order?.Status != OrderStatus.Delivered)
                return order?.ID;
        }
        return null;
    }
}

using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// GetOrdersListForAdmin: to get a list of all the orderForList
    /// </summary>
    /// <returns>a list of all OrderForList</returns>
    public IEnumerable<OrderForList?> GetOrdersListForAdmin(Func<DO.Order?, bool>? func = null);

    /// <summary>
    /// GetOrder: to get an Order (Order BO) by it's ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the Order (BO) with ID = id</returns>
    public Order? GetOrder(int id);

    /// <summary>
    /// SetSendOrderForAdmin: send the Order with ID = id to the Order (DO)
    /// </summary>
    /// <param name="id">the id of the Order (BO) you want to send</param>
    /// <returns>the Order after it was sent</returns>
    public Order? SetSendOrderForAdmin(int id);

    /// <summary>
    /// SetSendOrderDeliveredForAdmin: send the Order with ID = id to the Order (DO) and change the delivery date
    /// </summary>
    /// <param name="id">the Id of the Order (BO) you wnat to send</param>
    /// <returns>the Order after it updated the delivery date</returns>
    public Order? SetSendOrderDeliveredForAdmin(int id);


    /// <summary>
    /// get the id of a Products and return a Order tracking object of the Order
    /// </summary>
    /// <param name="id">Order id</param>
    /// <returns>orderTracking of the Order</returns>
    public OrderTracking? GetOrderTrackingForAdmin(int id);

    // Bonus
    /// <summary>
    /// sometime the admin can change an Order if for example one of the Order item is missing the admin can change the Order
    /// </summary>
    /// <param name = "orderId" >the id of the Order you want to update</ param >
    /// <param name = "orderItem" >the Order item you want to update change etc...</ param >
    /// <returns>true if the action was successful else return false (probably an exception also)</returns>
    public bool SetUpdateOrderForAdmin(BO.OrderItem orderItem, int newAmount);

    /// <summary>
    /// generate the next order to proccess
    /// </summary>
    public int? GenerateNextOrderToProccess();


}

using BO;

namespace BlApi;

/// <summary>
/// an Interface for the Logic Entity of Cart
/// </summary>
public interface ICart
{
    /// <summary>
    /// SetAddProductForUsers: adding a Prodcut to the Cart
    /// </summary>
    /// <param name="cart">the Cart you want to update</param>
    /// <param name="id">the id of the Products (BO) you want to add</param>
    /// <returns>the Cart after the update</returns>
    public Cart? SetAddProductForUsers(Cart cart, int id);

    /// <summary>
    /// SetAddAmountOfProductForUsers: changing the amount of item in the Products int the Cart
    /// </summary>
    /// <param name="cart">the Cart you want to update</param>
    /// <param name="id">the id of the Products (BO) you want to change</param>
    /// <param name="newAmount">the new amount</param>
    /// <returns></returns>
    public Cart? SetAddAmountOfProductForUsers(Cart cart, int id, int newAmount);

    /// <summary>
    /// SetFinalizeOrderForUsers: finalize the Order and create a Order (DO)
    /// </summary>
    /// <param name="cart">the Cart you want to Order</param>
    /// <param name="Name">the name of the costumer</param>
    /// <param name="Email">the email of the costumer</param>
    /// <param name="Address">the address of the costumer</param>
    public int SetFinalizeOrderForUsers(Cart cart);
}

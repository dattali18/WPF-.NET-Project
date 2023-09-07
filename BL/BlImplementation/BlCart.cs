using BlApi;
using BO;
using DalApi;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class BlCart : ICart
{
    readonly IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");

    public Cart? SetAddAmountOfProductForUsers(Cart cart, int id, int newAmount)
    {
        if (newAmount < 0)
        {
            throw new NegativeAmountException("from BlCart.SetAddAmountOfProductForUsers");
        }
        if(cart.Items == null)
            return cart;
        // finding the item we need to update
        var item = cart.Items.FirstOrDefault(item => item?.ProductID == id) ?? null;
        if (item != null)
        {
            if (newAmount != 0)
            {
                if (IsAvailable(id, newAmount))
                {
                    DO.OrderItem temp = AddToExist(id, item ?? new(), newAmount) ?? throw new NullReferenceException("from SetAddAmountOfProductForUsers");
                    cart.Items.Remove(item);
                    cart.Items.Add(temp);
                } else
                {
                    throw new OutOfStockException("From add new amount to cart");
                }
            }
            // if the new amout is 0 we remove the item from items
            else
            {
                cart.Items.Remove(item);
            }
        }
        cart.TotalPrice = GetCalculteTotalPrice(cart);
        return cart;
    }

    public Cart? SetAddProductForUsers(Cart cart, int id)
    {
        cart.Items ??= new();
        // if the product is allready in the cart we add one the item
        if (cart.Items.Any(item => item?.ProductID == id))
        {
            var item = cart.Items.FirstOrDefault(item => item?.ProductID == id) ?? new DO.OrderItem();
            int amount = item.Amount + 1;
            try
            {
                return SetAddAmountOfProductForUsers(cart, id, amount);
            } catch(OutOfStockException e)
            {
                throw new OutOfStockException("from add product to cart");
            }
            
            }
        // if the Products is not already in the cart
        // if the Products is available (which means does it exist and is the inStock amount > 0
        if (IsAvailable(id, 1))
            {
                DO.Products products = dal.Products.Get(p => p?.ID == id) ?? throw new ObjectNotFoundException("from SetAddProductForUsers");
                // important for now the Order id of the OrderItem is some random int but we need to get a real one once the Order is finislized
                DO.OrderItem? orderItem = new() { ProductID = id, Amount = 1, Price = products.Price, OrederID = 1000000 };
                // adding the new Order item
                cart.Items.Add(orderItem);
                //cart.TotalPrice = GetCalculteTotalPrice(cart);
            }
        else
        {
            throw new OutOfStockException("from BlCart.SetAddProductForUsers");
        }
        cart.TotalPrice = GetCalculteTotalPrice(cart);
        return (BO.Cart?)cart;
        }

    public int SetFinalizeOrderForUsers(Cart cart)
    {
        if (cart.CostumerName == string.Empty || cart.CostumerEmail == string.Empty || cart.CostumerAddress == string.Empty)
        {
            throw new NullReferenceException("from BlCart.SetFinalizeOrderForUsers");
        }
        else if (!IsValidEmail(cart.CostumerEmail))
        {
            throw new EmailNotValidException("from BlCart.SetFinalizeOrderForUsers");
        }
        int id = dal.Order.Create(cart.CostumerName, cart.CostumerEmail, cart.CostumerAddress, DateTime.Now, default, default);
        DO.OrderItem orderItem = new();
        if (cart.Items == null)
            return 0;
        // I assume this is either impossible to convert to LINQ or too complicated for this course
        foreach (var item in cart.Items)
        {
            if (item == null)
                continue;
            orderItem = item ?? new();
            orderItem.OrederID = id;
            try
            {
                dal.OrderItem.Add(orderItem);
                DO.Products products = dal.Products.Get(p => p?.ID == item?.ProductID) ?? throw new ObjectNotFoundException("from SetFinalizeOrderForUsers");
                if (products.InStock - orderItem.Amount >= 0)
                {
                    products.InStock -= orderItem.Amount;
                    dal.Products.Update(products);
                }
                else
                    throw new OutOfStockException("from BlCart.SetFinalizeOrderForUsers");
            }
            catch (ObjectNotFoundException e)
            {
                throw e;
            }

        }
        return id;
    }


    // methods for internal use
    internal static double GetCalculteTotalPrice(Cart cart)
    {
        double totalPrice = 0;

        if (cart.Items == null)
            return 0.0d;

        foreach (var item in cart.Items)
        {
            DO.OrderItem orderItem = item ?? new();
            totalPrice += orderItem.Price;
        }
        return totalPrice;
    }


    static bool IsValidEmail(string? email)
    {
        try
        {
            Regex rx = new Regex(
        @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }
        catch (FormatException)
        {
            return false;
        }

        //if (email == null)
        //    throw new ArgumentNullException(nameof(email));
        //var trimmedEmail = email.Trim();

        //if (trimmedEmail.EndsWith("."))
        //{
        //    return false;
        //}
        //try
        //{
        //    var addr = new System.Net.Mail.MailAddress(email);
        //    return addr.Address == trimmedEmail;
        //}
        //catch
        //{
        //    return false;
        //}
    }

    /// <summary>
    /// This function checks if a Products is in the stoke
    /// </summary>
    /// <param name="id"> Products's id </param>
    /// <returns> if it exist </returns>
    public bool IsAvailable(int id, int amount)
    {
        DO.Products prod = dal.Products.Get(p => p.Value.ID == id) ?? throw new ObjectNotFoundException("from IsAvailable");
        if (prod.InStock - amount >= 0)
            return true;
        else
            return false;
    }

    public DO.OrderItem? AddToExist(int id, DO.OrderItem item, int newAmount)
    {
        item.Amount = newAmount;
        var price = dal.Products.Get(p => p?.ID == id) ?? throw new NullReferenceException("");
        item.Price = price.Price * item.Amount;
        return (DO.OrderItem?)item;
    }
}
using BlApi;
using BO;
using DalApi;


namespace BLTest;

internal class Program
{
    static void Main(string[] args)
    {
        Testing test = new();
    }
}

internal class Testing
{
    private readonly IBl bl = BlApi.Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");
    private readonly IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");

    public Testing()
    {
        LoopMenu();
    }

    internal void PrintMenu()
    {
        Console.WriteLine("\nEnter 0 to exit");
        Console.WriteLine("Enter 1 to test the products entity");
        Console.WriteLine("Enter 2 to test the Order entity");
        Console.WriteLine("Enter 3 to test the Cart entity\n");
    }

    internal void LoopMenu()
    {
        int choice;
        do
        {
            PrintMenu();

            bool flag = int.TryParse(Console.ReadLine(), out choice);
            if (!flag)
            {
                ErrorInvalidInput();
                continue;
            }

            switch (choice)
            {
                case 1:
                    TestProduct();
                    break;
                case 2:
                    TestOrder();
                    break;
                case 3:
                    TestCart();
                    break;
                default:
                    ErrorInvalidInput();

                    break;
            }

        } while (choice != 0);
    }

    private void TestCart()
    {
        Cart cart = new()
        {
            Items = new()
        };
        Console.WriteLine("A new Cart as been created");
        bool flag;
        char option;
        do
        {
            PrintMenuCart();
            flag = char.TryParse(Console.ReadLine(), out option);
            if (!flag)
            {
                ErrorInvalidInput();
                return;
            }
            flag = false;
            switch (option)
            {
                case '1':
                    cart = AddProductToCart(cart);
                    Console.WriteLine(cart);
                    break;
                case '2':
                    cart = AddNewAmountOfProduct(cart);
                    Console.WriteLine(cart);
                    break;
                case '3':
                    flag = FinilizeOrder(cart);
                    Console.WriteLine(cart);
                    break;
                case '0':
                    break;
                default:
                    ErrorInvalidInput();
                    break;
            }
        } while (option != '0' && !flag);
        
    }

    private bool FinilizeOrder(Cart cart)
    {
        Console.WriteLine("Enter the name of the costumer (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return false;
        }

        Console.WriteLine("Enter the email address of the costumer (string):");
        string? email = Console.ReadLine() ?? string.Empty;
        if (email == string.Empty)
        {
            ErrorInvalidInput();
            return false;
        }

        Console.WriteLine("Enter the address of the the costumer (string):");
        string? address = Console.ReadLine() ?? string.Empty;
        if (address == string.Empty)
        {
            ErrorInvalidInput();
            return false;
        }
        try
        {
            cart.CostumerName = name;
            cart.CostumerAddress = address;
            cart.CostumerEmail = email;
            bl.Cart.SetFinalizeOrderForUsers(cart);
            return true;
        }
        catch (OutOfStockException)
        {

            Console.WriteLine("We weren't able to finilized the order because one of the prodcut is not availble");
        }
        catch (ObjectNotFoundException)
        {

            Console.WriteLine("We weren't able to finilized the order because we couldn't find one of the product");
        }
        catch (NullReferenceException)
        {

            Console.WriteLine("The name and\\or email and\\or address is empty");
        }
        catch(EmailNotValidException)
        {
            Console.WriteLine("The email address you gave is not valid");
        }
        catch(ArgumentNullException) 
        {
            Console.WriteLine("One of the parms was null");
        }
        return false;
    }

    private Cart AddNewAmountOfProduct(Cart cart)
    {
        Console.WriteLine("Enter the ID of the product (int) you want to change in the cart to the cart");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return cart;
        }
        Console.WriteLine("Enter the new amount (int) of the product");
        _ = int.TryParse(Console.ReadLine(), out int amount);
        if (amount < 0)
        {
            ErrorInvalidInput();
            return cart;
        }
        try
        {
            cart = bl.Cart.SetAddAmountOfProductForUsers(cart, id, amount) ?? throw new NullReferenceException("function returned a null cart");
        }
        catch(ObjectNotFoundException) {
            Console.WriteLine("We weren't able to change the amount of product in the cart because we couldn't find the one of the product");
        }
        catch (OutOfStockException)
        {
            Console.WriteLine("We weren't able to change the amount of product in the cart because one the product is out of stock");
        }
        return cart;
    }

    private Cart AddProductToCart(Cart cart)
    {
        Console.WriteLine("Enter the ID of the product (int) you want to add to the cart");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return cart;
        }
        try
        {
            cart = bl.Cart.SetAddProductForUsers(cart, id) ?? throw new NullReferenceException("function returned a null cart");
        }
        catch (OutOfStockException)
        {

            Console.WriteLine("We weren't able to add the product to the cart because we couldn't find the prodcut");
        }
        catch (ArgumentNullException) 
        {
            Console.WriteLine("One of the params was null");
        }
        return cart;
    }

    private void PrintMenuCart()
    {
        Console.WriteLine("\nEnter 0 to exit the Cart testing");
        Console.WriteLine("Enter 1 to add a product to the cart");
        Console.WriteLine("Enter 2 to change the amount of the prodcut in the cart");
        Console.WriteLine("Enter 3 finilize the order\n");
    }

    private void TestOrder()
    {
        char option;

        do
        {
            PrintMenuOrder();
            bool flag = char.TryParse(Console.ReadLine(), out option);
            if (!flag)
            {
                ErrorInvalidInput();
                return;
            }
            switch (option)
            {
                case '1':
                    GetOrder();
                    break;
                case '2':
                    GetOrderForList();
                    break;
                case '3':
                    SendOrderToClient();
                    break;
                case '4':
                    UpdateDeliveryDate();
                    break;
                case '0':
                    break;
                default:
                    ErrorInvalidInput();
                    break;
            }
        } while (option != '0');
    }

    private void UpdateDeliveryDate()
    {
        Console.WriteLine("Enter the ID of the Order you want to send to the client (int):");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            BO.Order order = bl.Order.SetSendOrderDeliveredForAdmin(id) ?? throw new NullReferenceException("function return a null object");
            Console.WriteLine("Succsess Order was deliverd to the client");
            Console.WriteLine(order);
        }
        catch (ObjectNotFoundException )
        {
            Console.WriteLine("We weren't able to update the delivery date of the product because we couldn't find the order");
        }
    }

    private void SendOrderToClient()
    {
        Console.WriteLine("Enter the ID of the Order you want to send to the client (int):");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            BO.Order order = bl.Order.SetSendOrderForAdmin(id) ?? throw new NullReferenceException("function return a null object");;
            Console.WriteLine("Succsess Order was sent to the client");
            Console.WriteLine(order);
        } catch(ObjectNotFoundException) 
        {
            Console.WriteLine("We weren't able to send the order to the client because we couldn't find the order");
        }
    }

    private void GetOrderForList()
    {
        List<BO.OrderForList?> orderForLists = new(bl.Order.GetOrdersListForAdmin());
        foreach (var item in orderForLists)
        {
            Console.WriteLine(item);
        }
    }

    private void GetOrder()
    {
        Console.WriteLine("Enter the ID of the Order you want to get (int):");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            BO.Order order = bl.Order.GetOrder(id) ?? throw new ObjectNotFoundException("from here");
            Console.WriteLine(order);
        }
        catch (ObjectNotFoundException)
        {
            Console.WriteLine("We weren't able to get the order because we coudn't find the order");
        }
        
    }

    private void PrintMenuOrder()
    {
        Console.WriteLine("\nEnter 0 to exit Order Testing");
        Console.WriteLine("Enter 1 to get an Order");
        Console.WriteLine("Enter 2 to get Order for list");
        Console.WriteLine("Enter 3 to send the Order to the client");
        Console.WriteLine("Enter 4 to update the arrival of the Order at the client\n");
    }

    private void TestProduct()
    {
        char option;

        do
        {
            PrintMenuProduct();
            bool flag = char.TryParse(Console.ReadLine(), out option);
            if (!flag)
            {
                ErrorInvalidInput();
                return;
            }
            switch (option)
            {
                case '1':
                    GetProduct();
                    break;
                case '2':
                    GetProductForList();
                    break;
                case '3':
                    GetProductItem();
                    break;
                case '4':
                    AddProduct();
                    break;
                case '5':
                    DeleteProduct();
                    break;
                case '6':
                    UpdateProduct();
                    break;
                case '0':
                    break;
                default:
                    ErrorInvalidInput();
                    break;
            }
        } while (option != '0');
    }
    private void UpdateProduct() {
        Console.WriteLine("Enter the ID of the product (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        Console.WriteLine("Enter a name (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter a category, 1: Laptop, 2: Desktop, 3: Server, 4: Phone, 5: Accessories (int):");
        flag = int.TryParse(Console.ReadLine(), out int ct);
        if (!flag || ct < 1 || ct > 5)
        {
            ErrorInvalidInput();
            return;
        }
        ProductCategories categories = (ProductCategories)(ct - 1);

        Console.WriteLine("Enter a price (double):");
        flag = double.TryParse(Console.ReadLine(), out double price);
        if (!flag || price < 0)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the number of item in stock (int):");
        flag = int.TryParse(Console.ReadLine(), out int amount);
        if (!flag || amount < 0)
        {
            ErrorInvalidInput();
            return;
        }


        BO.Product prod = new() { ID = id, Categorie = categories, InStock = amount, Name = name, Price = price };
        try
        {
            bl.Product.SetUpdateProductForAdmin(prod);
            Console.WriteLine("Product was updated");
            Console.WriteLine(prod);
        }
        catch (ObjectNotFoundException)
        {
            Console.WriteLine("We weren't able to update the product because we couldn't find the product");
        }
        catch (OutOfReachIDException)
        {
            Console.WriteLine("We weren't able to update the product because the id isn't valid");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("The name is empty");
        }
        catch (NegativePriceException)
        {
            Console.WriteLine("The price is negative");
        }
        catch (NegativeAmountException)
        {
            Console.WriteLine("The amount of product is negative");
        }
    }

    private void DeleteProduct()
    {
        Console.WriteLine("Enter the ID of the product (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            bl.Product.SetDeleteProductForAdmin(id);
            Console.WriteLine("Product was deleted from the data layer");
        }
        catch (ObjectNotFoundException)
        {
            Console.WriteLine("We weren't able to find the product you are trying to delete");
        }
        catch (NotAbleToDeleteException)
        {
            Console.WriteLine("We weren't able to delete the product becasue the product is apearing in order");
        }
    }

    private void AddProduct()
    {

        Console.WriteLine("Enter the ID of the product (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        Console.WriteLine("Enter a name (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter a category, 1: Laptop, 2: Desktop, 3: Server, 4: Phone, 5: Accessories (int):");
        flag = int.TryParse(Console.ReadLine(), out int ct);
        if (!flag || ct < 1 || ct > 5)
        {
            ErrorInvalidInput();
            return;
        }
        ProductCategories categories = (ProductCategories)(ct - 1);

        Console.WriteLine("Enter a price (double):");
        double price = 0.0d;
        flag = double.TryParse(Console.ReadLine(), out price);
        if (!flag || price < 0)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the number of item in stock (int):");
        int amount;
        flag = int.TryParse(Console.ReadLine(), out amount);
        if (!flag || amount < 0)
        {
            ErrorInvalidInput();
            return;
        }
        

        BO.Product prod = new() {ID = id, Categorie = categories, InStock = amount, Name = name, Price = price};
        try
        {
            bl.Product.SetAddProductForAdmin(prod);
            Console.WriteLine("Product was created");
            Console.WriteLine(prod);
        } 
        catch(DuplicateIDException )
        {
            Console.WriteLine($"There is allready a product with id={id}");
        }
        catch(OutOfReachIDException)
        {
            Console.WriteLine("The id is not a valid id");
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("The name is empty");
        }
        catch(NegativePriceException)
        {
            Console.WriteLine("The price is negative" );
        }
        catch(NegativeAmountException)
        {
            Console.WriteLine("The amount in stock is negative");
        }
    }

    private void GetProductItem()
    {
        Console.WriteLine("Enter the ID of the Order you want to get (int):");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            BO.ProductItem prod = bl.Product.GetProductItemForAdmin(id) ?? throw new ObjectNotFoundException("from here");
            Console.WriteLine(prod);
        }
        catch (ObjectNotFoundException)
        {
            Console.WriteLine("We weren't able to get the product because we couldn't find the product");
        }
    }

    private void GetProductForList()
    {
        List<BO.ProductForList?> prod = new(bl.Product.GetProductForListForAdmin());
        foreach (var item in prod)
        {
            Console.WriteLine(item);
        }
    }

    private void GetProduct()
    {
        Console.WriteLine("Enter the ID of the Order you want to get (int):");
        int id;
        bool flag = int.TryParse(Console.ReadLine(), out id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            BO.Product prod = bl.Product.GetProduct(id) ?? throw new ObjectNotFoundException("from here")
                ;
            Console.WriteLine(prod);
        }
        catch (ObjectNotFoundException)
        {
            Console.WriteLine("We weren't able to get the product because we couldn't find the product");
        }
    }

    private void PrintMenuProduct()
    {
        Console.WriteLine("\nEnter 0 to exit Order Testing");
        Console.WriteLine("Enter 1 to get an product");
        Console.WriteLine("Enter 2 to get all the prdocut in the productForList format");
        Console.WriteLine("Enter 3 to get a prdocut as productItem");
        Console.WriteLine("Enter 4 to add a product to the data");
        Console.WriteLine("Enter 5 to delete a product in the data");
        Console.WriteLine("Enter 6 to update a product in the data\n");
        
    }

    internal void ErrorInvalidInput()
    {
        Console.WriteLine("Error please enter a valid input");
    }

}
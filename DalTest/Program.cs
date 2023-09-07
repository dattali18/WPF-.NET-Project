using DalApi;
using Dal;
using DO;
using System.Xml.Linq;

namespace DalTest;

internal class Program
{
    static void Main(string[] args)
    {
        //config.initData();
        Testing _ = new();
    }
}

internal class Testing
{
    private readonly IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");


    public Testing()
    {
        LoopMenu();
    }

    internal static void PrintMenu()
    {
        Console.WriteLine("Enter 0 to exit");
        Console.WriteLine("Enter 1 to test the products entity");
        Console.WriteLine("Enter 2 to test the Order entity");
        Console.WriteLine("Enter 3 to test the OrderItem entity");
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
                    TestOrderItem();
                    break;
                default:
                    ErrorInvalidInput();
                    break;
            }

        } while (choice != 0);
    }

    internal static void PrintMenuProduct()
    {
        Console.WriteLine("Enter a to create a new product");
        Console.WriteLine("Enter b to print a product");
        Console.WriteLine("Enter c to print all product");
        Console.WriteLine("Enter d to update a product");
        Console.WriteLine("Enter e to delete a product");
    }

    internal void TestProduct()
    {
        PrintMenuProduct();

        bool flag = char.TryParse(Console.ReadLine(), out char option);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        switch (option)
        {
            case 'a':
                CreateProduct();
                break;
            case 'b':
                PrintProduct();
                break;
            case 'c':
                PrintAllProducts();
                break;
            case 'd':
                UpdateProduct();
                break;
            case 'e':
                DeleteProduct();
                break;
            default:
                ErrorInvalidInput();
                break;
        }
    }

    // methods for testing the s_Products entity

    internal void CreateProduct()
    {
        Console.WriteLine("Enter a name (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter a category, 1: Laptop, 2: Desktop, 3: Server, 4: Phone, 5: Accessories (int):");
        bool flag = int.TryParse(Console.ReadLine(), out int ct);
        if (!flag || ct < 1 || ct > 5)
        {
            ErrorInvalidInput();
            return;
        }
        Categories categories = (Categories)ct - 1;

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

        int id = dal.Products.Create(name, categories, price, amount);
        Console.WriteLine("ID: {0}", id);
    }

    internal void PrintProduct()
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
            Products? p = dal.Products.Get(item => item?.ID == id);
            if(p == null)
            {
                return;
            }
            Products prod = (Products)p;

            Console.WriteLine(prod);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    internal void PrintAllProducts()
    {
        List<Products?> products = new(dal.Products.GetAll());
        for (int i = 0; i < products.Count; i++)
            Console.WriteLine(products[i]);
    }

    internal void UpdateProduct()
    {
        Console.WriteLine("Enter the ID of the product you want to update (int):");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the parameter for the new Product:");
        Console.WriteLine("Enter a name (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter a category, 1: Laptop, 2: Desktop, 3: Server (int):");
        flag = int.TryParse(Console.ReadLine(), out int ct);
        if (!flag || ct < 1 || ct > 3)
        {
            ErrorInvalidInput();
            return;
        }
        Categories categories = (Categories)ct - 1;

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

        Products prod = new(0, name, categories, price, amount);
        try
        {
            dal.Products.Update(id, prod);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal void DeleteProduct()
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
            dal.Products.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // methods to test the Order entity

    internal static void PrintMenuOrder()
    {
        Console.WriteLine("Enter a to create a new order");
        Console.WriteLine("Enter b to print a order");
        Console.WriteLine("Enter c to print all order");
        Console.WriteLine("Enter d to update a order");
        Console.WriteLine("Enter e to delete a order");
    }

    internal void TestOrder()
    {
        PrintMenuOrder();

        bool flag = char.TryParse(Console.ReadLine(), out char option);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        switch (option)
        {
            case 'a':
                CreateOrder();
                break;
            case 'b':
                PrintOrder();
                break;
            case 'c':
                PrintAllOrder();
                break;
            case 'd':
                UpdateOrder();
                break;
            case 'e':
                DeleteOrder();
                break;
            default:
                ErrorInvalidInput();
                break;
        }
    }


    internal void CreateOrder()
    {
        Console.WriteLine("Enter the name of the costumer (string):");
        string? name = Console.ReadLine() ?? string.Empty;
        if (name == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the email address of the costumer (string):");
        string? email = Console.ReadLine() ?? string.Empty;
        if (email == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the address of the the costumer (string):");
        string? address = Console.ReadLine() ?? string.Empty;
        if (address == string.Empty)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the creating date (datetime):");
        bool flag = DateTime.TryParse(Console.ReadLine(), out DateTime cretingdate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the shipping date (datetime):");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime shipingdate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the delivery date (datetime):");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime deliverydate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        int id = dal.Order.Create(name, email, address, cretingdate, shipingdate, deliverydate);
        Console.WriteLine("ID: {0}", id);
    }

    internal void PrintOrder()
    {
        Console.WriteLine("Enter the ID of the Order (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            Order? o = dal.Order.Get(item => item?.ID == id);
            if (o == null)
                return;
            Order ord = (Order)o;
            Console.WriteLine(ord);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal void PrintAllOrder()
    {
        List<Order?> orders = new(dal.Order.GetAll());
        for (int i = 0; i < orders.Count; i++)
            Console.WriteLine(orders[i]);
    }

    internal void UpdateOrder()
    {
        Console.WriteLine("Enter the ID of the Order (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the name of the costumer (string):");
        string? name = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the email address of the costumer (string):");
        string? email = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the address of the the costumer (string):");
        string? address = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the creating date (datetime):");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime creatingdate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the shipping date (datetime):");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime shipingdate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the delivery date (datetime):");
        flag = DateTime.TryParse(Console.ReadLine(), out DateTime deliverydate);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        Order ord = new(0, name, email, address, creatingdate, shipingdate, deliverydate);

        try
        {
            dal.Order.Update(id, ord);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


    }

    internal void DeleteOrder()
    {
        Console.WriteLine("Enter the ID of the Order (int)");
        bool flag = int.TryParse(Console.ReadLine(), out int id);
        if (!flag || id < 100000 || id > 1000000)
        {
            ErrorInvalidInput();
            return;
        }
        try
        {
            dal.Order.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // methods for testing the OrederItem entity



    internal static void PrintMenuOrderItem()
    {
        Console.WriteLine("Enter a to create a new order item");
        Console.WriteLine("Enter b to print a order item");
        Console.WriteLine("Enter c to print all order item");
        Console.WriteLine("Enter d to update a order item");
        Console.WriteLine("Enter e to delete a order item");
    }

    internal void TestOrderItem()
    {
        PrintMenuOrderItem();

        bool flag = char.TryParse(Console.ReadLine(), out char option);
        if (!flag)
        {
            ErrorInvalidInput();
            return;
        }

        switch (option)
        {
            case 'a':
                CreateOrderItem();
                break;
            case 'b':
                PrintOrderItem();
                break;
            case 'c':
                PrintAllOrderItem();
                break;
            case 'd':
                UpdateOrderItem();
                break;
            case 'e':
                DeleteOrderItem();
                break;
            default:
                ErrorInvalidInput();
                break;
        }
    }

    internal void CreateOrderItem()
    {
        Console.WriteLine("Enter the id of the order (int):");
        bool flag = int.TryParse(Console.ReadLine(), out int orderId);
        if (!flag || orderId < 100000 || orderId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }



        Console.WriteLine("Enter the id of the order (int):");
        flag = int.TryParse(Console.ReadLine(), out int prodId);
        if (!flag || prodId < 100000 || prodId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        Order ord = new();
        Products prod = new();

        try
        {
            Order? o = dal.Order.Get( item => item?.ID == orderId);
            Products? p = dal.Products.Get(item => item?.ID == prodId);
            if (o == null || p == null)
                return;
            ord = (Order)o;
            prod = (Products)p;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("Enter the amount of item you want to order:");
        flag = int.TryParse(Console.ReadLine(), out int amount);
        if (!flag || amount < 0)
        {
            ErrorInvalidInput();
            return;
        }

        dal.OrderItem.Create(prod, ord, amount);
    }

    internal void PrintOrderItem()
    {
        Console.WriteLine("Enter the id of the order (int):");
        bool flag = int.TryParse(Console.ReadLine(), out int orderId);
        if (!flag || orderId < 100000 || orderId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }



        Console.WriteLine("Enter the id of the order (int):");
        flag = int.TryParse(Console.ReadLine(), out int prdoId);
        if (!flag || prdoId < 100000 || prdoId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        try
        {
            OrderItem? oi = dal.OrderItem.Get(item => item?.OrederID == orderId && item?.ProductID == prdoId);
            if (oi == null)
                return;
            OrderItem ordItm = (OrderItem) oi;
            Console.WriteLine(ordItm);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal void PrintAllOrderItem()
    {
        List<OrderItem?> orderItems = new(dal.OrderItem.GetAll());
        for (int i = 0; i < orderItems.Count; i++)
            Console.WriteLine(orderItems[i]);
    }

    internal void UpdateOrderItem()
    {
        Console.WriteLine("Enter the product id and order id of the orderItem you want to change:");
        Console.WriteLine("Enter the id of the order(int):");
        bool flag = int.TryParse(Console.ReadLine(), out int orderId1);
        if (!flag || orderId1 < 100000 || orderId1 > 1000000)
        {
            ErrorInvalidInput();
            return;
        }



        Console.WriteLine("Enter the id of the order (int):");
        flag = int.TryParse(Console.ReadLine(), out int prodId1);
        if (!flag || prodId1 < 100000 || prodId1 > 1000000)
        {
            ErrorInvalidInput();
            return;
        }


        Console.WriteLine("Enter the data for the new Orderitem:");
        Console.WriteLine("Enter the id of the order(int):");
        flag = int.TryParse(Console.ReadLine(), out int orderId2);
        if (!flag || orderId2 < 100000 || orderId2 > 1000000)
        {
            ErrorInvalidInput();
            return;
        }



        Console.WriteLine("Enter the id of the order (int):");
        flag = int.TryParse(Console.ReadLine(), out int prodId2);
        if (!flag || prodId2 < 100000 || prodId2 > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        Console.WriteLine("Enter the amount of item you want to order:");
        flag = int.TryParse(Console.ReadLine(), out int amount);
        if (!flag || amount < 0)
        {
            ErrorInvalidInput();
            return;
        }

        try
        {
            OrderItem? oi = dal.OrderItem.Get(item => item?.OrederID == orderId1 && item?.ProductID == prodId1);
            Products? p = dal.Products.Get(item => item?.ID == prodId1);
            if (oi == null || p == null)
                return;
            Products p1 = (Products)p;
            OrderItem ordItm = (OrderItem)oi;
            ordItm.ProductID = prodId2;
            ordItm.OrederID = orderId2;
            ordItm.Amount = amount;
            ordItm.Price = amount * p1.Price;
            dal.OrderItem.Update(prodId1, orderId1, ordItm);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    internal void DeleteOrderItem()
    {
        Console.WriteLine("Enter the id of the order (int):");
        bool flag = int.TryParse(Console.ReadLine(), out int orderId);
        if (!flag || orderId < 100000 || orderId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }



        Console.WriteLine("Enter the id of the order (int):");
        flag = int.TryParse(Console.ReadLine(), out int prdoId);
        if (!flag || prdoId < 100000 || prdoId > 1000000)
        {
            ErrorInvalidInput();
            return;
        }

        try
        {
            dal.OrderItem.Delete(prdoId, orderId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal static void ErrorInvalidInput()
    {
        Console.WriteLine("Error please enter a valid input");
    }
}
using BlImplementation;
using BO;
using DalApi;

namespace BlUtils;

public static class Convertor
{
    // methods for internal use
    readonly static IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");

    public static BO.Order? ToBoOrder(DO.Order? ord)
    {
        DO.Order ord1 = ord ?? throw new NullReferenceException("from ToBoOrder");
        BO.Order? boOrder = new()
        {
            ID = ord1.ID,
            CostumerName = new(ord1.CustomerName),
            CostumerEmail = new(ord1.CustomerEmail),
            CostumerAddress = new(ord1.CustomerAdress),
            OrderPlaced = ord1.OrderDate,
            Shipped = ord1.ShipingDate,
            Delivered = ord1.DeliveryDate,
            Status = BO.OrderStatus.Delivered
        };

        List<DO.OrderItem?> lst = new(dal.OrderItem.GetAll(item => item?.OrederID == ord1.ID));
        boOrder.Items = new();
        foreach (var item in lst)
        {
            boOrder.Items.Add(Convertor.ToBoOrderItem(item));

        }

        // checking the status of the Order by looking at the date shipped etc...
        DateTime now = DateTime.Now;
        if (boOrder.Delivered == null || boOrder.Delivered > now)
        {
            if (boOrder.Shipped == null || boOrder.Shipped > now)
            {
                boOrder.Status = BO.OrderStatus.Processing;
            }
            else
            {
                boOrder.Status = BO.OrderStatus.Shipped;
            }
        }
        else
        {
            boOrder.Status = BO.OrderStatus.Delivered;
        }

        return boOrder;
    }

    // internal methods for use
    public static OrderForList? ToBoOrderForList(DO.Order? ord1)
    {
            DO.Order order = ord1 ?? throw new NullReferenceException("from ToBoOrderForList");

            OrderForList ord = new()
            {
                ID = order.ID,
                Name = order.CustomerName
            };

            List<DO.OrderItem?> orditmlist = new(dal.OrderItem.GetAll(item => item?.OrederID == ord.ID));
            ord.Amount = orditmlist.Count;
            double sum = 0.0d;
            foreach (var orditem in orditmlist)
            {
                DO.OrderItem orderItem = orditem ?? new();
                sum += (orderItem.Amount * orderItem.Price);
            }
            ord.TotalPrice = sum;
            Order order1 = ToBoOrder(order) ?? throw new NullReferenceException("from ToBoOrderForList");
            ord.Status = order1.Status;
            return (BO.OrderForList?)ord;
    }

    public static OrderForList? ToBoOrderForList(BO.Order? ord1)
    {
        BO.Order order = ord1 ?? throw new NullReferenceException("from ToBoOrderForList");

        OrderForList ord = new()
        {
            ID = order.ID,
            Name = order.CostumerName
        };

        List<DO.OrderItem?> orditmlist = new(dal.OrderItem.GetAll(item => item?.OrederID == ord.ID));
        ord.Amount = orditmlist.Count;
        double sum = 0.0d;
        foreach (var orditem in orditmlist)
        {
            DO.OrderItem orderItem = orditem ?? new();
            sum += (orderItem.Amount * orderItem.Price);
        }
        ord.TotalPrice = sum;
        //Order order1 = ToBoOrder(order) ?? throw new NullReferenceException("from ToBoOrderForList");
        ord.Status = ord1.Status;
        return (BO.OrderForList?)ord;
    }

    // methods for internal use
    public static OrderItem? ToBoOrderItem(DO.OrderItem? orderItem)
    {
        DO.OrderItem orderItem1 = orderItem ?? throw new NullReferenceException("parameter is null");
        DO.Products products = dal.Products.Get(prod => prod?.ID == orderItem1.ProductID) ?? throw new NullReferenceException("returned null");
        BO.OrderItem orditm = new()
        {
            OrderId = orderItem1.OrederID,
            ProductId = orderItem1.ProductID,
            Price = products.Price,
            Amount = orderItem1.Amount,
            Name = products.Name,
            TotalPrice = orderItem1.Amount * products.Price
        };
        //orditm.TotalPrice = orditm.Amount * orditm.Price;       
        //orditm.Name = products.Name;
        return orditm;
    }

    // methods for internal use
    public static BO.OrderTracking? ToBoOrderTracking(DO.Order? order1)
    {
        DO.Order order = order1 ?? new();
        BO.OrderTracking? orderTracking = new();
        BO.Order boOrder = BlUtils.Convertor.ToBoOrder(order) ?? throw new NullReferenceException("from ToBoOrderTracking");

        orderTracking.ID = boOrder.ID;
        orderTracking.Status = boOrder.Status;
        orderTracking.Values = new()
        {
            (boOrder.OrderPlaced, OrderStatus.OrderPlaced),
            (boOrder.Shipped, OrderStatus.Shipped),
            (boOrder.Delivered, OrderStatus.Delivered)
        };

        return orderTracking;
    }

    // methods for internal use
    /// <summary>
    /// ToBoProduct: take DO.Products and return the object in the BO.Products format
    /// </summary>
    /// <param name="prod">the Products you want to convert</param>
    /// <returns>the Products in the BO.Products format</returns>
    public static Product? ToBoProduct(DO.Products? doProd)
    {
        DO.Products prod1 = doProd ?? throw new NullReferenceException("from ToBoProduct");
        BO.Product prod = new()
        {
            Name = prod1.Name,
            Price = prod1.Price,
            ID = prod1.ID,
            InStock = prod1.InStock,
            Categorie = (ProductCategories)(int)prod1.Categorie
        };

        return (BO.Product?)prod;
    }

    public static BO.ProductForList? ToBoProductForList(DO.Products? prod1)
    {
        DO.Products prod = prod1 ?? new();
        BO.ProductForList productForList = new() { ID = prod.ID, Categories = (ProductCategories)(int)prod.Categorie, Name = prod.Name, Price = prod.Price };
        return (BO.ProductForList?)productForList;
    }

    // for internal use
    public static BO.ProductItem? ToBoPrductItem(DO.Products? prod1)
    {
        DO.Products prod = prod1 ?? new();
        BO.ProductItem item = new()
        {
            ID = prod.ID,
            Amount = prod.InStock,
            InStock = (bool)(prod.InStock > 0),
            Name = prod.Name,
            Price = prod.Price,
            Categories = (ProductCategories)(int)prod.Categorie
        };
        return (BO.ProductItem?)item;
    }

    public static BO.ProductItem? ToBoProductItem(BO.Product? prod)
    {
        BO.Product p = prod ?? new();
        BO.ProductItem? item = new()
        {
            ID = p.ID,
            Amount = 0,
            InStock = (bool)(prod?.InStock > 0),
            Name = p.Name,
            Price = p.Price,
            Categories = p.Categorie
        };
        return item;
    }

    public static BO.ProductItem? ToBoProductItem(BO.Product? prod, BO.Cart? cart)
    {
        BO.Product p = prod ?? new();
        int amount = 0;
        if(cart != null && cart.Items != null)
        {
            var orderItem = cart.Items.FirstOrDefault(item => item?.ProductID == prod?.ID);
            amount = orderItem?.Amount ?? 0;
        }
        BO.ProductItem? item = new()
        {
            ID = p.ID,
            Amount = amount,
            InStock = (bool)(prod?.InStock > 0),
            Name = p.Name,
            Price = p.Price,
            Categories = p.Categorie
        };
        return item;
    }
}

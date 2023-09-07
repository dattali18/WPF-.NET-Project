using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;

namespace PL.Utils;

static class CategoriesConvertor
{
    public static BO.ProductCategories FromTextCategories(string cat) => cat switch
    {
        nameof(BO.ProductCategories.Laptop) => BO.ProductCategories.Laptop,
        nameof(BO.ProductCategories.Desktop) => BO.ProductCategories.Desktop,
        nameof(BO.ProductCategories.Accessories) => BO.ProductCategories.Accessories,
        nameof(BO.ProductCategories.Server) => BO.ProductCategories.Server,
        nameof(BO.ProductCategories.Phone) => BO.ProductCategories.Phone,
        _ => BO.ProductCategories.All
    };
}

static class PriceRange
{
    public static double Get(double value) => value switch
    {
        < 100 => 100,
        >= 100 and < 250 => 250,
        >= 250 and < 500 => 500,
        >= 500 and < 1000 => 1000,
        _ => ((int)(value / 1000)) * 1000
    };
}

static class Data
{
    private readonly static IBl bl = Factory.Get() ?? throw new NullReferenceException("BlApi.Factory.Get() returned null");
    public static IEnumerable<BO.ProductForList?> GetProducts(Func<DO.Products?, bool>? func = null) => bl.Product.GetProductForListForAdmin(func);

    public static BO.Product? GetProduct(int id) => bl.Product.GetProduct(id);

    public static IEnumerable<BO.OrderForList?> GetOrders(Func<DO.Order?, bool>? func = null) => bl.Order.GetOrdersListForAdmin(func);

    public static BO.Order? GetOrder(int id) => bl.Order.GetOrder(id);
}
using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

public class BlProduct : IProduct
{
    readonly IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("DalApi.Factory.Get() returned null");


    public Product? GetProduct(int id)
    {
        if (id < 0)
            throw new OutOfReachIDException("from BlProduct.GetProductForAdmin");
        Product? prod;
        try
        {
            DO.Products doProd = dal.Products.Get(p => p?.ID == id) ?? throw new ObjectNotFoundException("from GetProduct");
            prod = BlUtils.Convertor.ToBoProduct(doProd);
            return prod;
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    public ProductItem? GetProductItemForAdmin(int id)
    {
        DO.Products products = dal.Products.Get(p => p?.ID == id) ?? throw new ObjectNotFoundException("from GetProductItemForAdmin");
        return (BO.ProductItem?)BlUtils.Convertor.ToBoPrductItem(products);
    }

    public IEnumerable<ProductForList?> GetProductForListForAdmin(Func<DO.Products?, bool>? func)
    {
        List<DO.Products?> products = new(dal.Products.GetAll(func));
        List<BO.ProductForList?> prodList = new(
            from prod in products
            where prod != null
            select BlUtils.Convertor.ToBoProductForList(prod)
                                                );
        
        //foreach (var item in products)
        //{
        //    DO.Products prod = item ?? new();
        //    BO.ProductForList? prd = BlUtils.Convertor.ToBoProductForList(prod);
        //    prodList.Add(prd);
        //}
        return prodList;
    }

    public void SetAddProductForAdmin(BO.Product prod)
    {
        if (prod.ID < 100000 || prod.ID > 1000000)
        {
            throw new OutOfReachIDException("from BlProduct.SetAddProductForAdmin");
        }
        else if (prod.Name == string.Empty)
        {
            throw new NullReferenceException("from BlProduct.SetAddProductForAdmin");
        }
        else if (prod.Price <= 0)
        {
            throw new NegativePriceException("from BlProduct.SetAddProductForAdmin");
        }
        else if(prod.InStock < 0)
        {
            throw new NegativeAmountException("from BlProduct.SetAddProductForAdmin");
        }
        try
        {
            DO.Products p1 = new(prod.ID, prod.Name, (DO.Categories)(int)prod.Categorie, prod.Price, prod.InStock);
            dal.Products.Add(p1);
        }
        catch (DuplicateIDException e)
        {
            throw e;
        }
    }

    /// <summary>
    /// This function delete a Products
    /// </summary>
    /// <param name="id"> pruduct's id to delete </param>
    /// <exception cref="DeleteUnexistItemException"></exception>
    /// <exception cref="DeleteProductFromOrderExceprion"></exception>
    public void SetDeleteProductForAdmin(int id)
    {
        // If the Products exist
        List<DO.Products?> products = new(dal.Products.GetAll());
        bool exist = products.Any(prod => prod?.ID == id);

        //foreach (DO.Products? p in products)
        //{
        //    DO.Products prod = p ?? new();
        //    if (prod.ID == id)
        //    {
        //        exist = true; 
        //        break;
        //    }
        //}
        
        if (!exist)
        {
            throw new ObjectNotFoundException("from BlProduct.SetDeleteProductForAdmin");
        }

        // If there are orders with the wanted Products
        List<DO.OrderItem?> orderItems = new(dal.OrderItem.GetAll());
        if(orderItems.Any(orderItem => orderItem?.ProductID == id))
            throw new NotAbleToDeleteException("from BlProduct.SetDeleteProductForAdmin");
        //foreach (DO.OrderItem? item in orderItems)
        //{
        //    DO.OrderItem orderItem = item ?? new();
        //    if (orderItem.ProductID == id)
        //    {
        //        throw new NotAbleToDeleteException("from BlProduct.SetDeleteProductForAdmin");
        //    }
        //}

        try
        {
            dal.Products.Delete(id);
        }
        catch (DuplicateIDException e)
        {
            throw e;
        }
    }

    public void SetUpdateProductForAdmin(BO.Product prod)
    {
        if (prod.ID < 100000 || prod.ID > 1000000)
        {
            throw new OutOfReachIDException("from BlProduct.SetAddProductForAdmin");
        }
        else if (prod.Name == string.Empty)
        {
            throw new NullReferenceException("from BlProduct.SetAddProductForAdmin");
        }
        else if (prod.Price <= 0)
        {
            throw new NegativePriceException("from BlProduct.SetAddProductForAdmin");
        }
        else if (prod.InStock < 0)
        {
            throw new NegativeAmountException("from BlProduct.SetAddProductForAdmin");
        }

        try
        {
            DO.Products p1 = new(prod.ID, prod.Name, (DO.Categories)(int)prod.Categorie, prod.Price, prod.InStock);
            DO.Products p2 = dal.Products.Get(p => p?.ID == prod.ID) ?? throw new ObjectNotFoundException("from GetProductItemForAdmin");
            dal.Products.Update(p2, p1);
        }
        catch (ObjectNotFoundException e)
        {
            throw e;
        }
    }

    public IEnumerable<Product?> GetProductListForAdmin(Func<Products?, bool>? func = null)
    {
        var list  = from prod in dal.Products.GetAll(func)
                    where prod != null
                    select BlUtils.Convertor.ToBoProduct(prod);
        return list;
    }
}

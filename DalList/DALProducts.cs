using DO;
using DalApi;

namespace Dal;

public class DALProducts : IProducts
{
    /// <summary>
    /// Create: create a new DO.Products in the Data accsess layer (DAL)
    /// </summary>
    /// <param name="name">the name of the Products</param>
    /// <param name="categorie">the categorie of the Products</param>
    /// <param name="price">the price of the Products</param>
    /// <param name="inStock">the amount of item in stock</param>
    /// <returns>the id of the current Products</returns>
    public int Create(string? name, Categories categorie, double price, int inStock)
    {
        int ID = DataSource.Confige.LastProductID;
        if(name == null)
            throw new ArgumentNullException(nameof(name));
        Products prod = new(ID, name, categorie, price, inStock);
        DataSource.AddProduct(prod);
        return ID;
    }


    /// <summary>
    /// Update: receive an id with another prod and update the Products with ID=id with the info from prod
    /// </summary>
    /// <param name="ID">int value of id</param>
    /// <param name="prod">a Products with the value you want to update</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(int ID, Products prod)
    {
        if (DataSource.SearchProduct(ID) == null)
            throw new ObjectNotFoundException("product was not found");
        DataSource.s_Products.RemoveAll(prod => prod?.ID == ID);
        DataSource.s_Products.Add(prod);
        //int idx = DataSource.SearchProduct(ID);
        //if (idx != -1)
        //{
        //    DataSource.s_Products[idx] = prod;
        //}
        //else
        //{
        //    throw new ObjectNotFoundException("from DALProduct.Update");
        //}
    }

    /// <summary>
    /// Delete: receive an id and delete it from the DAL
    /// </summary>
    /// <param name="ID">int value of id</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Delete(int ID)
    {
        if (DataSource.SearchProduct(ID) == null)
            throw new ObjectNotFoundException("from DALProduct.Delete");
        DataSource.s_Products.Remove(DataSource.SearchProduct(ID));
    }

    /// <summary>
    /// Add: receive a Products update and try to add it to the DAL
    /// </summary>
    /// <param name="t"></param>
    /// <returns>the id of the Products</returns>
    /// /// <exception cref="DuplicateIDException"></exception>
    public int Add(Products t)
    {
       if (DataSource.SearchProduct(t.ID) != null)
            throw new DuplicateIDException("from DALProduct.Add");
        DataSource.AddProduct(t);
        return t.ID;
    }

    /// <summary>
    /// Update: receive two DO.Products object and take the info from t2 and put's it into t1 (wich should be in DAL)
    /// </summary>
    /// <param name="t1">the DO.Pruduct to update</param>
    /// <param name="t2">the DO.Products containing the data</param>
    /// <exception cref="ObjectNotFoundException"></exception>
    public void Update(Products t1, Products t2)
    {
        if (DataSource.SearchProduct(t1.ID) == null)
            throw new ObjectNotFoundException("product was not found");
        DataSource.s_Products.RemoveAll(prod => prod?.ID == t1.ID);
        DataSource.s_Products.Add(t2);
    }
    //{
    //    int i = DataSource.SearchProduct(t1.ID);
    //    if (i == -1)
    //    {
    //        throw new ObjectNotFoundException("from DALProducts.Update");
    //    }
    //    t2.ID = t1.ID;
    //    DataSource.s_Products[i] = t2;
    //}

    /// <summary>
    /// Delete: receive an object t and try to delete it from DAL
    /// </summary>
    /// <param name="t">the object to delete</param>
    /// <returns>return true if the deletion was successful and flase if the Products didn't exist or the deletiong didn't work</returns>
    public bool Delete(Products t) => DataSource.s_Products.Remove(t);
    //{
    //    return DataSource.s_Products.Remove(t);
    //}


    public Products? Get(Predicate<Products?> func) => DataSource.s_Products?.FirstOrDefault(prod => func(prod));
    //{
    //    foreach (var item in DataSource.s_Products)
    //    {
    //        if (item == null) continue;
    //        if (func(item) == true)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}

    /// <summary>
    /// GetAll: return all the DO.Products in DAL
    /// </summary>
    /// <returns>a list of all the product in DAL</returns>
    public IEnumerable<Products?> GetAll(Func<Products?, bool>? func = null) => (func == null ? DataSource.s_Products?.Select(prod => prod) :
        DataSource.s_Products?.Where(prod => func(prod)) ?? throw new ObjectNotFoundException("prod not found"))
        ?? throw new ObjectNotFoundException("the prod list wasn't found");
    //{
    //    if(func == null)
    //        return DataSource.s_Products;
    //    List<Products?> list = new();
    //    foreach (var item in DataSource.s_Products)
    //    {
    //        if (item == null) continue;
    //        if(func(item) == true)
    //        {
    //            list.Add(item);
    //        }
    //    }
    //    return list;
    //}


    public void Update(Products t)
    {
        if (DataSource.SearchProduct(t.ID) == null)
            throw new ObjectNotFoundException("product was not found");
        DataSource.s_Products.RemoveAll(prod => prod?.ID == t.ID);
        DataSource.s_Products.Add(t);
    }
}

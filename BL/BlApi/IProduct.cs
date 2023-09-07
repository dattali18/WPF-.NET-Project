using BO;

namespace BlApi;

/// <summary>
/// IProduct: the interface for the Logic Entity Products
/// </summary>
public interface IProduct
{


    /// <summary>
    /// GetProductForListForAdmin: getting the list of Products (for admin - only accessible to the admin)
    /// </summary>
    /// <returns>a IEnumerable<ProductForList> of all the ProductForList</returns>
    public IEnumerable<ProductForList?> GetProductForListForAdmin(Func<DO.Products?, bool>? func = null);


    /// <summary>
    /// GetProductForListForAdmin: getting the list of Products (for admin - only accessible to the admin)
    /// </summary>
    /// <returns>a IEnumerable<Product> of all the ProductForList</returns>
    public IEnumerable<Product?> GetProductListForAdmin(Func<DO.Products?, bool>? func = null);

    /// <summary>
    /// GetProductItemForAdmin: getting the Products in a ProductItem format
    /// </summary>
    /// <param name="id">the id for the Products you want to get</param>
    /// <returns></returns>
    public ProductItem? GetProductItemForAdmin(int id);

    /// <summary>
    /// GetProduct: getting the Products with the ID of id (for admin - only accessible to the admin)
    /// </summary>
    /// <param name="id">the id for the Products you want to get</param>
    /// <returns>a s_Products with ID = id</returns>
    public Product? GetProduct(int id);

    /// <summary>
    /// SetDeleteProductForAdmin: deleting a Products in the Products (Data Entity) (for admin - only accessible to the admin)
    /// </summary>
    /// <param name="id">the id of the Products you want to delete</param>
    public void SetDeleteProductForAdmin(int id);

    /// <summary>
    /// SetAddProductForAdmin: adding a Products to the data sourch
    /// </summary>
    /// <param name="prod">the Products with the values and data for creating the new Products</param>
    public void SetAddProductForAdmin(BO.Product prod);


    // Bonus
    /// <summary>
    /// SetUpdateProductForAdmin: update a Products in the Products (Data Entity) (for admin - only accessible to the admin)
    /// </summary>
    /// <param name="prod">the Products you want to Update</param>
    public void SetUpdateProductForAdmin(BO.Product prod);
}

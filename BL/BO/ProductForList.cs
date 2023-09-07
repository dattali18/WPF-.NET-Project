
namespace BO;

public class ProductForList
{
    /// <summary>
    /// ID: the id of the Products
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name: the name of the Products
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price: the price of the Products
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Categories: the categories of the Products such as Laptop etc...
    /// </summary>
    public ProductCategories Categories { get; set; }

    // Methods

    public override string ToString()
    {
        return $@"
ID            {ID}
Name:         {Name}
Price:        {Price}
Categories:   {Categories}";
    }

}

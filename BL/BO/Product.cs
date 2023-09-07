namespace BO;

public class Product
{
    /// <summary>
    /// ID: the identifying number for each Products
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name: the identifying string for each Products
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price: the price of the Products
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Categorie: the categorie of the Products such as laptop etc...
    /// </summary>
    public ProductCategories Categorie { get; set; }

    /// <summary>
    /// InStock: the amount of s_Products in stock in the store
    /// </summary>
    public int InStock { get; set; }


    // Methods

    public override string ToString()
    {
        return $@"
ID:           {ID}
Name:         {Name}
Price:        {Price}
Categorie:    {Categorie}                
InStock:      {InStock}";
    }
}

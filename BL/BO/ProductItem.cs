namespace BO;

public class ProductItem
{
    /// <summary>
    /// ID: the number to identify the Products
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
    /// Categories: the categorie of the Products such as Laptop etc...
    /// </summary>
    public ProductCategories Categories { get; set; }

    /// <summary>
    /// InStock: is the Products in stock (true:yes/false:no)
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// Amount: the amount of the Products in the cart
    /// </summary>
    public int Amount { get; set; }

    // Methods

    public override string ToString()
    {
        string s1 =  $@"
ID:           {ID}
Name:         {Name}
Price:        {Price}
Categories:   {Categories}
Amount:       {Amount}";
        s1 = s1 + "\nInStock:   " + ((InStock) ? "    yes" : "    no");
        return s1;
    }
}

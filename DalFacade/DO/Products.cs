namespace DO;

/// <summary>
/// this is the structure for the Products data
/// </summary>

public struct Products
{
    /// <summary>
    ///  ID: identify a unique Products
    /// </summary>
    public int ID { get; set; }


    /// <summary>
    /// Name: to identify the Products
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Categorie: such as computer, laptop etc...
    /// </summary>
    public Categories? Categorie { get; set; }


    /// <summary>
    /// Price: of the Products
    /// </summary>
    public double Price { get; set; }


    /// <summary>
    /// InStock: the amount of item in stock
    /// </summary>
    public int InStock { get; set; }


    // methods override

    /// <summary>
    /// return a string represention of the structure Products
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $@"
Product ID:       {ID}
Name:             {Name}
Categories:       {Categorie}
Price:            {Price}
Amount in stock:  {InStock}";
    }

    // copy ctor
    public Products(Products prod)
    {
        ID = prod.ID;
        Name = prod.Name;
        Categorie = prod.Categorie;
        Price = prod.Price;
        InStock = prod.InStock;
    }

    public Products(int id, string name, Categories categorie, double price, int inStock)
    {
        ID = id;
        Name = new(name);
        Categorie = categorie;
        Price = price;
        InStock = inStock;
    }

}


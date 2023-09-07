using DO;

namespace DalApi;

public interface IProducts : ICRUD<Products>
{ 

    int Create(string? name, Categories categorie, double price, int inStock);

    void Update(int ID, Products prod);

    void Delete(int ID);
}

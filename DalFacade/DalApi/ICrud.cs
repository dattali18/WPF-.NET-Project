namespace DalApi;


public interface ICRUD<T> where T : struct
{
    public int Add(T t);

    public void Update(T t1, T t2);

    public void Update(T t);

    public bool Delete(T t);

    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);

    public T? Get(Predicate<T?> func);
}

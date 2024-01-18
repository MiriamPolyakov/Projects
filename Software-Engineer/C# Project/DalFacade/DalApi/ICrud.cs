
namespace DalApi;
/// <summary>
/// Defining a template for all data entity classes
/// </summary>
/// <typeparam name="T">data entity type</typeparam>
public interface ICrud<T>
{
    public int Add(T obj);
    public void Delete(int id);
    public void Update(T obj);
    public IEnumerable<T?> Get(Func<T?, bool>? func = null);
    public T? GetSingle(Func<T?, bool>? func );
}

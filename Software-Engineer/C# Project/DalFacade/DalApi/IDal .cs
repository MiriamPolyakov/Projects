
namespace DalApi;
/// <summary>
/// Defining variables that allow access to pure classes of various data entities
/// </summary>
public interface IDal
{
    public IOrderItem OrderItem { get;  }
    public IOrder Order { get; }
    public IProduct Product { get; }
}


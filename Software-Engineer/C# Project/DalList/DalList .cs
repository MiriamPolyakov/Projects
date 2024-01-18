using DalApi;
namespace Dal;
/// <summary>
/// Defining variables that allow access to the different data layers
/// </summary>
sealed internal class DalList : IDal
{
    private static Lazy<IDal> instance = new Lazy<IDal>(() => new DalList());

    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get => instance.Value; }
    private DalList()
    {
        lock (instance)
        {
            if (instance.Value == null)
            {
                instance = new Lazy<IDal>();
            }
        }
    }
}

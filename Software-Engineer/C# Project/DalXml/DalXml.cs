using DalApi;

namespace Dal;

sealed internal class DalXml : IDal
{
    private DalXml()
    {
    }
    public static IDal Instance { get; } = new DalXml();
    public IOrder Order { get; } = new Order();
    public IProduct Product { get; } = new Product();
    public IOrderItem OrderItem { get; } = new OrderItem();

    



}

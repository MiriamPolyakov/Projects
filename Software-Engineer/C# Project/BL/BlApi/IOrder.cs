
namespace BlApi;
/// <summary>
/// The template of functions required from the order-logical entity
/// </summary>
public interface IOrder
{
    public IEnumerable<BO.OrderForList?>? Get();
    public BO.Order? Get(int id);
    public BO.Order? UpdateDelivery(int id);
    public BO.Order? UpdateShipping(int id);
    public void UpdateOrderAmountByManager(int id,int amount,string str);
    public BO.Order UpdateOrder(BO.Order order);
    public int GetOldestOrder();
}


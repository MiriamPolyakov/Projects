using DO;
using DalApi;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Dal;
/// <summary>
/// order-item functions
/// </summary>
internal class DalOrderItem : IOrderItem
{

    /// <summary>
    ///adding an order-item
    /// </summary>
    /// <param name="orderItem">order item to add</param>
    /// <returns>id of order item added</returns>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem orderItem)
    {
        orderItem.OrderItemID = DataSours.Config.OrderItemID;
        int idx = DataSours.OrderItemArr.FindIndex(o => o.ProductID == orderItem.ProductID);
        if (idx == -1)
            throw new EntityNotFoundException("ERROR---the product ID you entered in order to add an order item dosn't exist");
        idx = DataSours.OrderArr.FindIndex(o => o.ID == orderItem.OrderID);
        if (idx == -1)
            throw new EntityNotFoundException("ERROR---the order ID you in order to add an order item entered dosn't exist");
        DataSours.OrderItemArr.Add(orderItem);
        return orderItem.OrderItemID;
    }
    /// <summary>
    /// get order items filtering by func or all order items
    /// </summary>
    /// <param name="func">filtering func</param>
    /// <returns>order items filtered by func or all order items</returns>
    /// <exception cref="EntityNotFoundException">order dos not exist in all order item</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> Get(Func<OrderItem, bool>? func)
    {
        if (func == null)
            return DataSours.OrderItemArr;
        List<OrderItem> order_items = DataSours.OrderItemArr.Where(func).ToList();
        if (order_items.Count == 0)
            throw new EntityNotFoundException("ERROR---this order dos not exist in all order item");
        return order_items;
    }


    /// <summary>
    ///updating an order-item's details
    /// </summary>
    /// <param name="orderItem">order item to update</param>
    /// <exception cref="EntityNotFoundException"> no order item with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem orderItem)
    {
        int i = DataSours.OrderItemArr.FindIndex(oi => oi.OrderItemID == orderItem.OrderItemID);
        if (i == -1)
            throw new EntityNotFoundException("ERROR---there is no order item with this ID");
        DataSours.OrderItemArr[i] = orderItem;
    }

    /// <summary>
    ///deleting an order-item
    /// </summary>
    /// <param name="id">id of order item to delete</param>
    /// <exception cref="EntityNotFoundException">no order item with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        int i = DataSours.OrderItemArr.FindIndex(oi => oi.OrderItemID == id);
        if (i == -1)
            throw new EntityNotFoundException("ERROR---there is no order item with this ID");
        DataSours.OrderItemArr.Remove(DataSours.OrderItemArr[i]);
    }

    /// <summary>
    ///reading a specific order-item according to which order-item id and which order it was ordered in 
    /// </summary>
    /// <param name="func">filtering the specific order item by func</param>
    /// <returns>found order item</returns>
    /// <exception cref="EntityNotFoundException">order item dos not exist</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetSingle(Func<OrderItem, bool> func)
    {
        OrderItem? orderItem = DataSours.OrderItemArr.Where(func).ToList()[0];
        if (orderItem == null)
            throw new EntityNotFoundException("ERROR---this order item dos not exist");
        return (OrderItem)orderItem;
    }
}

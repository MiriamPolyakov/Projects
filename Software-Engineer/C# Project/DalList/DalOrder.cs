using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal;
/// <summary>
/// order functions
/// </summary>
internal class DalOrder : IOrder
{

    /// <summary>
    ///adding an order
    /// </summary>
    /// <param name="order"> order to add</param>
    /// <returns>id of oredr added</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        order.ID = DataSours.Config.OrderID;
        DataSours.OrderArr.Add(order);
        return order.ID;
    }

    /// <summary>
    ///reading all orders or filtered orders
    /// </summary>
    /// <param name="func">filtering func<</param>
    /// <returns>orders filtered by func or all orders</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> Get(Func<Order, bool>? func = null)
    {
        if (func == null)
            return DataSours.OrderArr;
        return DataSours.OrderArr.Where(func).ToList();
    }

    /// <summary>
    ///updating an order's deatails
    /// </summary>
    /// <param name="order">the update order</param>
    /// <exception cref="EntityNotFoundException">no order with this ID</exception>
    
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        int i = DataSours.OrderArr.FindIndex(o => o.ID == order.ID);
        if (i == -1)
            throw new EntityNotFoundException("ERROR---there is no order with this ID");
        DataSours.OrderArr[i] = order;
    }
   
    /// <summary>
    ///deleting an order
    /// </summary>
    /// <param name="id">id of order to delete</param>
    /// <exception cref="EntityNotFoundException">no order with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        int idx = DataSours.OrderArr.FindIndex(o => o.ID == id);
        if (idx == -1)
            throw new EntityNotFoundException("ERROR---there is no order with this ID");
        DataSours.OrderArr.Remove(DataSours.OrderArr[idx]);
    }
    /// <summary>
    /// get single order
    /// </summary>
    /// <param name="func">filtering func<</param>
    /// <returns>founded order</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetSingle(Func<Order, bool> func)
    {
        return DataSours.OrderArr.Where(func).ToList()[0];
    }
}



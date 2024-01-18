using BO;
using Dal;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
/// <summary>
/// function of oredr in bl
/// </summary>
internal class BlOrder : BlApi.IOrder
{
    private IDal? Dal = DalApi.Factory.Get();
    private void checkValidation(BO.Order order)
    {
        if (order.CustomerName == "")
            throw new BlEntityInvalidException("Name cant be empty");
        if (order.CustomerEmail == "")
            throw new BlEntityInvalidException("Email cant be empty");
        if (order.CustomerAdress == "")
            throw new BlEntityInvalidException("Adress cant be empty");
        if (order.TotalPrice < 0)
            throw new BlEntityInvalidException("total price cant be less than 0");
    }
    private DO.Order convertBoOrderToDoOrder(BO.Order b_order)
    {
        object d_order = new DO.Order();
        d_order.GetType().GetProperties().Select(prop =>
        {
            prop.SetValue(d_order, b_order.GetType().GetProperty(prop.Name)?.GetValue(b_order));
            return prop;
        }).ToList();
        return (DO.Order)d_order;
    }
    private BO.Order convertDoOrderToBoOrder(DO.Order d_order, int id)
    {
        BO.Order b_order = new();
        b_order.GetType().GetProperties().Where(prop => (prop.Name != "Status" && prop.Name != "Items" && prop.Name != "TotalPrice")).Select(prop =>
        {
            prop.SetValue(b_order, d_order.GetType().GetProperty(prop.Name)?.GetValue(d_order));
            return prop;
        }).ToList();
        (List<BO.OrderItem> ls, double totalPrice) = getItemsf(id);
        b_order.Items = ls;
        b_order.TotalPrice = totalPrice;
        if (b_order.ShipDate <= DateTime.Now)
            b_order.Status = BO.Enums.eOrderStatus.shipped;
        else
            b_order.Status = BO.Enums.eOrderStatus.ordered;
        if (b_order.DeliveryDate <= DateTime.Now)
            b_order.Status = BO.Enums.eOrderStatus.received;
        return (BO.Order)b_order;
    }

    public BO.Order UpdateOrder(BO.Order order)
    {
        checkValidation(order);
        DO.Order d_order = convertBoOrderToDoOrder(order);
        Dal.Order.Update(d_order);
        return Get(order.ID);

    }

    /// <summary>
    /// get all Order For List
    /// </summary>
    /// <returns>all Order For List</returns>
    /// <exception cref="BlEntityNotFoundExcueption">ID was not found</exception>

    public IEnumerable<BO.OrderForList?>? Get()
    {
        try
        {
            List<DO.Order> orders = Dal.Order.Get().ToList();
            var orderList =
                from order in orders
                let BOorder = Dal.OrderItem.Get(o => o.OrderID == order.ID).ToList()
                select new BO.OrderForList()
                {
                    ID = order.ID,
                    CustomerName = order.CustomerName,
                    AmountOfItems = BOorder == null ? 0 : BOorder.Count,
                    TotalPrice = BOorder == null ? 0 : BOorder.Sum(o => o.Price * o.Amount),
                    Status = (order.DeliveryDate < DateTime.Now) ? BO.Enums.eOrderStatus.received :
                    (order.ShipDate < DateTime.Now) ? BO.Enums.eOrderStatus.shipped : BO.Enums.eOrderStatus.ordered,
                };
            return orderList;
        }

        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }

    }
    /// <summary>
    /// get oredr by id
    /// </summary>
    /// <param name="id">id of oredr</param>
    /// <returns>founded order with this id</returns>

    public BO.Order Get(int id)
    {
        try
        {
            if (id < 0)
                throw new BlEntityInvalidIDException("ID cant be a negetive number");
            DO.Order d_order = Dal.Order.GetSingle(o => o.ID == id);
            return convertDoOrderToBoOrder(d_order, id);
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }

    /// <summary>
    /// Update Order Amount By Manager
    /// </summary>
    /// <param name="id_oi">id of order item</param>
    /// <param name="amount">new amount</param>
    /// <param name="ope">which actions to do</param>
    /// <exception cref="BlEntityInvalidException">amount cant be a nagitive number</exception>

    public void UpdateOrderAmountByManager(int id_oi, int amount, string ope)
    {
        if (amount < 0)
            throw new BlEntityInvalidException("amount cant be a nagitive number");
        DO.OrderItem d_orderItem = Dal.OrderItem.GetSingle(o => o.OrderItemID == id_oi);
        switch (ope)
        {
            case "change":
                if (amount == 0)
                    Dal.OrderItem.Delete(id_oi);
                else
                {
                    d_orderItem.Amount = amount;
                }
                break;
            case "add":
                d_orderItem.Amount += amount;
                break;
            case "decrease":
                if (d_orderItem.Amount - amount <= 0)
                    Dal.OrderItem.Delete(id_oi);
                else
                    d_orderItem.Amount -= amount;

                break;
            default:
                break;
        }
        Dal.OrderItem.Update(d_orderItem);
    }

    /// <summary>
    /// Update Delivery date by Manager
    /// </summary>
    /// <param name="id">id of order to update</param>
    /// <returns>updated oredr</returns>
    /// <exception cref="BlEntityInvalidException">product has been already been shiped</exception>
    /// <exception cref="BlEntityNotFoundException">ID was not found</exception>

    public BO.Order UpdateDelivery(int id)
    {
        try
        {
            DO.Order d_order = Dal.Order.GetSingle(o => o.ID == id);
            if (d_order.DeliveryDate < DateTime.Now)
                throw new BlEntityInvalidException("the product has been already been delivered");
            d_order.DeliveryDate = DateTime.Now;
            Dal.Order.Update(d_order);
            BO.Order b_order = convertDoOrderToBoOrder(d_order, id);
            b_order.DeliveryDate = DateTime.Now;
            return b_order;
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }
    /// <summary>
    /// Update Shipping date
    /// </summary>
    /// <param name="id">id of oredr to update</param>
    /// <returns>updateed order</returns>

    public BO.Order UpdateShipping(int id)
    {
        try
        {
            DO.Order d_order = Dal.Order.GetSingle(o => o.ID == id);
            if (d_order.ShipDate < DateTime.Now)
                throw new BlEntityInvalidException("the product has been already shiped");
            //if (d_order.DeliveryDate < d_order.ShipDate)
            //    throw new BlEntityInvalidException("the product has been delivery but not shiped");
            d_order.ShipDate = DateTime.Now;
            Dal.Order.Update(d_order);
            BO.Order b_order = convertDoOrderToBoOrder(d_order, id);
            b_order.ShipDate = DateTime.Now;
            return b_order;
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }
    /// <summary>
    /// A helper function for  functions UpdateShipping and UpdateDelivery
    /// </summary>
    /// <param name="id">id of oredr item</param>
    /// <returns>list of the Order Items and totalPrice</returns>
    (List<BO.OrderItem>, double) getItemsf(int id)
    {
        List<DO.OrderItem> orderItems = Dal.OrderItem.Get(o => o.OrderID == id).ToList();
        List<BO.OrderItem> items = new();
        return castingDOOrderToBOProductForList(orderItems);
    }

    public (List<BO.OrderItem>, double) castingDOOrderToBOProductForList(List<DO.OrderItem> orderItems)
    {

        List<BO.OrderItem> b_orderItems =
            orderItems.Select(oi => new BO.OrderItem
            {
                ID = oi.OrderItemID,
                Name = Dal.Product.GetSingle(p => p.ID == oi.ProductID).Name,
                ProductID = oi.ProductID,
                Price = oi.Price,
                Amount = oi.Amount,
                TotalPrice = oi.Amount * oi.Price,
            }).ToList();
        double totalPrice = 0;
        b_orderItems.Sum(oi => totalPrice += oi.TotalPrice);
        return (b_orderItems.ToList(), totalPrice);
    }

    public int GetOldestOrder()
    {
        DateTime? minDate = DateTime.Now;
        int minId = -1;

        foreach (var order in Dal.Order.Get())
        {
            if (order.DeliveryDate != null)
            {
                continue;
            }
            if (order.ShipDate != null)
            {
                if (minDate > order.ShipDate)
                {

                    minId = order.ID;
                    minDate = (DateTime)order.ShipDate;
                    continue;
                }

            }
            if (minDate > order.OrderDate)
            {
                minId = order.ID;
                minDate = (DateTime)order.OrderDate;
            }
        }
        return minId;
    }
}

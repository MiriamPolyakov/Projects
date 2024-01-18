using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BLOrderTracking : IOrderTracking
{
    private IDal? Dal = DalApi.Factory.Get();
    public BO.OrderTracking checkOrderTracking(BO.Order? order)
    {
        try
        {
            BO.OrderTracking orderTracking = new();
            if (order == null)
                throw new DalApi.EntityInvalidException("there is no order with this order ID");
            orderTracking.OrderID = order.ID;
            orderTracking.Status = order.Status;
            orderTracking.lsStatus.Add(new Tuple<DateTime, Enums.eOrderStatus>((DateTime)order.OrderDate, Enums.eOrderStatus.ordered));
            if (order.ShipDate != null)
                orderTracking.lsStatus.Add(new Tuple<DateTime, Enums.eOrderStatus>((DateTime)order.ShipDate, Enums.eOrderStatus.shipped));
            if (order.DeliveryDate != null)
                orderTracking.lsStatus.Add(new Tuple<DateTime, Enums.eOrderStatus>((DateTime)order.DeliveryDate, Enums.eOrderStatus.received));
            return orderTracking;
        }
        catch (EntityInvalidException e)
        {
            throw new EntityInvalidException(e.Message);
        }

    }
}


using BO;
namespace Simulator;

public class SimulatorDetails:EventArgs
{
    public Enums.eOrderStatus prevStatus;//private?
    public Enums.eOrderStatus nextStatus;
    public int id;
    public int seconds;
    public SimulatorDetails(BO.Order order, int s)
    {
        if (prevStatus == Enums.eOrderStatus.received)
            throw new Exception("this order has been received!!");
        seconds = s;
        id = order.ID;
        prevStatus = order.Status;
        if (prevStatus == Enums.eOrderStatus.shipped)
            nextStatus = Enums.eOrderStatus.received;
        else
            nextStatus = Enums.eOrderStatus.shipped;

    }
}

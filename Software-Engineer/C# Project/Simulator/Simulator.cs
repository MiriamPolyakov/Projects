using BlImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simulator.Exceptions;

namespace Simulator;

public static class Simulator
{
    private static bool stop = false;
    private static BlApi.IBl bl = BlApi.Factory.Get();

    public static event EventHandler? StopSimulator;

    public static event EventHandler? ProgressChange;


    private static void ChangeStatuses()
    {
        while (stop == false)
        {
            try
            {
                Random random = new Random();
                int id = bl.Order.GetOldestOrder();
                if (id == -1)
                {
                    StopSimulator(null, EventArgs.Empty);
                    return;
                }
                int seconds = random.Next(1, 10);
                BO.Order order = bl.Order.Get(id);
                SimulatorDetails details = new SimulatorDetails(order, seconds);
                if (ProgressChange != null)
                {
                    ProgressChange(null, details);
                }
                Thread.Sleep(seconds * 1000);
                if (details.prevStatus == BO.Enums.eOrderStatus.ordered)
                {

                    bl.Order.UpdateShipping(order.ID);
                }
                else
                {
                    bl.Order.UpdateDelivery(order.ID);
                }

            }
            catch (BlFromDalEntityNotFoundException exp)
            {
                throw new Simulator_BlFromDalEntityNotFoundException(exp.Message, exp.InnerException);
            }

        }
    }
    public static void play()
    {
        try
        {
            stop = false;
            Thread change = new Thread(ChangeStatuses);
            change.Start();
        }
        catch (Simulator_BlFromDalEntityNotFoundException ex)
        {
            throw new Simulator_BlFromDalEntityNotFoundException(ex.Message,ex.InnerException);
        }

    }
    public static void Stop()
    {
        stop = true;
        if (StopSimulator != null)
        {
            StopSimulator(null, EventArgs.Empty);
        }
    }

}



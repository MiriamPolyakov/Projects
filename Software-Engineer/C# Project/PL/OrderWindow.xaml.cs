using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>

public partial class OrderWindow : Window
{
    private IBl bl;
    private BO.Order? order = new();
    private ObservableCollection<BO.OrderForList> orderCollection;
    Tuple<BO.Order, bool> tuple;
    public bool isCustomer { get; set; }
    Window lastWindow;
    MainWindow mainWindow;
    public OrderWindow(IBl blO, int id, Window lastW, bool iC = false, ObservableCollection<BO.OrderForList> collection = null)
    {
        orderCollection = collection;
        isCustomer = iC;
        InitializeComponent();
        bl = blO;
        lastWindow = lastW;
        order = bl.Order.Get(id);
        this.DataContext = order;
    }
    /// <summary>
    /// update Oredr for manager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void updateOredrBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            order = bl.Order.UpdateOrder(order);

            OrderForList? ofl = orderCollection?.Where(p => p?.ID == order.ID).FirstOrDefault();
            if (ofl != null)
            {
                int? idx = orderCollection?.IndexOf(ofl);
                orderCollection?.Remove(orderCollection?.Where(o => o?.ID == order.ID).FirstOrDefault());
                ofl.CustomerName = order.CustomerName;
                ofl.TotalPrice = order.TotalPrice;
                //  if(OrderItemsListview.updateAmount)
                orderCollection?.Insert(idx ?? -1, ofl);
            }
            lastWindow.Show();
            this.Close();
        }
        catch (BlEntityInvalidException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + ex?.InnerException?.Message);
        }
    }
    /// <summary>
    /// update Delivey date of an order by manager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void updateDeliveyBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            order = bl.Order.UpdateDelivery(order.ID);
            OrderForList? ofl = orderCollection?.Where(p => p?.ID == order.ID).FirstOrDefault();
            int idx = orderCollection.IndexOf(ofl);
            ofl.Status = order.Status;
            orderCollection[idx] = ofl;
            this.DataContext = order;
            lastWindow.Show();
            this.Close();
        }
        catch (BlEntityInvalidException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + ex?.InnerException?.Message);
        }

    }
    /// <summary>
    /// update shipping date of an order by manager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void updateShippingBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            order = bl.Order.UpdateShipping(order.ID);
            this.DataContext = order;
        }
        catch (BlEntityInvalidException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + ex?.InnerException?.Message);
        }
    }
    /// <summary>
    /// go back to last window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        lastWindow.Show();
        this.Hide();
    }


}

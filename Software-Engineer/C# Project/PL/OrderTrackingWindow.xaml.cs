using BlApi;
using BO;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
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
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>

public partial class OrderTrackingWindow : Window
{
    private IBl bl;
    BO.OrderTracking orderTarcking = new();
    MainWindow lastWindow;
    private Tuple<OrderTracking, ObservableCollection<Tuple<DateTime, Enums.eOrderStatus>>> tuple;

    public OrderTrackingWindow(IBl blP, int id, MainWindow lastW)
    {
        InitializeComponent();
        lastWindow = lastW;
        bl = blP;
        BO.Order? order = bl.Order.Get(id);
        if (order.ID == 0)
            throw new Exception("there was no order with this ID ");
        orderTarcking = bl.OrderTracking.checkOrderTracking(order);
        tuple = new Tuple<OrderTracking, ObservableCollection<Tuple<DateTime, Enums.eOrderStatus>>>(orderTarcking, new ObservableCollection<Tuple<DateTime, Enums.eOrderStatus>>(orderTarcking.lsStatus));
        this.DataContext= tuple;
    }

    private void OredrDetailsBtn_Click(object sender, RoutedEventArgs e)
    {
        OrderWindow window = new(bl, orderTarcking.OrderID, iC: true, lastW: this);
        window.Show();
        this.Hide();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        lastWindow.Show();
        this.Close();
    }


}

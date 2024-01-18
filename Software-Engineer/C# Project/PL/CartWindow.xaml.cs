using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl bl;
    BO.Cart cart;

    BO.OrderTracking? OrderTracking = new();
    ProductItems lastWindow;
    MainWindow mainWindow;

    private ObservableCollection<BO.OrderItem> orderItemCollection { get; set; }
    Tuple<BO.Cart, ObservableCollection<BO.OrderItem>> dataContextTuple;
    public CartWindow(IBl blP, BO.Cart c, ProductItems lastW, MainWindow mainW)
    {
        InitializeComponent();
        cart = c;
        lastWindow = lastW;
        mainWindow = mainW;
        orderItemCollection = cart.Items == null ? new() : new(cart.Items);
        bl = blP;
        dataContextTuple = new(cart, orderItemCollection);
        DataContext = dataContextTuple;
    }
    /// <summary>
    /// complete an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Cart.OrderConfirmation(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAdress);
            mainWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// delete an order item from cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void deleteOrderItemBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.OrderItem item = (BO.OrderItem)((Button)sender).DataContext;
            dataContextTuple = new(null, null);
            this.DataContext = dataContextTuple;
            cart = bl.Cart.update(cart, item.ProductID, 0);
            orderItemCollection.Remove(item);
            dataContextTuple = new(cart, orderItemCollection);
            this.DataContext = dataContextTuple;
        }
        catch (BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + ex?.InnerException?.Message);
        }
    }
    /// <summary>
    /// decrease Order Item Amount in cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void decreaseOrderItemAmountBtn_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem item = (BO.OrderItem)((Button)sender).DataContext;
        cart = bl.Cart.update(cart, item.ProductID, item.Amount - 1);
        orderItemCollection = new(); dataContextTuple = new(null, null);
        this.DataContext = dataContextTuple;
        cart.Items.Select(item =>
        {
            orderItemCollection.Add(item);
            return item;
        }).ToList();
        dataContextTuple = new(cart, orderItemCollection);
        this.DataContext = dataContextTuple;
    }
    /// <summary>
    /// add Order Item Amount in cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addOrderItemAmountBtn_Click(object sender, RoutedEventArgs e)
    {
        BO.OrderItem item = (BO.OrderItem)((Button)sender).DataContext;
        cart = bl.Cart.update(cart, item.ProductID, item.Amount + 1);
        orderItemCollection = new();
        dataContextTuple = new(null, null);
        this.DataContext = dataContextTuple;
        cart?.Items?.Select(item =>
        {
            orderItemCollection.Add(item);
            return item;
        }).ToList();
        dataContextTuple = new(cart, orderItemCollection);
        this.DataContext = dataContextTuple;
    }
    /// <summary>
    /// go back to last window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        lastWindow.Show();
        this.Close();
    }


}

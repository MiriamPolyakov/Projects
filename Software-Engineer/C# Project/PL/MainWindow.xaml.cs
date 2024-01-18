using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;
namespace PL;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl;
    private Cart cart = new();
    /// <summary>
    ///  class constractor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        bl = BlApi.Factory.Get();
    }
    /// <summary>
    /// The product option click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        AdminWindow window = new(bl, this);
        window.Show();
        this.Hide();
    }

    private void NewOrderOption_Click(object sender, RoutedEventArgs e)
    {
        ProductItems window = new(bl, cart, this);
        window.Show();
        this.Hide();
    }
    private void TrackBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(OrderNumberToTrack.Text, out int id))
                throw new BlImplementation.BlEntityInvalidException("enter a number in order to view an order tracking");
            OrderTrackingWindow window = new(bl, id, this);
            window.Show();
            this.Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void playBtn_Click(object sender, RoutedEventArgs e)
    {
        SimulatorWindow window = new();
        window.Show();
    }
}


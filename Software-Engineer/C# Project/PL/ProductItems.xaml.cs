using BlApi;
using BlImplementation;
using BO;
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
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for ProductItems.xaml
/// </summary>
public partial class ProductItems : Window
{
    private IBl bl;
    private Cart cart;
    BO.OrderTracking? OrderTracking = new();
    private MainWindow mainWindow;
    private Tuple<List<ProductItem>, List<string>> tuple;

    public ProductItems(IBl blP, Cart c, MainWindow w = null)
    {
        InitializeComponent();
        bl = blP;
        cart = c;
        tuple = new Tuple<List<ProductItem>, List<string>>(bl.Product.GetCatalog().ToList(), makeCategorySelector());
        mainWindow = w;
        this.DataContext= tuple;
    }
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var choice = CategorySelector.SelectedItem;
        ProductItemListview.ItemsSource = choice == "All" ? bl.Product.GetCatalog()
            : bl.Product.GetCatalog((BO.Enums.eCategory)Enum.Parse(typeof(BO.Enums.eCategory), choice.ToString()));
    }

    private void ProductItem_Click(object sender, MouseButtonEventArgs e)
    {

        BO.ProductItem ProductItem = (BO.ProductItem)((ListView)sender).SelectedItem;
        ProductWindow window = new(bl, this, mainWindow, ProductItem.ID, true, cart);
        window.Show();
        this.Hide();
    }

    private void CartBtn_Click(object sender, RoutedEventArgs e)
    {
        CartWindow window = new(bl, cart, this, mainWindow);
        window.Show();
        this.Hide();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.Show();
        this.Hide();
    }
    List<string> makeCategorySelector()
    {
        List<string> strings = new();
        strings.Add("All");
        for (int i = 0; i < Enum.GetValues(typeof(BO.Enums.eCategory)).Length; i++)
            strings.Add(((BO.Enums.eCategory)i).ToString());
        return strings;
    }
}

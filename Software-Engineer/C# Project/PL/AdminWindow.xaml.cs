using BlApi;
using BlImplementation;
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
using static Simulator.Exceptions;

namespace PL;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    private IBl bl;
    private ObservableCollection<BO.ProductForList> productCollection { get; set; }
    public ObservableCollection<BO.OrderForList> orderCollection { get; set; }
    MainWindow lastWindow;
    private Tuple<ObservableCollection<BO.ProductForList>, ObservableCollection<BO.OrderForList>, List<string>> tuple;

    /// <summary>
    /// class constractor
    /// </summary>
    /// <param name="blP">parameter type IBl</param>
    ///
    public AdminWindow(IBl blP, MainWindow Window = null)
    {
        lastWindow = Window;
        bl = blP;
        InitializeComponent();
        List<BO.ProductForList> productList = bl.Product.GetProducList().ToList();
        productCollection = new(productList);
        List<BO.OrderForList> orderList = bl.Order.Get().ToList();
        orderCollection = new(orderList);
        tuple = new Tuple<ObservableCollection<BO.ProductForList>, ObservableCollection<BO.OrderForList>, List<string>>(productCollection, orderCollection, makeCategorySelector());
        DataContext = tuple;
    }
    /// <summary>
    /// Filter category type change event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var choice = CategorySelector.SelectedItem;
        if (choice == "All")
            ProductListview.ItemsSource = bl.Product.GetProducList();
        else
        {
            var x = (from xx in bl.Product.GetProducList()
                     where xx.Category == (BO.Enums.eCategory)Enum.Parse(typeof(BO.Enums.eCategory), choice.ToString())
                     group xx by xx.Category into newGroup
                     orderby newGroup.Key
                     select newGroup).ToList();
            ProductListview.ItemsSource = x[0];
        }
    }
    /// <summary>
    /// Click event on adding a product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        ProductWindow productWindow = new(bl, this, lastWindow, collection: productCollection);
        productWindow.Show();
    }
    /// <summary>
    /// A click event on a specific product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Clicked_on_product(object sender, MouseButtonEventArgs e)
    {
        try
        {
            BO.ProductForList ProductForList = (BO.ProductForList)((ListView)sender).SelectedItem;
            if (ProductForList == null)
                throw new Simulator_ErrorInXamlException("click on product...");
            ProductWindow productWindow = new(Bl: bl, this, lastWindow, id: ProductForList.ID, collection: productCollection);
            productWindow.Show();
            Hide();
        }
        catch (Simulator_ErrorInXamlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Clicked_on_order(object sender, MouseButtonEventArgs e)
    {
        try
        {
            BO.OrderForList OredrForList = (BO.OrderForList)((ListView)sender).SelectedItem;
            OrderWindow window = new(bl, OredrForList.ID, lastW: this, collection: orderCollection);
            window.Show();
            Hide();
        }
        catch (BlFromDalEntityNotFoundException ex)
        {
            MessageBox.Show(ex.Message + ex?.InnerException?.Message);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        lastWindow.Show();
        Close();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    List<string> makeCategorySelector()
    {
        List<string> strings = new();
        strings.Add("All");
        for (int i = 0; i < Enum.GetValues(typeof(BO.Enums.eCategory)).Length; i++)
            strings.Add(((BO.Enums.eCategory)i).ToString());
        return strings;
    }


}














///// <summary>
///// Interaction logic for AdminWindow.xaml
///// </summary>
//public partial class AdminWindow : Window
//{
//    private IBl bl;
//    private ObservableCollection<PO.ProductForList?> currentProductList { get; set; }//the list of the products 
//    /// <summary>
//    /// A private help function to convert BO.ProductForList entity to PO.ProductForList entity.
//    /// </summary>
//    private PO.ProductForList convertBoPrdLstToPoPrdLst(BO.ProductForList bP)
//    {
//        PO.ProductForList p = new()
//        {
//            Name = bP.Name,
//            Price = bP.Price,
//            Id = bP.Id,
//            Category = (BO.eCategory?)bP.Category ?? BO.eCategory.Others
//        };
//        return p;
//    }

//    /// <summary>
//    /// constractor of AdminWindow which imports the list of products.
//    /// </summary>
//    public AdminWindow(IBl Ibl)
//    {
//        InitializeComponent();
//        bl = Ibl;
//        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList();
//        currentProductList = new();
//        bProds.Select(bP =>
//        {
//            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
//            currentProductList.Add(p);
//            return bP;
//        }).ToList();
//        ProductsListview.DataContext = currentProductList;
//        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
//    }

//    /// <summary>
//    /// A function that filters the products by category.
//    /// </summary>
//    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
//    {
//        IEnumerable<BO.ProductForList?> bProds = bl.Product.ReadProdsList((BO.eCategory?)CategorySelector.SelectedItem);
//        currentProductList.Clear();
//        bProds.Select(bP =>
//        {
//            PO.ProductForList p = convertBoPrdLstToPoPrdLst(bP);
//            currentProductList.Add(p);
//            return bP;
//        }).ToList();
//        ProductsListview.DataContext = currentProductList;
//    }
//    /// <summary>
//    /// A function that opens the ProductWindow for adding a product.
//    /// </summary>
//    private void AddProductButton_Click(object sender, RoutedEventArgs e)
//    {
//        new ProductWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, null, currentProductList).Show();
//        this.Hide();
//    }
//    /// <summary>
//    /// A function that opens the ProductWindow for updating or deleting a product.
//    /// </summary>
//    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//    {
//        PO.ProductForList p = (PO.ProductForList)((ListView)sender).SelectedItem;
//        new ProductWindow(bl, this, (BO.eCategory?)CategorySelector.SelectedItem, p.Id, currentProductList).Show();
//        this.Hide();
//    }
//    /// <summary>
//    /// A function that show all the product
//    /// </summary>
//    public void DisplayAllProductsButton_Click(object sender, RoutedEventArgs e)
//    {
//        CategorySelector.SelectedItem = null;
//    }
//    /// <summary>
//    /// A function for returning to the AdminWindow.
//    /// </summary>
//    private void BackToAdminWindow_Click(object sender, RoutedEventArgs e)
//    {
//        new AdminWindow(bl).Show();
//        Close();
//    }

//    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
//    {

//    }
//}

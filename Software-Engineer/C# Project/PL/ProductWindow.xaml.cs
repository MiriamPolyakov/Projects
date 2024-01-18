using BlApi;
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
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl;
    private Product product = new();
    private Cart cart;
    private ObservableCollection<ProductForList> productCollection;
    Window LastWindow;
    MainWindow mainWindow;
    Tuple<Product, bool, Array> tuple;

    public bool IsCustomer { get; set; }
    /// <summary>
    /// Class constractor
    /// </summary>
    /// <param name="PAddBl">parameter type IBl</param>
    /// <param name="id">A parameter indicating the id sent - in deletion and update will send the ID of the product</param>
    public ProductWindow(IBl Bl, Window w, MainWindow mw, int id = -1, bool isCostumer = false, Cart c = default, ObservableCollection<ProductForList> collection = null)
    {
        productCollection = collection;
        LastWindow = w;
        mainWindow = mw;
        cart = c;
        bl = Bl;
        IsCustomer = isCostumer;
        InitializeComponent();
        if (!isCostumer)
            addProductToCartBtn.Visibility = Visibility.Hidden;
        catergorySelector.ItemsSource = Enum.GetValues(typeof(Enums.eCategory));
        try
        {
            if (id != -1)
            {
                if (isCostumer == true)
                {
                    ReadOnly();
                }
                CRUD_btn1.Content = "Update";
                CRUD_btn2.Content = "Delete";
                UpdateORDelete(id);
            }
            else
            {
                product = new Product();
                DataContext = product;
                ID_lbl.Visibility = Visibility.Hidden;
                ID_text.Visibility = Visibility.Hidden;
                CRUD_btn1.Content = "Add";
                CRUD_btn2.Visibility = Visibility.Hidden;

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void ReadOnly()
    {
        catergorySelector.IsEnabled = !true;
        CRUD_btn1.Visibility = Visibility.Hidden;
        CRUD_btn2.Visibility = Visibility.Hidden;
    }
    /// <summary>
    /// Adding the product to the product list by the entered details
    /// </summary>
    private void AddButton_Click()
    {
        bl.Product.Add(product);
        List<ProductForList> x = bl.Product.GetProducList().ToList();
        x.Select(item =>
        {
            productCollection.Add(item);
            return item;
        }).ToList();
        LastWindow.Show();
        Close();
    }
    /// <summary>
    /// Adding the details to the buttons for adding or deleting
    /// </summary>
    /// <param name="id">The id of the product</param>
    private void UpdateORDelete(int id)
    {
        product = bl.Product.GetByManager(id);
        DataContext = product;
    }

    /// <summary>
    /// Update the product to the product list by the entered details
    /// </summary>
    /// <param name="id">The id of the product</param>
    private void UpdateButton_Click()
    {
        bl.Product.update(product);
        ProductForList? pfl = productCollection?.Where(p => p?.ID == product.ID).FirstOrDefault();
        if (pfl != null)
        {
            int? idx = productCollection?.IndexOf(pfl);
            productCollection?.Remove(productCollection?.Where(p => p?.ID == product.ID).FirstOrDefault());
            pfl.Name = product.Name;
            pfl.Category = product.Category;
            pfl.Price = product.Price;
            productCollection?.Insert(idx ?? -1, pfl);
        }
        LastWindow.Show();
        Close();
    }
    /// <summary>
    /// Delete the product 
    /// </summary>
    /// <param name="id">The id of the product to delete</param>
    private void DeleteButton_Click(int id)
    {
        bl.Product.Delete(id);
        mainWindow.Show();
        Close();
    }
    /// <summary>
    /// Add/delete/update button click event at final
    /// </summary>
    /// <param name="sender">Features of the send button</param>
    /// <param name="e"></param>
    private void CRUD_Click_btn(object sender, RoutedEventArgs e)
    {
        try
        {
            if (((Button)sender).Content == "Add")
                AddButton_Click();
            else if (((Button)sender).Content == "Update")
                UpdateButton_Click();
            else if (((Button)sender).Content == "Delete")
                DeleteButton_Click(product.ID);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// add a Product from list To Cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addProductToCartBtn_Click(object sender, RoutedEventArgs e)
    {
        cart = bl.Cart.Add(cart, product.ID);
        DataContext = cart;
        BackButton_Click(sender, e);
    }
    /// <summary>
    /// go back to last window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        LastWindow.Show();
        Close();
    }


}

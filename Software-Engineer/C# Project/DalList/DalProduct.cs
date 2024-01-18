using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal;
/// <summary>
/// product functions
/// </summary>
internal class DalProduct : IProduct
{

    /// <summary>
    ///adding a product
    /// </summary>
    /// <param name="product">the product to add</param>
    /// <returns>id of the added product</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product product)
    {
        product.ID = DataSours.Config.ProductID;
        DataSours.ProductArr.Add(product);
        return product.ID;
    }


    /// <summary>
    ///reading all products
    /// </summary>
    /// <param name="func">func to filtering returning data</param>
    /// <returns>data filtered or all data</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> Get(Func<Product, bool>? func)
    {
        if (func == null)
            return DataSours.ProductArr;
        return DataSours.ProductArr.Where(func).ToList();
    }

    /// <summary>
    ///updating a product's details
    /// </summary>
    /// <param name="product">the product to update</param>
    /// <exception cref="EntityNotFoundException">no product with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product product)
    {

        int i = DataSours.ProductArr.FindIndex(p => p.ID == product.ID);
        if (i == -1)
            throw new EntityNotFoundException("ERROR---there is no product with this ID");
        DataSours.ProductArr[i] = product;
    }
    /// <summary>
    ///deleting a product
    /// </summary>
    /// <param name="id">id of product to delete</param>
    /// <exception cref="EntityNotFoundException">there is no product with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        int idx = DataSours.ProductArr.FindIndex(p => p.ID == id);
        if (idx == -1)
            throw new EntityNotFoundException("ERROR---there is no product with this ID");
        DataSours.ProductArr.Remove(DataSours.ProductArr[idx]);
    }

    /// <summary>
    /// get single product
    /// </summary>
    /// <param name="func">filtering product function</param>
    /// <returns>founded product</returns>
    /// <exception cref="EntityNotFoundException">no product with this ID</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product GetSingle(Func<Product, bool> func)
    {
        Product? product = DataSours.ProductArr.Where(func).ToList()[0];
        if (product == null)
            throw new EntityNotFoundException("ERROR---there is no product with this ID");
        else
            return (Product)product;
    }
  
}
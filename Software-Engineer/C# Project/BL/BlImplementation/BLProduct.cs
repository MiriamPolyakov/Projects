using DalApi;
using Dal;
using System.Runtime.CompilerServices;
using System.Collections;

namespace BlImplementation;
/// <summary>
/// function of product in bl
/// </summary>
internal class BLProduct : BlApi.IProduct
{

    private IDal? Dal = DalApi.Factory.Get();
    /// <summary>
    /// check validation of product
    /// </summary>
    /// <param name="p">product to check</param>
    public void CheckValidation(BO.Product p)
    {
        if (p.ID < 0)
            throw new BlEntityInvalidIDException("ID cant be a negetive number");
        if (p.Name == null || p.Name == "")
            throw new BlEntityInvalidException("Name cant be empty");
        if (p.Price <= 0)
            throw new BlEntityInvalidException("Price cant be a negetive number or 0");
        if (p.InStock < 0)
            throw new BlEntityInvalidException("InStock cant be a negetive number");
    }
    private DO.Product convertBoProdToDoProd(BO.Product b_p)
    {
        object d_p = new DO.Product();
        d_p.GetType().GetProperties().Where(prop => prop.Name != "Category").Select(prop =>
        {
            prop.SetValue(d_p, b_p.GetType().GetProperty(prop.Name)?.GetValue(b_p));
            return prop;
        }).ToList();
        DO.Product temp = (DO.Product)d_p;
        temp.Category = (DO.Enums.eCategory)b_p.Category;
        return temp;
    }
    private BO.Product convertDoProdToBoProd(DO.Product dO)
    {
        object b_p = new BO.Product();
        b_p.GetType().GetProperties().Where(prop => prop.Name != "Category").Select(prop =>
        {
            prop.SetValue(b_p, dO.GetType().GetProperty(prop.Name)?.GetValue(dO));
            return prop;
        }).ToList();
        BO.Product temp = (BO.Product)b_p;
        temp.Category = (BO.Enums.eCategory)dO.Category;
        return temp;
    }
    /// <summary>
    /// add product
    /// </summary>
    /// <param name="b_product">product to add</param>
    /// <exception cref="BlEntityNotFoundException">ID was not found and from where</exception>

    public void Add(BO.Product b_product)
    {
        try
        {
            CheckValidation(b_product);
            int i = Dal.Product.Add(convertBoProdToDoProd(b_product));
            Console.WriteLine($"product ID {i} has been added succesfully");
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }


    /// <summary>
    /// delete product by id
    /// </summary>
    /// <param name="id"></param>

    public void Delete(int id)
    {
        try
        {
            if (id < 0)
                throw new BlEntityInvalidIDException("ID cant be a negetive number");
            IEnumerable<DO.OrderItem> d_orders = Dal.OrderItem.Get();
            var x = from item in d_orders
                    where (item.ProductID == id)
                    select item.ProductID;
            if (x.FirstOrDefault() == default)
                throw new BlEntityFoundIDException("this product can't be deleted because we found an order with this id");
            Dal.Product.Delete(id);
            Console.WriteLine($"product ID {id} has been deleted succesfully");
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
        catch (BlEntityFoundIDException exception)
        {
            throw new BlEntityFoundIDException(exception.Message);
        }
    }
    /// <summary>
    /// Get Product List filtered by category or all Product List
    /// </summary>
    /// <param name="eCategory">category to filter</param>
    /// <returns>Product List filltered or all Product List</returns>

    public IEnumerable<BO.ProductForList?>? GetProducList(BO.Enums.eCategory? eCategory = null)//Func<Product?, bool>? func = null
    {
        IEnumerable<DO.Product> productsList;
        if (eCategory == null)
            productsList = Dal.Product.Get();
        else
        {
            productsList = Dal.Product.Get(p => p.Category == (DO.Enums.eCategory)eCategory);
        }
        List<BO.ProductForList> productForLists =
            productsList.Select(p => new BO.ProductForList
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (BO.Enums.eCategory)p.Category
            }).OrderBy(p => p.ID).ToList();
        return productForLists;
    }
    /// <summary>
    /// gets Products for catalog
    /// </summary>
    /// <returns>Products for catalog</returns>
    public IEnumerable<BO.ProductItem> GetCatalog(BO.Enums.eCategory? eCategory = null)
    {
        IEnumerable<DO.Product> productsList = eCategory == null ? Dal.Product.Get() :
            Dal.Product.Get(p => p.Category == (DO.Enums.eCategory)eCategory);

        List<BO.ProductItem> productItems =
            productsList.Select(p => new BO.ProductItem
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Amount = p.InStock,
                InStock = Convert.ToBoolean(p.InStock),
                Category = (BO.Enums.eCategory)p.Category

            }).ToList();
        return productItems;
    }
    /// <summary>
    /// get product by id for manager
    /// </summary>
    /// <param name="id"> id of product</param>
    /// <returns>product founded whith this id</returns>

    public BO.Product GetByManager(int id)
    {
        try
        {
            if (id < 0)
                throw new BlEntityInvalidIDException("ID cant be a negetive number");
            DO.Product d_product = Dal.Product.GetSingle(p => p.ID == id);
            return convertDoProdToBoProd(d_product);
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }

    /// <summary>
    /// get product item by id for costumer
    /// </summary>
    /// <param name="id">id of product</param>
    /// <param name="cart">cart of costumer to update details</param>
    /// <returns>product item with this id</returns>
    
    private BO.ProductItem convertDoProdToBoProdItem(DO.Product d_p)
    {
        object b_product = new BO.ProductItem();
        b_product.GetType().GetProperties().Where(prop => prop.Name != "Category" && prop.Name != "InStock" && prop.Name != "Amount").Select(prop =>
        {
            prop.SetValue(b_product, d_p.GetType().GetProperty(prop.Name)?.GetValue(d_p));
            return prop;
        }).ToList();
        BO.ProductItem temp = (BO.ProductItem)b_product;
        temp.Category = (BO.Enums.eCategory)d_p.Category;
        temp.Amount = d_p.InStock;
        temp.InStock = Convert.ToBoolean(d_p.InStock);
        return temp;
    }
    public BO.ProductItem GetByCostumer(int id, BO.Cart cart)
    {
        try
        {
            if (id < 0)
                throw new BlEntityInvalidIDException("ID cant be a negetive number");
            DO.Product d_product = Dal.Product.GetSingle(p => p.ID == id);
            return convertDoProdToBoProdItem(d_product);
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }

    }

    /// <summary>
    /// update product
    /// </summary>
    /// <param name="b_product">product to update</param>
    /// <exception cref="BlEntityNotFoundException">ID was not found</exception>
    public void update(BO.Product b_product)
    {
        try
        {
            CheckValidation(b_product);
            Dal.Product.Update(convertBoProdToDoProd(b_product));
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
    }

}



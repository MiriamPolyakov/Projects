
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using System;
using System.Net;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace BlImplementation;
/// <summary>
/// function of cart in bl
/// </summary>
internal class BLCart : ICart
{

    private IDal? Dal = DalApi.Factory.Get();
    /// <summary>
    /// adding oredr item to cart
    /// </summary>
    /// <param name="cart">global cart</param>
    /// <param name="id">id of product</param>
    /// <returns>updated cart</returns>
    /// <exception cref="BlEntityNotFoundException">ID was not found</exception>

    public BO.Cart Add(BO.Cart cart, int id)
    {
        try
        {
            int orderItemIdx = -1;
            if (!(cart?.Items == null))
                orderItemIdx = cart.Items.FindIndex(o => o.ProductID == id);
            DO.Product product = Dal.Product.GetSingle(p => p.ID == id);
            if (product.InStock > 0)
            {
                if (orderItemIdx == -1)
                {
                    List<BO.OrderItem?> localOI = new();
                    if (cart?.Items != null)
                        localOI = cart.Items;
                    localOI?.Add(createBOOrderItem(product));
                    cart.Items = localOI;
                    cart.TotalPrice += product.Price;
                }
                else
                {
                    BO.OrderItem? orderItem = cart?.Items?[orderItemIdx];
                    orderItem.Amount += 1;
                    orderItem.TotalPrice += product.Price;
                    cart.TotalPrice += product.Price;
                    cart.Items[orderItemIdx] = orderItem;
                }
            }
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
        return cart;
    }

    /// <summary>
    /// adding finnal Order
    /// </summary>
    /// <param name="cart">global cart with all order items</param>
    /// <param name="name">name of costumer</param>
    /// <param name="email">email of costumer</param>
    /// <param name="address">address of costumer</param>

    public void OrderConfirmation(BO.Cart cart, string name, string email, string address)
    {

        try
        {
            checkValidation(address, email, name);
            if (cart.Items != null)
                checkValidationOfAmountAndInstock(cart);
            int o_id = Dal.Order.Add(addOrder(address, email, name));
            DO.OrderItem d_oi = new();
            if (cart.Items != null)
            {
                foreach (BO.OrderItem? oItem in cart.Items)
                {
                    d_oi = convertBoOrderItemToDoOrderItem(oItem, o_id);
                    int id = Dal.OrderItem.Add(d_oi);
                    DO.Product product = Dal.Product.GetSingle(p => p.ID == oItem.ProductID);
                    product.InStock -= d_oi.Amount;
                    Dal.Product.Update(product);
                    Console.WriteLine($"the Order ID {o_id} Confirmation was ok!!");
                }
            }
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
        catch (BlEntityInvalidException exception)
        {
            throw new BlEntityInvalidException(exception.Message);
        }
    }

    /// <summary>
    /// update amount of order item in cart
    /// </summary>
    /// <param name="cart">global cart with all order items</param>
    /// <param name="id">id of product</param>
    /// <param name="num">new amount for order item</param>
    /// <returns>updated cart</returns>
    /// <exception cref="BlEntityNotFoundException">ID was not found</exception>

    public BO.Cart update(BO.Cart cart, int id, int num)
    {
        try
        {
            int orderItemIdx = cart.Items.FindIndex(o => o.ProductID == id);
            DO.Product product = Dal.Product.GetSingle(p => p.ID == id);
            if (orderItemIdx == -1)
            {
                cart.TotalPrice += product.Price * num;
                cart?.Items?.Add(createBOfromDO(product, num));
            }
            else
            {
                BO.OrderItem? orderItem = cart.Items[orderItemIdx];
                if (num > orderItem?.Amount)
                    cart.Items[orderItemIdx] = addAmount(orderItem, product, num, cart);
                else if (num == 0)
                {
                    cart.TotalPrice -= product.Price * orderItem.Amount;
                    cart.Items.Remove(cart.Items[orderItemIdx]);
                }
                else
                {
                    cart.Items[orderItemIdx] = decreaseAmount(orderItem, product, num, cart);
                }
            }
        }
        catch (EntityNotFoundException exception)
        {
            throw new BlFromDalEntityNotFoundException("the ID was not found-", exception);
        }
        return cart;
    }
    DO.OrderItem convertBoOrderItemToDoOrderItem(BO.OrderItem oi, int id)
    {
        object d_orderi = new DO.OrderItem();
        d_orderi.GetType().GetProperties().Where(oi => (oi.Name != "OrderID" && oi.Name != "OrderItemID")).Select(prop =>
        {
            prop.SetValue(d_orderi, oi.GetType().GetProperty(prop.Name)?.GetValue(oi));
            return prop;
        }).ToList();
        DO.OrderItem temp = (DO.OrderItem)d_orderi;
        temp.OrderID = id;
        temp.OrderItemID = 0;
        return temp;
    }
    void checkValidation(string address, string email, string name)
    {
        if (name == null || name == "")
            throw new BlEntityInvalidException("name can't be null");
        if (email == null || email == "" && IsValidEmail(email))
            throw new BlEntityInvalidException("email address can't be null");
        if (address == null || address == "")
            throw new BlEntityInvalidException("address can't be null");
    }
    DO.Order addOrder(string address, string email, string name)
    {
        DO.Order d_order = new();
        d_order.ID = 0;
        d_order.CustomerAdress = address;
        d_order.CustomerEmail = email;
        d_order.CustomerName = name;
        d_order.OrderDate = DateTime.Now;
        d_order.ShipDate = null;
        d_order.DeliveryDate = null;
        return d_order;
    }
    BO.OrderItem createBOOrderItem(DO.Product product)
    {
        BO.OrderItem oItem = new();
        oItem.Price = product.Price;
        oItem.Amount = 1;
        oItem.Name = product.Name;
        oItem.ID = 0;
        oItem.ProductID = product.ID;
        oItem.TotalPrice = product.Price;
        return oItem;
    }
    /// <summary>
    /// check Validtion of an Email
    /// </summary>
    /// <param name="email">email to check</param>
    /// <returns>if the email is Valid</returns>
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
    private (bool, bool) inStockAndAmountValidtion(BO.OrderItem oItem)
    {
        DO.Product product = Dal.Product.GetSingle(p => p.ID == oItem?.ProductID);
        bool f1 = false, f2 = false;
        if (oItem?.Amount < 0)
            f1 = true;
        if (product.InStock < oItem?.Amount)
            f2 = true;
        return (f1, f2);
    }
    BO.OrderItem createBOfromDO(DO.Product product, int num)
    {
        BO.OrderItem oItem = new();
        oItem.Price = product.Price;
        oItem.Amount = num;
        oItem.Name = product.Name;
        oItem.ID = 0;
        oItem.ProductID = product.ID;
        oItem.TotalPrice = product.Price * num;
        return oItem;
    }
    BO.OrderItem addAmount(BO.OrderItem orderItem, DO.Product product, int num, Cart cart)
    {
        orderItem.TotalPrice = product.Price * num;
        cart.TotalPrice += product.Price * (num - orderItem.Amount);
        orderItem.Amount = num;
        return orderItem;
    }
    BO.OrderItem decreaseAmount(BO.OrderItem orderItem, DO.Product product, int num, Cart cart)
    {
        orderItem.TotalPrice = product.Price * num;
        cart.TotalPrice -= product.Price * (orderItem.Amount - num);
        orderItem.Amount = num;
        return orderItem;
    }
    DO.Order createDoOrder()
    {
        DO.Order d_order = new();
        d_order.ID = 0;
        d_order.OrderDate = DateTime.Now;
        d_order.ShipDate = null;
        d_order.DeliveryDate = null;
        return d_order;
    }
    void checkValidationOfAmountAndInstock(BO.Cart cart)
    {

        var x = (from item in cart.Items
                 let check = inStockAndAmountValidtion(item)
                 select check).ToList();
        int idx1 = x.FindIndex(item => item.Item1 == true);
        int idx2 = x.FindIndex(item => item.Item2 == true);
        if (idx1 != -1)
            throw new BlEntityInvalidException("element " + idx1 + "---- amount can't be nagative");
        if (idx2 != -1)
            throw new BlEntityInvalidException("element " + idx2 + "---- sorry,the instock is less than amount");
    }
}




using BlApi;
namespace BlImplementation;
/// <summary>
/// Defining variables for access to the bl layer of logical entities
/// </summary>
sealed public class Bl : IBl
{
    public IOrder Order => new BlOrder();

    public ICart Cart => new BLCart();
    public IOrderItem OrderItem => new BLOrderItem();

    public IOrderTracking OrderTracking => new BLOrderTracking();

    public IProduct Product =>  new BLProduct();

    public IProductForList ProductForList => new BLProductForList();

    public IProductItem ProductItem => new BLProductItem();
}


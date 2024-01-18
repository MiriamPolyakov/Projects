namespace BlApi;
/// <summary>
/// Declaration of variables of the pure classes of the logical layer
/// </summary>
public interface IBl
{
    public ICart Cart { get;}
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
    public IOrderTracking OrderTracking { get; }
    public IProduct Product { get;}
    public IProductForList ProductForList { get; }
    public IProductItem ProductItem { get; }
}


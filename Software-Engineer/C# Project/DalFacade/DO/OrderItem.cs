namespace DO;
/// <summary>
/// defining order-item struct
/// </summary>
public struct OrderItem
{
    public int OrderItemID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    //overriding the ToString function for printing the order-item's details
    public override string ToString() => $@"
    OrderID: {OrderID},
    Product ID: {ProductID},
    OrderItemID: {OrderItemID},
    Price: {Price},
    Amount : {Amount}";

}


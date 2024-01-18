namespace BO;

public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string?CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public Enums.eOrderStatus Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem?>? Items { get; set; }
    public double TotalPrice { get; set; }
    //overriding the ToString function for printing the order's details
    public override string ToString()
    {
        string str = $@"
    ID={ID}, 
    Customer name - {CustomerName},
    Customer email: {CustomerEmail},
    Customer adress: {CustomerAdress},
    Order date: {OrderDate},
    Ship date: {ShipDate},
    Delivery date {DeliveryDate}";
        foreach (OrderItem item in Items)
            str += $"\n \t {item}";
        return str;
    }

}

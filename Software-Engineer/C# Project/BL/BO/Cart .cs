
namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        string str = $@"
    Customer name - {CustomerName},
    Customer email: {CustomerEmail},
    Customer adress: {CustomerAdress},
    Total price {TotalPrice}";
        if (Items != null)
            foreach (OrderItem? item in Items)
                str += $"\n \t {item}";
        return str;

    }
}



namespace BO;
public class Product
    {
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Enums.eCategory Category { get; set; }
    public int InStock { get; set; }
    //overriding the ToString function for printing the product's details
    public override string ToString() => $@"
    Product ID:{ID},
    name:{Name}, 
    category : {Category}
    Price: {Price}
    Amount in stock: {InStock}
    ";
}


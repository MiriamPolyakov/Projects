﻿namespace BO;
public class ProductItem
    {
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Enums.eCategory Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }
    public override string ToString() => $@"
    Product ID:{ID},
    name:{Name}, 
    Price: {Price}
    category : {Category}
    Amount: {Amount}
    Amount in stock: {InStock}
    ";
}


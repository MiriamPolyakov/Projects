using DO;
namespace Dal;
public static class DataSours
{
    static readonly Random rand = new Random();

    internal static List<Product> ProductArr = new();
    internal static List<Order> OrderArr = new();
    internal static List<OrderItem> OrderItemArr = new();

    const int defaultOIArrAmount = 40;
    const int defaultOArrAmount = 20;
    const int defaultPArrAmount = 10;


    /// <summary>
    ///initializes the products info 
    /// </summary>
    private static void AddProducts()
    {
        string[] productsNames = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        for (int i = 0; i < defaultPArrAmount; i++)
        {
            Product product = new();
            product.ID = Config.ProductID;
            product.Name = productsNames[i];
            product.Price = rand.NextInt64(50, 500);
            product.Category = (Enums.eCategory)(i % Enum.GetValues(typeof(Enums.eCategory)).Length);
            if (i % 20 == 0)
                product.InStock = 0;
            else
                product.InStock = (int)rand.NextInt64(0, 1000);
            ProductArr.Add(product);
        }
    }
    /// <summary>
    ///initializes the order info
    /// </summary>
    private static void AddOrders()
    {

        TimeSpan shipSpan = TimeSpan.FromDays(15);
        TimeSpan delSpan = TimeSpan.FromDays(7);

        string[] CustomersNames = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        string[] CustomersEmails = { "a@", "b@", "c@", "d@", "e@", "f@", "g@", "h@", "i@", "g@" };
        string[] CustomersAdresses = { "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "gg" };

        for (int i = 0; i < defaultOArrAmount; i++)
        {
            Order order = new();
            order.ID = Config.OrderID;
            order.CustomerName = CustomersNames[i % 10];
            order.CustomerEmail = CustomersEmails[i % 10];
            order.CustomerAdress = CustomersAdresses[i % 10];
            if (i % 10 < 8)  // 80% have ship date
                order.OrderDate = DateTime.Now;
            else
                order.OrderDate = null;
            order.ShipDate = order.OrderDate + shipSpan;
            if (i % 10 < 6)// 60% from them have delivery date
                order.DeliveryDate = order.ShipDate + delSpan;
            else
                order.DeliveryDate = null;
            OrderArr.Add(order);
        }
    }

    /// <summary>
    ///initializes the order-items info
    /// </summary>
    private static void AddOrderItems()
    {

        for (int i = 0; i < defaultOIArrAmount; i++)
        {
            OrderItem orderItem = new();
            orderItem.OrderItemID = Config.OrderItemID;
            orderItem.ProductID = (int)rand.NextInt64(ProductArr[0].ID, ProductArr[0].ID + ProductArr.Count);
            if (i < OrderArr.Count)
                orderItem.OrderID = OrderArr[i].ID;
            else
                orderItem.OrderID = (int)rand.NextInt64(OrderArr[0].ID, OrderArr[OrderArr.Count - 1].ID);
            orderItem.Amount = (int)rand.NextInt64(0, ProductArr[orderItem.ProductID - ProductArr[0].ID].InStock);
            Product p = ProductArr[orderItem.ProductID - ProductArr[0].ID];
            p.InStock -= orderItem.Amount;
            ProductArr[orderItem.ProductID - ProductArr[0].ID] = p;
            orderItem.Price = ProductArr[orderItem.ProductID - ProductArr[0].ID].Price;
            OrderItemArr.Add(orderItem);
        }
    }


    /// <summary>
    ///initializing our info
    /// </summary>
    public static void s_Initialize()
    {
        AddProducts();
        AddOrders();
        AddOrderItems();
    }
    static DataSours()
    {
        s_Initialize();
    }
    /// <summary>
    ///handing the ids and indexes of our structs
    /// </summary>
    public static class Config
    {
        private static int s_ProductID = 100000;
        public static int ProductID { get => s_ProductID++; }

        private static int s_OrderID = 500000;
        public static int OrderID { get => s_OrderID++; }

        private static int s_OrderItemID = 100000;
        public static int OrderItemID { get => s_OrderItemID++; }

    }

}





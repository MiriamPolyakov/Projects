
using DO;
using DalApi;
namespace Dal;
public class Program
{
    private static IDal? dalList =  Factory.Get(); 
    private static Product s_product = new Product();
    private static Order s_order = new Order();
    private static OrderItem s_orderItem = new OrderItem();

    /// <summary>
    /// product CRUD
    /// </summary>
    static public void OptionProduct()
    {
        /// <summary>
        /// gets the details of the product you want to add
        /// </summary>
        void AddP()
        {
            Console.WriteLine(" enter a name");
            string? str = Console.ReadLine();
            Console.WriteLine("enter a price");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("enter a caterory (1-men,2-women,3-girls,4-boys)");
            int category = Convert.ToInt32(Console.ReadLine());
            while (category > 4 || category < 1)
            {
                Console.WriteLine("wrong input, enter again");
                category = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("enter a Instock number");
            int instock = Convert.ToInt32(Console.ReadLine());
            s_product.ID = DataSours.Config.ProductID;
            s_product.Name = str;
            s_product.Price = price;
            s_product.InStock = instock;
            s_product.Category = (Enums.eCategory)category;
            int i = dalList.Product.Add(s_product);
            Console.WriteLine($"the Product ID {i} added succesfuly!!");

        }
        /// <summary>
        /// get details of one product
        /// </summary>
        void GetItemP()
        {
            try
            {
                Console.WriteLine("enter an ID of product to view");
                int id = Convert.ToInt32(Console.ReadLine());
                Product product = dalList.Product.GetSingle(p=>p.ID==id);
                Console.WriteLine(product);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        /// get details of all product arr
        /// </summary>
        void GetP()
        {
            List<Product> products = dalList.Product.Get().ToList();
            products.ForEach(p => Console.WriteLine(p));
      
        }
        /// <summary>
        ///updait a product
        /// </summary>
        void UpdateP()
        {
            string? choice;
            Console.WriteLine("enter an ID of product to update");
            int id = Convert.ToInt32(Console.ReadLine());
            Product p = dalList.Product.GetSingle(p => p.ID == id);
            Console.WriteLine(p);
            s_product.ID = id;
            Console.WriteLine("do you want to change name? (y / n) ");
            choice = Console.ReadLine();
            if (choice == "y")
            {
                Console.WriteLine("enter a name");
                string? name = Console.ReadLine();
                s_product.Name = name;
            }
            else
            {
                s_product.Name = p.Name;
            }
            Console.WriteLine("do you want to change price? (y / n) ");
            choice = Console.ReadLine();
            if (choice == "y")
            {
                Console.WriteLine("enter a price");
                int price = Convert.ToInt32(Console.ReadLine());
                s_product.Price = price;
            }
            else
            {
                s_product.Price = p.Price;
            }
            Console.WriteLine("do you want to change category? (y / n) ");
            choice = Console.ReadLine();
            if (choice == "y")
            {
                Console.WriteLine("enter a caterory (1-men,2-women,3-girls,4-boys)");
                int category = Convert.ToInt32(Console.ReadLine());
                while (category > 4 || category < 1)
                {
                    Console.WriteLine("wrong input, enter again");
                    category = Convert.ToInt32(Console.ReadLine());
                }
                s_product.Category = (Enums.eCategory)category;
            }
            else
            {
                s_product.Category = p.Category;
            }
            Console.WriteLine("do you want to change inStock num? (y / n) ");
            choice = Console.ReadLine();
            if (choice == "y")
            {
                Console.WriteLine("enter a Instock number");
                int inStock = Convert.ToInt32(Console.ReadLine());
                s_product.InStock = inStock;
            }
            else
            {
                s_product.InStock = p.InStock;
            }
            try
            {
                dalList.Product.Update(s_product);
                Product pro = dalList.Product.GetSingle(p => p.ID == id);
                Console.WriteLine(pro);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }

        /// <summary>
        ///delete a product
        /// </summary>
        void DeleteP()
        {
            try
            {
                Console.WriteLine("enter an ID of product to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                dalList.Product.Delete(id);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        Console.WriteLine("enter your choice:" +
    " 0- add product," +
    " 1- view product by id," +
    " 2- view all products," +
    " 3- update product by id," +
    " 4- delete product");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.CRUDoption.Add:
                AddP();
                break;
            case (int)Enums.CRUDoption.GetItem:
                GetItemP();
                break;
            case (int)Enums.CRUDoption.GetAll:
                GetP();
                break;
            case (int)Enums.CRUDoption.Update:
                UpdateP();
                break;
            case (int)Enums.CRUDoption.Delete:
                DeleteP();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }
    /// <summary>
    /// order CRUD
    /// </summary>
    static public void OptionOrder()
    {

        /// <summary>
        ///gets the details of the order you want to add
        /// </summary>
        void AddO()
        {
            Console.WriteLine("enter a customer name");
            string? name = Console.ReadLine();
            Console.WriteLine("enter a customer email");
            string? email = Console.ReadLine();
            Console.WriteLine("enter a customer adress");
            string? adress = Console.ReadLine();

            TimeSpan dateShip = TimeSpan.FromDays(10);
            TimeSpan dateDelivery = TimeSpan.FromDays(7);
            s_order.ID = DataSours.Config.OrderID;
            s_order.CustomerName = name;
            s_order.CustomerEmail = email;
            s_order.CustomerAdress = adress;
            s_order.ShipDate = s_order.OrderDate + dateShip;
            s_order.DeliveryDate = s_order.ShipDate + dateDelivery;
            int id = dalList.Order.Add(s_order);
            Console.WriteLine("the order ID " + id + " was added succesfuly!!");
            Order order = dalList.Order.GetSingle(o => o.ID == id);
            Console.WriteLine(order);

        }

        /// <summary>
        ///get details of one order
        /// </summary>
        void GetItemO()
        {
            Console.WriteLine("enter an ID of order to view");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                Order order = dalList.Order.GetSingle(o => o.ID == id);
                Console.WriteLine(order);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }

        /// <summary>
        /// get details of all product arr
        /// </summary>
        void GetAllO()
        {
            List<Order> orders = dalList.Order.Get().ToList();
            orders.ForEach(o => Console.WriteLine(o));
        }
        /// <summary>
        ///updait an order
        /// </summary>
        void UpdateO()
        {
            string? choice;
            Console.WriteLine("enter an ID of oreder to update");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                Order o = dalList.Order.GetSingle(o => o.ID == id);
                Console.WriteLine(o);
                s_order.ID = id;
                Console.WriteLine("do you want to change customer name? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a customer name");
                    string? name = Console.ReadLine();
                    s_order.CustomerName = name;
                }
                else
                {
                    s_order.CustomerName = o.CustomerName;
                }
                Console.WriteLine("do you want to change customer email? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a customer email");
                    string? email = Console.ReadLine();
                    s_order.CustomerEmail = email;
                }
                else
                {
                    s_order.CustomerEmail = o.CustomerEmail;
                }
                Console.WriteLine("do you want to change customer adress? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a customer adress");
                    string? adress = Console.ReadLine();
                    s_order.CustomerAdress = adress;
                }
                else
                {
                    s_order.CustomerAdress = o.CustomerAdress;
                }
                Console.WriteLine("do you want to change ship date? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("Enter a ship date: ");
                    DateTime userDateTime;
                    string? dateOrder = Console.ReadLine();
                    while (!(DateTime.TryParse(dateOrder, out userDateTime)))
                    {
                        Console.WriteLine("incorrect date, try entering again!");
                    }
                    s_order.ShipDate = DateTime.Parse(dateOrder);
                }
                else
                {
                    s_order.ShipDate = o.ShipDate;
                }
                Console.WriteLine("do you want to change delivery date? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("Enter a delivery date: ");
                    DateTime userDateTime;
                    string? dateOrder = Console.ReadLine();
                    while (!(DateTime.TryParse(dateOrder, out userDateTime)))
                    {
                        Console.WriteLine("incorrect date, try entering again!");
                    }
                    s_order.DeliveryDate = DateTime.Parse(dateOrder);
                }
                else
                {
                    s_order.DeliveryDate = o.DeliveryDate;
                }
                dalList.Order.Update(s_order);
                Order order = dalList.Order.GetSingle(o => o.ID == id);
                Console.WriteLine(order);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }

        /// <summary>
        ///delete an order
        /// </summary>
        void DeleteO()
        {
            Console.WriteLine("enter an ID of order to delete");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                dalList.Order.Delete(id);
                Console.WriteLine($"the order ID {id} deleted succesfully!!");
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        Console.WriteLine("enter your choice:" +
        " 0- add order," +
        " 1- view order by id," +
        " 2- view all orders," +
        " 3- update order by id," +
        " 4- delete order");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.CRUDoption.Add:
                AddO();
                break;
            case (int)Enums.CRUDoption.GetItem:
                GetItemO();
                break;
            case (int)Enums.CRUDoption.GetAll:
                GetAllO();
                break;
            case (int)Enums.CRUDoption.Update:
                UpdateO();
                break;
            case (int)Enums.CRUDoption.Delete:
                DeleteO();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }
    static readonly Random rand = new Random();

    /// <summary>
    ///orderItem CRUD
    /// </summary>
    static public void OptionOrderItem()
    {

        /// <summary>
        ///gets the details of the order-item you want to add
        /// </summary>
        void AddOI()
        {

            s_orderItem.OrderItemID = DataSours.Config.OrderItemID;
            Console.WriteLine("enter order id");
            int ido = Convert.ToInt32(Console.ReadLine());
            s_orderItem.OrderID = ido;
            Console.WriteLine("enter product id");
            int idp = Convert.ToInt32(Console.ReadLine());
            s_orderItem.ProductID = idp;
            double price = dalList.Product.GetSingle(o => o.ID == idp).Price;
            Console.WriteLine("enter a amount");
            int amount = Convert.ToInt32(Console.ReadLine());
            s_orderItem.Price = price;
            s_orderItem.Amount = amount;
            try
            {

                int idx = dalList.OrderItem.Add(s_orderItem);
                Console.WriteLine($"the order item ID {idx }added succesfuly!!");
                OrderItem o = dalList.OrderItem.GetSingle(o => o.OrderItemID == idx);
                Console.WriteLine(o);
            }

            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        ///get details of one order-item
        /// </summary>
        void GetItemOI()
        {
            Console.WriteLine("enter an  ID for order item to view ");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                OrderItem orderItem = dalList.OrderItem.GetSingle(o => o.OrderItemID == id);
                Console.WriteLine(orderItem);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        ///get order item by id
        /// </summary>
        void GetItemOIWithID()
        {
            Console.WriteLine("enter an  ID for order item to view ");
            int id_o = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter an  ID for product in this order to view ");
            int id_p = Convert.ToInt32(Console.ReadLine());
            try
            {
                OrderItem orderItem = dalList.OrderItem.GetSingle(o => o.OrderID == id_o&& o.ProductID == id_p);
                Console.WriteLine(orderItem);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        ///get details of all order-item arr
        /// </summary>
        void GetAllOI()
        {
            try
            {
                List<OrderItem> orderItems = dalList.OrderItem.Get().ToList().ToList();
                orderItems.ForEach(oi => Console.WriteLine(oi));
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        ///updait an order-item
        /// </summary>
        void UpdateOI()
        {
            string? choice;
            Console.WriteLine("enter an ID of order item to update and ID order item");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                OrderItem o = dalList.OrderItem.GetSingle(o => o.OrderItemID == id);
                Console.WriteLine(o);
                s_orderItem.OrderItemID = o.OrderItemID;
                s_orderItem.OrderID = o.OrderID;
                s_orderItem.ProductID = o.ProductID;
                Console.WriteLine("do you want to change price? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a price");
                    int price = Convert.ToInt32(Console.ReadLine());
                    s_orderItem.Price = price;
                }
                else
                {
                    s_orderItem.Price = o.Price;
                }
                Console.WriteLine("do you want to change amount? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a Instock number");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    s_orderItem.Amount = amount;
                }
                else
                {
                    s_orderItem.Amount = o.Amount;
                }
                dalList.OrderItem.Update(s_orderItem);
                OrderItem oi = dalList.OrderItem.GetSingle(o => o.OrderItemID == id);
                Console.WriteLine(oi);
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }
        /// <summary>
        ///delete an order-item
        /// </summary>
        void DeleteOI()
        {
            try
            {
                Console.WriteLine("enter an ID of order item to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                dalList.OrderItem.Delete(id);
                Console.WriteLine($"the order item id number {id} was deleted");
            }
            catch (EntityNotFoundException err)
            {
                throw new EntityNotFoundException(err.Message);
            }
        }

        Console.WriteLine("enter your choice:" +
             " 0- add order item," +
             " 1- view order item by id," +
             " 2- view order item by id of order and id of product" +
             " 3- view all order items," +
             " 4- update order item by id," +
             " 5- delete order");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.CRUDoption.Add:
                AddOI();
                break;
            case (int)Enums.CRUDoption.GetItem:
                GetItemOI();
                break;
            case (int)Enums.CRUDoption.GetItemID:
                GetItemOIWithID();
                break;
            case (int)Enums.CRUDoption.GetAll:
                GetAllOI();
                break;
            case (int)Enums.CRUDoption.Update:
                UpdateOI();
                break;
            case (int)Enums.CRUDoption.Delete:
                DeleteOI();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }
    public static void Main()
    {
        /// <summary>
        ///switch for user to decide what he wants to deal with: orders, orderitems or products (or exit the program)
        /// </summary>
        DataSours.s_Initialize();
        Console.WriteLine("enter your choice:" +
       " 0- exit," +
       " 1- product crud," +
       " 2- order crud," +
       " 3- order item crud");
        try
        {

            int choice = Convert.ToInt32(Console.ReadLine());
            while (choice != (int)Enums.mainOption.Exit)
            {
                switch (choice)
                {
                    case (int)Enums.mainOption.Product:
                        OptionProduct();
                        break;
                    case (int)Enums.mainOption.Order:
                        OptionOrder();
                        break;
                    case (int)Enums.mainOption.OrderItem:
                        OptionOrderItem();
                        break;
                    default:
                        Console.WriteLine("input ERROR");
                        break;
                }
                Console.WriteLine("enter your choice:" +
         " 0- exit," +
         " 1- product crud," +
         " 2- order crud," +
         " 3- order item crud");
                choice = Convert.ToInt32(Console.ReadLine());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("error please enter a number");
        }
    }
}

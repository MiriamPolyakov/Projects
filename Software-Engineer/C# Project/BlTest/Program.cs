using BlApi;
using BlImplementation;
using BO;
using System;

public class Program
{
    private static BlApi.IBl Bl = new Bl();
    private static Cart cart = new();
    //==================Main=====================
    public static void Main()
    {
        Console.WriteLine("enter your choice:" +
       " 0- Exsit," +
       " 1- Product," +
       " 2- Order," +
       " 3- Cart,");
        try
        {
            int choice = Convert.ToInt32(Console.ReadLine());
            while (choice != (int)Enums.eMainOption.exsit)
            {
                switch (choice)
                {
                    case (int)Enums.eMainOption.product:
                        OptionProduct();
                        break;
                    case (int)Enums.eMainOption.order:
                        OptionOrder();
                        break;
                    case (int)Enums.eMainOption.cart:
                        OptionCart();
                        break;
                    default:
                        Console.WriteLine("input ERROR");
                        break;
                }
                Console.WriteLine("enter your choice:" +
                " 0- Exsit," +
                " 1- Product," +
                " 2- Order," +
                " 3- Cart,");
                choice = Convert.ToInt32(Console.ReadLine());
            }
        }
        catch (BlEntityInvalidException ex)
        {
            throw new BlEntityInvalidException(ex.Message);
        }

    }

    //==================Product========================

    static public void OptionProduct()
    {
        /// <summary>
        /// add product
        /// </summary>
        void Add()
        {
            try
            {
                int category;
                Product b_product = new();
                b_product.ID = 0;
                Console.WriteLine(" enter a name");
                b_product.Name = Console.ReadLine();
                Console.WriteLine("enter a caterory (0-men,1-women,2-girls,3-boys)");
                while (!(int.TryParse(Console.ReadLine(), out category)))
                    Console.WriteLine("Error---enter a number");
                while (category > Enum.GetValues(typeof(Enums.eCategory)).Length - 1 || category < 0)
                    Console.WriteLine("Error---enter a number or in range");
                b_product.Category = (Enums.eCategory)category;
                Console.WriteLine("enter a price");
                double price;
                while (!(double.TryParse(Console.ReadLine(), out price)))
                    Console.WriteLine("Error---enter a number");
                b_product.Price = price;
                Console.WriteLine("enter a Instock number");
                int instock;
                while (!(int.TryParse(Console.ReadLine(), out instock)))
                    Console.WriteLine("Error---enter a number");
                b_product.InStock = instock;
                Bl.Product.Add(b_product);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
            catch (BlEntityInvalidException exception)
            {
                throw new BlEntityInvalidException("BlEntityInvalidException:" + exception.Message);
            }
            catch (BlEntityInvalidIDException exception)
            {
                throw new BlEntityInvalidIDException("BlEntityInvalidIDException:" + exception.Message);
            }

        }
        /// <summary>
        /// get products for the list of products
        /// </summary>
        void GetProducList()
        {
            try
            {
                List<ProductForList> productForLists = Bl.Product.GetProducList().ToList();
                productForLists.ForEach(p => Console.WriteLine(p));
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
        }
        /// <summary>
        /// get product by id for managet
        /// </summary>
        void GetByManager()
        {
            try
            {
                Console.WriteLine("enter an Id of product to Get");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Product product = Bl.Product.GetByManager(id);
                Console.WriteLine(product);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// get product by id for costumer
        /// </summary>
        void GetByCostumer()
        {
            try
            {
                Console.WriteLine("enter an Id of product to Get");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                ProductItem product = Bl.Product.GetByCostumer(id, cart);
                Console.WriteLine(product);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// delete product by id 
        /// </summary>
        void Delete()
        {
            try
            {
                int id;
                Console.WriteLine("enter an Id of product to delete");
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Bl.Product.Delete(id);
                Console.WriteLine($"The Product ID {id} deleted succesfully");
            }
            catch (BlEntityFoundIDException exception)
            {
                throw new BlEntityFoundIDException("BlEntityFoundIDException:" + exception.Message);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// update product by id for costumer
        /// </summary>
        void update()
        {
            try
            {
                Product b_product = new();
                Console.WriteLine("enter a ID of product to update");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Product product = Bl.Product.GetByManager(id);
                Console.WriteLine(product);
                b_product.ID = product.ID;
                Console.WriteLine("do you want to change name? (y / n) ");
                string choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a name");
                    b_product.Name = Console.ReadLine();
                }
                else
                {
                    b_product.Name = product.Name;
                }
                Console.WriteLine("do you want to change category? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a caterory (0-men,1-women,2-girls,3-boys)");
                    int category;
                    while (!(int.TryParse(Console.ReadLine(), out category)))
                        Console.WriteLine("Error---enter a number");
                    while (category > Enum.GetValues(typeof(Enums.eCategory)).Length || category < 0)
                    {
                        Console.WriteLine("wrong input, enter number in range");
                    }
                    b_product.Category = (Enums.eCategory)category;
                }
                else
                {
                    b_product.Category = product.Category;
                }
                Console.WriteLine("do you want to change price? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a price");
                    double price;
                    while (!(double.TryParse(Console.ReadLine(), out price)))
                        Console.WriteLine("Error---enter a number");
                    b_product.Price = price;
                }
                else
                {
                    b_product.Price = product.Price;
                }
                Console.WriteLine("do you want to change inStock num? (y / n) ");
                choice = Console.ReadLine();
                if (choice == "y")
                {
                    Console.WriteLine("enter a Instock number");
                    int InStock;
                    while (!(int.TryParse(Console.ReadLine(), out InStock)))
                        Console.WriteLine("Error---enter a number");
                    b_product.InStock = InStock;
                }
                else
                {
                    b_product.InStock = product.InStock;
                }
                Bl.Product.update(b_product);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
            catch (BlEntityInvalidException exception)
            {
                throw new BlEntityInvalidException("BlEntityInvalidException:" + exception.Message);
            }
            catch (BlEntityInvalidIDException exception)
            {
                throw new BlEntityInvalidIDException("BlEntityInvalidIDException:" + exception.Message);
            }
        }
        /// <summary>
        /// get product items for catalog
        /// </summary>
        void GetCatalog()
        {
            List<ProductItem> productItems = Bl.Product.GetCatalog().ToList();
            productItems.ForEach(p=>Console.WriteLine(p));
        }
        /// <summary>
        /// get product by id 
        /// </summary>
        void Get()
        {
            try
            {
                Console.WriteLine("enter an Id of product to Get");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Product product = Bl.Product.GetByManager(id);
                Console.WriteLine(product);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        Console.WriteLine("enter your choice:" +
    " 0- add product," +
    " 1- get catalog," +
    " 2- get list for manager" +
    " 3- get product by id for manager," +
    " 4- get product by id for costumer" +
    " 5- update product by id," +
    " 6- delete product");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.eProductCRUDoption.add:
                Add();
                break;
            case (int)Enums.eProductCRUDoption.getCatalog:
                GetCatalog();
                break;
            case (int)Enums.eProductCRUDoption.get:
                GetByManager();
                break;
            case (int)Enums.eProductCRUDoption.update:
                update();
                break;
            case (int)Enums.eProductCRUDoption.delete:
                Delete();
                break;
            case (int)Enums.eProductCRUDoption.getListForManager:
                GetProducList();
                break;
            case (int)Enums.eProductCRUDoption.getByCostumer:
                GetByCostumer();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }
    //==================Order=====================
    static public void OptionOrder()
    {
        /// <summary>
        /// get all oredrs
        /// </summary>
        void GetAll()
        {
            try
            {
                List<OrderForList> orderForLists = Bl.Order.Get().ToList();
                orderForLists.ForEach(o => Console.WriteLine(o));
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// get oreder by id
        /// </summary>
        void GetByID()
        {
            try
            {
                Console.WriteLine("enter an Id of order to get");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Order order = Bl.Order.Get(id);
                Console.WriteLine(order);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// update delivery date of order by manager
        /// </summary>
        void updateDeliveryManager()
        {
            try
            {
                Console.WriteLine("enter an Id of order to update delivery");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Order order = Bl.Order.UpdateDelivery(id);
                Console.WriteLine(order);
            }
            catch (BlEntityInvalidException exception)
            {
                throw new BlEntityInvalidException("BlEntityInvalidException:" + exception.Message);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// update shipping date of order
        /// </summary>
        void updateShipping()
        {
            try
            {
                Console.WriteLine("enter an Id of order to update shipping");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Order order = Bl.Order.UpdateShipping(id);
                Console.WriteLine(order);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException", exception);
            }
        }
        Console.WriteLine("enter your choice:" +
        " 0- view order by id," +
        " 1- view all orders," +
        " 2- update delivery of order by id," +
        " 3- update shiping of order by id");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.eOrderCRUDoption.getByID:
                GetByID();
                break;
            case (int)Enums.eOrderCRUDoption.getAll:
                GetAll();
                break;
            case (int)Enums.eOrderCRUDoption.updateDelivery:
                updateDeliveryManager();
                break;
            case (int)Enums.eOrderCRUDoption.updateShipping:
                updateShipping();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }
    //==================Cart=====================
    static public void OptionCart()
    {
        /// <summary>
        /// add product to cart
        /// </summary>
        void AddProductToCart()
        {
            try
            {
                Console.WriteLine("enter product id");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                cart = Bl.Cart.Add(cart, id);
                Console.WriteLine(cart);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// update product amount in a cart
        /// </summary>
        void updateProductAmount()
        {
            try
            {
                Console.WriteLine("enter product id");
                int id;
                while (!(int.TryParse(Console.ReadLine(), out id)))
                    Console.WriteLine("Error---enter a number");
                Console.WriteLine("enter new amount for the product");
                int num;
                while (!(int.TryParse(Console.ReadLine(), out num)))
                    Console.WriteLine("Error---enter a number");
                cart = Bl.Cart.update(cart, id, num);
                Console.WriteLine(cart);
            }
            catch (BlInvalidIntegerException exception)
            {
                throw new BlInvalidIntegerException("BlInvalidIntegerException:" + exception.Message);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
        }
        /// <summary>
        /// adding finnal oreder
        /// </summary>
        void OrderConfirmation()
        {
            try
            {
                Console.WriteLine("enter the customer's name");
                string? CustomerName = Console.ReadLine();
                Console.WriteLine("enter the customer's email");
                string? CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter the customer's address");
                string? CustomerAddress = Console.ReadLine();
                Bl.Cart.OrderConfirmation(cart, CustomerName, CustomerEmail, CustomerAddress);
            }
            catch (BlEntityNotFoundException exception)
            {
                throw new BlEntityNotFoundException("BlEntityNotFoundException:", exception);
            }
            catch (BlEntityInvalidException exception)
            {
                throw new BlEntityInvalidException("BlEntityInvalidException:" + exception.Message);
            }
        }
        Console.WriteLine("enter your choice:" +
        " 0- add cart," +
        " 1- update cart," +
        " 2- order confirmation");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case (int)Enums.eCartCRUDoption.add:
                AddProductToCart();
                break;
            case (int)Enums.eCartCRUDoption.update:
                updateProductAmount();
                break;
            case (int)Enums.eCartCRUDoption.orderConfirmation:
                OrderConfirmation();
                break;
            default:
                Console.WriteLine("input ERROR");
                break;
        }
    }

}




namespace BO;
public class Enums
{
    /// <summary>
    /// enum for category of products
    /// </summary>
    public enum eCategory
    {
       school,
        office,
        crafts,
        arts
    }
    /// <summary>
    /// enum for order satatus
    /// </summary>
    public enum eOrderStatus
    {
        ordered,
        shipped,
        received
    }
    /// <summary>
    /// enum for getting from main
    /// </summary>
    public enum eMainOption
    {
        exsit,
        product,
        order,
        cart
    }
    /// <summary>
    /// enum for product crud fanctions
    /// </summary>
    public enum eProductCRUDoption
    {
        add,
        getCatalog,
        getListForManager,
        get,
        getByCostumer,
        update,
        delete
    
    }
    /// <summary>
    /// enum for order crud fanctions
    /// </summary>
    public enum eOrderCRUDoption
    {
        getByID,
        getAll,
        updateDelivery,
        updateShipping
    }
    /// <summary>
    /// enum for cart crud fanctions
    /// </summary>
    public enum eCartCRUDoption
    {
        add,
        update,
        orderConfirmation
    }

}


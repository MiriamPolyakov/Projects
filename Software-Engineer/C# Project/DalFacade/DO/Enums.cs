namespace DO;
public struct Enums
{
    /// /// <summary>
    /// enum for the categories in the store
    /// </summary>
    public enum eCategory
    {
        school,
        office,
        crafts,
        arts
    }
    /// <summary>
    /// enum for the main options
    /// </summary>
    public enum mainOption
    {
        Exit, Product, Order, OrderItem
    }
    /// <summary>
    /// enum for the options of the different actions on orders and products and order item
    /// </summary>
    public enum CRUDoption
    {
        Add, GetItem, GetItemID, GetAll, Update, Delete
    }

}

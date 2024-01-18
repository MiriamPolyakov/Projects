

namespace BlImplementation;

public static class Convertions
{
    public static IEnumerable<BO.Product> ConvertListFromDOToBO(this IEnumerable<DO.Product> d_products)
    {
        List<BO.Product> b_products = new();
        BO.Product b_p = new();
        foreach (DO.Product d_p in d_products)
        {
            b_p.ID = d_p.ID;
            b_p.Name = d_p.Name;
            b_p.Category = (BO.Enums.eCategory)d_p.Category;
            b_p.InStock = d_p.InStock;
            b_p.Price = d_p.Price;
            b_products.Add(b_p);
        }
        return b_products;
    }
}



using System.Collections;

namespace BlApi;
/// <summary>
/// The template of functions required from the product-logical entity
/// </summary>
public interface IProduct
{
    public void Add(BO.Product p);
    public IEnumerable<BO.ProductForList?>? GetProducList(BO.Enums.eCategory? eCategory = null);
    public void Delete(int id);
    public void update(BO.Product p);
    public IEnumerable<BO.ProductItem?>? GetCatalog(BO.Enums.eCategory? eCategory = null);
    public BO.Product? GetByManager(int id);
    public BO.ProductItem? GetByCostumer(int id, BO.Cart cart);
}


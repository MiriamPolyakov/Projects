using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class Product : IProduct
{

    XDocument? root = XDocument.Load("..\\xml\\product.xml");

    public int Add(DO.Product obj)
    {
        XDocument? exConfig = XDocument.Load("..\\xml\\config.xml");
        if (!int.TryParse(exConfig.Descendants("s_ProductID").FirstOrDefault()?.Value, out int id))
            throw new EntityInvalidException("s_ProductID is null");
        exConfig.Descendants("s_ProductID").FirstOrDefault().Value = (id + 1).ToString();
        exConfig.Save("..\\xml\\config.xml");
        XElement? product = makeXElementProduct(obj, id);
        root?.Element("Products")?.Add(product);
        root?.Save("..\\xml\\product.xml");
        return obj.ID;
    }

    public void Delete(int id)
    {
        root?.Descendants("product").Where(p => int.Parse(p.Element("ID").Value) == id).Remove();
        root?.Save("..\\xml\\product.xml");
    }
    public IEnumerable<DO.Product> Get(Func<DO.Product, bool>? func = null)
    {
        List<XElement>? productArr = root?.Descendants("product").ToList();
        List<DO.Product> d_products = convertFromXelementToProduct(productArr).ToList();
        //productArr?.ForEach(p => convertFromXmlToXelement(d_products, p));
        if (func == null)
            return d_products;
        return d_products.Where(func);
    }

    public DO.Product GetSingle(Func<DO.Product, bool>? func)
    {
        /////////////////אופציה של id לא קיים!!!!!!!!!!
        if (func == null)
            throw new EntityInvalidException("func is null");
        List<XElement>? productArr = root?.Descendants("product").ToList();
        List<DO.Product> d_products = convertFromXelementToProduct(productArr).ToList();
        if (d_products.Where(func).FirstOrDefault().ID == default)
          throw new EntityNotFoundException("this product id not found");
        return d_products.Where(func).FirstOrDefault();
    }

    public void Update(DO.Product obj)
    {
        XElement? product = makeXElementProduct(obj, obj.ID);
        root?.Descendants("product")?.Where(p => int.Parse(p.Element("ID").Value) == obj.ID)?.FirstOrDefault()?.ReplaceWith(product);
        root?.Save("..\\xml\\product.xml");
    }
    //public DO.Product convertFromXmlToXelement(List<DO.Product> d_products, XElement product)
    //{
    //    DO.Product d_product = new();
    //    d_product.ID = int.Parse(product.Element("ID").Value);
    //    d_product.Name = product.Element("Name").Value;
    //    d_product.Category = Enum.Parse<DO.Enums.eCategory>(product.Element("Category").Value);
    //    d_product.Price = int.Parse(product.Element("Price").Value);
    //    d_product.InStock = int.Parse(product.Element("InStock").Value);
    //    d_products.Add(d_product);
    //}
    public IEnumerable<DO.Product> convertFromXelementToProduct(List<XElement>? productArr)
    {
        return from product in productArr
               select new DO.Product
               {
                   ID = int.Parse(product.Element("ID").Value),
                   Name = product.Element("Name").Value,
                   Category = Enum.Parse<DO.Enums.eCategory>(product.Element("Category").Value),
                   Price = int.Parse(product.Element("Price").Value),
                   InStock = int.Parse(product.Element("InStock").Value)
               };
    }
    public XElement makeXElementProduct(DO.Product obj, int id)
    {
        XElement? product = new("product",
           new XElement("ID", id),
           new XElement("Name", obj.Name),
           new XElement("Price", obj.Price),
           new XElement("Category", obj.Category),
           new XElement("InStock", obj.InStock));
        return product;
    }

}

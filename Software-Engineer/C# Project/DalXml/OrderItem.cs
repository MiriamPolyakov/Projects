
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class OrderItem : IOrderItem
{
    
    public int Add(DO.OrderItem obj)
    {
        XDocument? exConfig = XDocument.Load("..\\xml\\config.xml");
        int id;
        if (!int.TryParse(exConfig.Descendants("s_OrderItemID").FirstOrDefault()?.Value, out id))
            throw new EntityInvalidException("s_OrderItemID is null");
        exConfig.Descendants("s_OrderItemID").FirstOrDefault().Value = (id + 1).ToString();
        exConfig.Save("..\\xml\\config.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader r = new("..\\xml\\orderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        obj.OrderItemID = id;
        lst?.Add(obj);
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\orderItem.xml");
        ser.Serialize(w, lst);
        w.Close();
        return obj.OrderItemID;
    }

    public void Delete(int id)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader r = new("..\\xml\\orderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        lst = lst?.Where(o => o.OrderItemID != id).ToList();
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\orderItem.xml");
        ser.Serialize(w, lst);
        w.Close();
    }

    public IEnumerable<DO.OrderItem> Get(Func<DO.OrderItem, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader r = new("..\\xml\\orderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        if (func != null)
            lst = lst?.Where(func).ToList();
        r.Close();
        return lst;
    }

    public DO.OrderItem GetSingle(Func<DO.OrderItem, bool>? func)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader r = new("..\\xml\\orderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        if (func == null)
            throw new Exception("func can't be null");
        r.Close();
        return (DO.OrderItem)lst.Where(func).ToList().FirstOrDefault();
    }

    public void Update(DO.OrderItem obj)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamReader r = new("..\\xml\\orderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        int idx = lst.FindIndex(o => o.OrderItemID == obj.OrderItemID);
        if (idx == -1)
            throw new EntityNotFoundException("this obj is not found,so it cant be updated");
        lst[idx] = obj;
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\orderItem.xml");
        ser.Serialize(w, lst);
        w.Close();
    }
    public void AddList(List<DO.OrderItem> obj)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>));
        StreamWriter w = new StreamWriter("..\\xml\\orderItem.xml");
        ser.Serialize(w, obj);
        w.Close();
    }
}

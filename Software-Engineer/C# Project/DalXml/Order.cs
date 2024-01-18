namespace Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class Order : IOrder
{
   
    public int Add(DO.Order obj)
    {
        XDocument? exConfig = XDocument.Load("..\\xml\\config.xml");
        int id;
        if (!int.TryParse(exConfig.Descendants("s_OrderID").FirstOrDefault()?.Value, out id))
            throw new EntityInvalidException("s_OrderID is null");
        exConfig.Descendants("s_OrderID").FirstOrDefault().Value = (id + 1).ToString();
        exConfig.Save("..\\xml\\config.xml");
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new("..\\xml\\order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        obj.ID = id;
        lst?.Add(obj);
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\order.xml");
        ser.Serialize(w, lst);
        w.Close();
        return obj.ID;
    }

    public void Delete(int id)
    {

        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new("..\\xml\\order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        lst = lst?.Where(o => o.ID != id).ToList();
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\order.xml");
        ser.Serialize(w, lst);
        w.Close();
    }

    public IEnumerable<DO.Order> Get(Func<DO.Order, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new("..\\xml\\order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        if (func != null)
            lst = lst?.Where(func).ToList();
        r.Close();
        return lst;
    }

    public DO.Order GetSingle(Func<DO.Order, bool>? func)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new("..\\xml\\order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        if (func == null)
            throw new Exception("func can't be null");
        r.Close();
        return (DO.Order)lst.Where(func).ToList().FirstOrDefault();
    }

    public void Update(DO.Order obj)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamReader r = new("..\\xml\\order.xml");
        List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        int idx = lst.FindIndex(o => o.ID == obj.ID);
        if (idx == -1)
            throw new EntityNotFoundException("this obj is not found,so it cant be updated");
        lst[idx] = obj;
        r.Close();
        StreamWriter w = new StreamWriter("..\\xml\\order.xml");
        ser.Serialize(w, lst);
        w.Close();
    }
    public void AddList(List<DO.Order> obj)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        StreamWriter w = new StreamWriter("..\\xml\\order.xml");
        ser.Serialize(w, obj);
        w.Close();
    }
}


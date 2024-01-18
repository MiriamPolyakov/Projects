namespace BO;
public class OrderTracking
{
    public int OrderID { get; set; }
    public Enums.eOrderStatus Status { get; set; }
    public List<Tuple<DateTime, Enums.eOrderStatus>> lsStatus = new();
    public override string ToString() => $@"
    ID={OrderID}, 
    Status :{Status}
";
}


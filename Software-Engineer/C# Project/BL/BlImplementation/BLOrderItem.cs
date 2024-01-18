
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BLOrderItem : BlApi.IOrderItem
{
     private IDal? Dal = DalApi.Factory.Get();
}



using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

    internal class BLProductForList:IProductForList
    {
    private IDal? Dal = DalApi.Factory.Get();
}


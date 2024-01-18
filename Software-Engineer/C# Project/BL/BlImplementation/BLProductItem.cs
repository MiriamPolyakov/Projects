
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BLProductItem : IProductItem
{
    private IDal? Dal = DalApi.Factory.Get();
}


using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BLOrderForList:IOrderForList
{
    private IDal? Dal = DalApi.Factory.Get();
}


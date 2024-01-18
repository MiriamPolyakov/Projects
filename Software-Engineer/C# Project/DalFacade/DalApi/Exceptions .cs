
namespace DalApi;
/// <summary>
/// id not found
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) :base(message){ }
}
/// <summary>
/// error of dupplicated data
/// </summary>
public class EntityDuplicateException : Exception
{
    public EntityDuplicateException(string message) :base(message){}
}
public class EntityInvalidException : Exception
{
    public EntityInvalidException(string message) :
                                      base(message)
    {

    }
}



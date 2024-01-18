using DalApi;
namespace BlImplementation;


/// <summary>
/// getting invalid data
/// </summary>
public class BlEntityInvalidException : Exception
{
    public BlEntityInvalidException(string message) :
                                      base(message)
    {

    }
}
/// <summary>
/// getting not found id
/// </summary>
public class BlFromDalEntityNotFoundException : Exception
{
    public BlFromDalEntityNotFoundException(string message, EntityNotFoundException inner) :
                                   base(message, inner)
    {
        
    }
}
public class BlEntityNotFoundException : Exception
{
    public BlEntityNotFoundException(string message, BlEntityNotFoundException inner) :
                                   base(message, inner)
    {

    }
}
/// <summary>
/// getting invalid intenger
/// </summary>
public class BlInvalidIntegerException : Exception
{
    public BlInvalidIntegerException(string message) :
                                    base(message)
    {

    }
}
/// <summary>
/// getting invalid id
/// </summary>
public class BlEntityInvalidIDException : Exception
{
    public BlEntityInvalidIDException(string message) :
                                    base(message)
    {

    }
}
/// <summary>
/// getting id that exists already
/// </summary>
public class BlEntityFoundIDException : Exception
{
    public BlEntityFoundIDException(string message) :
                                      base(message)
    {

    }
}
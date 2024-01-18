using BlImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Exceptions
{
    public class Simulator_BlFromDalEntityNotFoundException : Exception
    {
        public Simulator_BlFromDalEntityNotFoundException(string message, Exception inner) :
                                          base(message, inner)
        {

        }
    }
    public class Simulator_ErrorInXamlException : Exception
    {
        public Simulator_ErrorInXamlException(string message) :
                                          base(message)
        {

        }
    }
}

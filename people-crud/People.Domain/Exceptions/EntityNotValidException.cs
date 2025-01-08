using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Domain.Exceptions
{
    public class EntityNotValidException(string message) : Exception(message)
    {
    }
}

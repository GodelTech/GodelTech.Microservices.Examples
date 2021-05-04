using System;

namespace Microservice.DataAccessEFCore.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userName) 
            : base($"User { userName } is not found in database")
        {
        }
    }
}

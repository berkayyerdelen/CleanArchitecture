using System;

namespace Core.Comman.Exceptions
{
    public class VerifyPasswordHashException:Exception
    {
        public VerifyPasswordHashException(string name, object key)
        :base($"Entity \"{name}\" ({key}) was not found.")
        {
            
        }

      
    }
}
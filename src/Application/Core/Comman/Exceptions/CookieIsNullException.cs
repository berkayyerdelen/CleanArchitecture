using System;

namespace Core.Comman.Exceptions
{
    public class CookieIsNullException: Exception
    {
        public CookieIsNullException()
            : base($"Cookie is null or empty")
        {
        }
    }
}

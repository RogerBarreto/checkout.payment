using System;

namespace Checkout.WebApi.Common.Exceptions
{
    public class AuthUserNotFoundException : Exception
    {
        public AuthUserNotFoundException() : base("Authenticated user not found")
        {
            
        }
    }
}
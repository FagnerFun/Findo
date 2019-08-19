using System;

namespace Findo.Framework.Interface.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get { return 400; } }

        public BusinessException()
        {

        }

        public BusinessException(string message) : base(message)
        {
        }
    }
}

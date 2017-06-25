using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Infraestrutura
{
    [Serializable]
    public class BusinessServiceException : Exception
    {
        public BusinessServiceException(string message) : base(message) { }
        public BusinessServiceException(string message, Exception inner) : base(message, inner) { }
        protected BusinessServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
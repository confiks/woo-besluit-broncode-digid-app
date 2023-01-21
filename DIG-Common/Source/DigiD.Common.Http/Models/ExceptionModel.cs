using System;
using System.Collections.Generic;

namespace DigiD.Common.Http.Models
{
    public class ExceptionModel
    {
        public Exception Exception { get; }
        public Dictionary<string, string> Properties { get; }

        public ExceptionModel(Exception exception, Dictionary<string, string> properties)
        {
            Exception = exception;
            Properties = properties;
        }
    }
}

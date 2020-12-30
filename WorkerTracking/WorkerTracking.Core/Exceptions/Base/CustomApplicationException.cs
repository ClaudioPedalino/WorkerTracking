﻿using System;

namespace WorkerTracking.Core.Exceptions.Base
{
    public class CustomApplicationException : Exception
    {
        public CustomApplicationException(string message, Exception innerException)
             : base(message, innerException)
        {
        }

        public CustomApplicationException(string message)
            : base(message)
        {
        }
    }
}

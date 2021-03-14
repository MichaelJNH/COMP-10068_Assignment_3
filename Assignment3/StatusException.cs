using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3
{
    /// <summary>
    /// An exception that can be thrown and includes the relevant status code
    /// </summary>
    public class StatusException : Exception
    {
        /// <summary>
        /// Status code to return
        /// </summary>
        public int StatusCode { get; set; }

        public StatusException(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }
    }
}

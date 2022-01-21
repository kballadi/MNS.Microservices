using System;
using System.Runtime.Serialization;

namespace MNS.Services.Utilization.DomainExceptions
{
    /// <summary>
    /// Domain Exception Class
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}

using System;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class NotActiveException : Exception
    {
        public string ExceptionMessage { get; set; }
        public RedirectToRouteResult RedirectToRouteResult { get; set; }

        public NotActiveException(string message)
            : base(string.Format("{0}", message))
        {
        }
    }
}
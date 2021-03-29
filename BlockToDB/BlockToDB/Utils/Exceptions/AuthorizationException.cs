using System;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class AuthorizationException : Exception
    {
        public string ExceptionMessage { get; set; }
        public RedirectToRouteResult RedirectToRouteResult { get; set; }

        public AuthorizationException(string message)
            : base(string.Format("{0}", message))
        {
        }
    }
}
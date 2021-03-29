using System;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class FunctionalityAuthorizationException : Exception
    {
        public string ExceptionMessage { get; set; }
        public RedirectToRouteResult RedirectToRouteResult { get; set; }

        public FunctionalityAuthorizationException(string message)
            : base(string.Format("{0}", message))
        {
        }
    }
}
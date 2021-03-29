using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class BussinesExceptionMvc : Application.BussinesException
    {
        public RedirectToRouteResult RedirectToRouteResult { get; set; }

        public BussinesExceptionMvc(int number, string message, RedirectToRouteResult redirectToRouteResult)
            : base(string.Format("({0}) {1}", number.ToString(), message))
        {
            Number = number;
            ExceptionMessage = message;
            RedirectToRouteResult = redirectToRouteResult;
        }
    }
}
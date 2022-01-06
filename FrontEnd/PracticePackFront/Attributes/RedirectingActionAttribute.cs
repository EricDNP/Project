using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticePackFront.Helpers;
using PracticePackFront.Models;

namespace PracticePackFront.Attributes
{
    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Controller controller = filterContext.Controller as Controller;
            User user = null;
            var identity = filterContext.HttpContext.Session.Get("Identity");

            if (identity != null)
                user = ByteConverter.ByteArrayToObject<User>(identity);

            if (user == null)
            {
                string action = filterContext.ActionDescriptor.RouteValues["action"];

                if(action != "Login" && action != "Register")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                }
            }
            controller.ViewData["Identity"] = user;
        }
    }
}

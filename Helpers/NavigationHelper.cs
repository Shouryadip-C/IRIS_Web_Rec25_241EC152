using Microsoft.AspNetCore.Mvc.Rendering;

namespace IRIS_Web_Rec25_241EC152.Helpers
{
    public static class NavigationHelper
    {
        public static string MakeActive(this IHtmlHelper html, string controller, string action)
        {
            var routeData = html.ViewContext.RouteData.Values;
            var currentController = routeData["controller"]?.ToString();
            var currentAction = routeData["action"]?.ToString();

            return (controller == currentController && action == currentAction) ? "active" : "";
        }
    }
}
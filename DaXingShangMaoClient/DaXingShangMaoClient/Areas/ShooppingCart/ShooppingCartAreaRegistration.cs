using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.ShooppingCart
{
    public class ShooppingCartAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShooppingCart";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShooppingCart_default",
                "ShooppingCart/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
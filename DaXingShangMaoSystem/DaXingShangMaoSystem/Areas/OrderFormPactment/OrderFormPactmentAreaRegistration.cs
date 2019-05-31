using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.OrderFormPactment
{
    public class OrderFormPactmentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OrderFormPactment";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OrderFormPactment_default",
                "OrderFormPactment/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DaJianUIL
{
    public class DaJianUILAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DaJianUIL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DaJianUIL_default",
                "DaJianUIL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
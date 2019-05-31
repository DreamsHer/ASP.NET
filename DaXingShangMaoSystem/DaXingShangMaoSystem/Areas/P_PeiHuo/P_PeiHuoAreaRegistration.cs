using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.P_PeiHuo
{
    public class P_PeiHuoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "P_PeiHuo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "P_PeiHuo_default",
                "P_PeiHuo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DiaoBaDan
{
    public class DiaoBaDanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DiaoBaDan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DiaoBaDan_default",
                "DiaoBaDan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
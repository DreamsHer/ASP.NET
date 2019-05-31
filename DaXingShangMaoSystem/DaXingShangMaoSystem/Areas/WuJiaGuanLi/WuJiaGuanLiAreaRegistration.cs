using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.WuJiaGuanLi
{
    public class WuJiaGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WuJiaGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WuJiaGuanLi_default",
                "WuJiaGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
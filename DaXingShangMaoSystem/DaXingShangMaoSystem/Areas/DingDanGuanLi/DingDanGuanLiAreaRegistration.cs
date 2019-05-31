using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DingDanGuanLi
{
    public class DingDanGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DingDanGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DingDanGuanLi_default",
                "DingDanGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
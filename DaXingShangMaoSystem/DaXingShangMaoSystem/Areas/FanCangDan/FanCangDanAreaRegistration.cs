using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.FanCangDan
{
    public class FanCangDanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FanCangDan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FanCangDan_default",
                "FanCangDan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
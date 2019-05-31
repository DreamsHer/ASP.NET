using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaXinXiGuanLi
{
    public class HuiYuanKaXinXiGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HuiYuanKaXinXiGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HuiYuanKaXinXiGuanLi_default",
                "HuiYuanKaXinXiGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
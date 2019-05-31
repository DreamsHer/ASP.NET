using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi
{
    public class HuiYuanKaJiChuXinXiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HuiYuanKaJiChuXinXi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HuiYuanKaJiChuXinXi_default",
                "HuiYuanKaJiChuXinXi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
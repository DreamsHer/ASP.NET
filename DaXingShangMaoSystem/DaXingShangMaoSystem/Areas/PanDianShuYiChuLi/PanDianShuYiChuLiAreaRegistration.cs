using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi
{
    public class PanDianShuYiChuLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PanDianShuYiChuLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PanDianShuYiChuLi_default",
                "PanDianShuYiChuLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
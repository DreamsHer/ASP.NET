using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinJinCang
{
    public class ShangPinJinCangAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShangPinJinCang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShangPinJinCang_default",
                "ShangPinJinCang/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
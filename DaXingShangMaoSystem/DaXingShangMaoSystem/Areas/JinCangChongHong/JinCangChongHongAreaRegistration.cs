using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.JinCangChongHong
{
    public class JinCangChongHongAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JinCangChongHong";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JinCangChongHong_default",
                "JinCangChongHong/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
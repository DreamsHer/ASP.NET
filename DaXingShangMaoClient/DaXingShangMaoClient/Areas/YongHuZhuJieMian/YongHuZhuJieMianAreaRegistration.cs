using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.YongHuZhuJieMian
{
    public class YongHuZhuJieMianAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "YongHuZhuJieMian";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "YongHuZhuJieMian_default",
                "YongHuZhuJieMian/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
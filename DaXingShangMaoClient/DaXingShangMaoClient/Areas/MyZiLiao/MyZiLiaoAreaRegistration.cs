using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.MyZiLiao
{
    public class MyZiLiaoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MyZiLiao";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MyZiLiao_default",
                "MyZiLiao/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
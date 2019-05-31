using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinGuanLi
{
    public class ShangPinGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShangPinGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShangPinGuanLi_default",
                "ShangPinGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi
{
    public class GoodsZhangGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GoodsZhangGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GoodsZhangGuanLi_default",
                "GoodsZhangGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaKuCunKaGuanLi
{
    public class HuiYuanKaKuCunKaGuanLiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HuiYuanKaKuCunKaGuanLi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HuiYuanKaKuCunKaGuanLi_default",
                "HuiYuanKaKuCunKaGuanLi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
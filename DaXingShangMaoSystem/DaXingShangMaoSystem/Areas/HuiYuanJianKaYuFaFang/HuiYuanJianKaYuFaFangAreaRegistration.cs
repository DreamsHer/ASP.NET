using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanJianKaYuFaFang
{
    public class HuiYuanJianKaYuFaFangAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HuiYuanJianKaYuFaFang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HuiYuanJianKaYuFaFang_default",
                "HuiYuanJianKaYuFaFang/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
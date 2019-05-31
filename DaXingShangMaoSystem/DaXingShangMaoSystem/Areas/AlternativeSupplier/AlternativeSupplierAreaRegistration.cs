using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.AlternativeSupplier
{
    public class AlternativeSupplierAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AlternativeSupplier";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AlternativeSupplier_default",
                "AlternativeSupplier/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
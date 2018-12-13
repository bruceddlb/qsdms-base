using System.Web.Mvc;

namespace QSDMS.Application.Web.Areas.AAManage
{
    public class AAManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AAManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AAManage_default",
                "AAManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

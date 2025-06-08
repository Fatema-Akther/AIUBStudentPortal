using AutoMapper;

using System.Data.Entity.SqlServer;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AIUBStudentPortal.BLL;

namespace AIUBStudentPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Fix for EF provider error
        static MvcApplication()
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Add your profile here
            });

            IMapper mapper = config.CreateMapper();
            // Save the mapper globally so it can be used via DI or manually
            Application["mapper"] = mapper;
        }
    }
}

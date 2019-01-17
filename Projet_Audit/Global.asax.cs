using Microsoft.AspNet.Identity;
using Projet_Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Projet_Audit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected void Application_Start()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjAzODVAMzEzNjJlMzQyZTMwajRYL3dNNU4rTDB2VlZLRFB3QXZxRnZjVEpXaUxSS1JOSVpXRm1YR3ZFYz0=");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Add Roles
            if(db.Roles.ToList().Count==0)
            {
                Microsoft.AspNet.Identity.EntityFramework.IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name="Admin";
                db.Roles.Add(role);
                db.SaveChanges();
            }
            
           

        }
     

    }
}

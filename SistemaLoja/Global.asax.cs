using SistemaLoja.Models;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SistemaLoja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Migração automática.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.SistemaLojaContext, Migrations.Configuration>());
            //Referência para a conexão Default (usada para login)
            ApplicationDbContext db = new ApplicationDbContext();

            CriarRoles(db);
            CriarSuperUser(db);
            AddPermissoesSuperUser(db);
            db.Dispose();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissoesSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("super@super.com");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }

            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }

            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CriarSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("super@super.com");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "super@super.com",
                    Email = "super@super.com"
                };

                userManager.Create(user, "Super.123");
            }
        }

        private void CriarRoles(ApplicationDbContext db)
        {
            //Conexão para a geração dos métodos de create, ...
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            //A base é criada dimanicamente, por isso essas verificações.
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
        }
    }
}

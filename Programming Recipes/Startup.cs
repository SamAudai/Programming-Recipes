using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("مدير الموقع"))
            {
                role.Name = "مدير الموقع";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Audai Sam";
                user.Email = "audaisam5@gmail.com";
                var check = userManager.Create(user, "Aud@i123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "مدير الموقع");
                }
            }
        }

    }
}

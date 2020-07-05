using IdentityServer3.AspNetIdentity;
using Kbalan.TouchType.Data.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBalan.IdentityServerHost
{
    public static class UserServiceFactory
    {
        public static AspNetIdentityUserService<IdentityUser, string> Create()
        {
            var context = new IdentityDbContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TouchTypeGameContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            return new AspNetIdentityUserService<IdentityUser, string>(userManager);
        }
    }
}

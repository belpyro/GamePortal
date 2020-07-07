using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AliaksNad.Battleship.IdentityServer3.SelfHost
{
    public static class UserServiceFactory
    {
        public static AspNetIdentityUserService<IdentityUser, string> Create()
        {
            var context = new SelfhostUsersContext();
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            return new AspNetIdentityUserService<IdentityUser, string>(userManager);
        }
    }
}
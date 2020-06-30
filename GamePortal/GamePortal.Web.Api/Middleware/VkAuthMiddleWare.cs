using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GamePortal.Web.Api.Middleware
{
    public class VkAuthMiddleWare : OwinMiddleware
    {
        public VkAuthMiddleWare(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
            {
                RedirectUri = "/external/vk"
            }, "MyVk");
            return Task.CompletedTask;
        }
    }
}
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GamePortal.Web.Api.Middleware
{
    /// <summary>
    /// Google Authenthication class
    /// </summary>
    public class TTGGoogleAuthMiddleWare : OwinMiddleware
    {
        public TTGGoogleAuthMiddleWare(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
            {
                RedirectUri = "/ttg/external/google"
            }, "TTGGoogle" );
            return Task.CompletedTask;
        }
    }
}
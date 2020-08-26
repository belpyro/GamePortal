using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Kbalan.TouchType.Logic.Services;
using JetBrains.Annotations;
using Ninject;
using Kbalan.TouchType.Logic;


namespace GamePortal.Web.Api.Filters.TTG
{

        public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
        {
        private readonly IUserService userService;

        private string[] usersList;

        IKernel kernel = new StandardKernel(new TTGDIModule());
        public CustomAuthorizationAttribute( params string[] users)
            {
            this.userService = kernel.Get<IUserService>();
  
            this.usersList = users;
            }
            public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
                            CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
            {

            IPrincipal principal = actionContext.RequestContext.Principal;

            string id = principal.Identity.GetUserId();
            var userrole = await userService.GetRoleByIdAsync(id);
            if (principal == null || !usersList.Contains(userrole))
                {
                    return await Task.FromResult<HttpResponseMessage>(
                           actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
                }
                else
                {
                    return await continuation();
                }
            }
            public bool AllowMultiple
            {
                get { return false; }
            }
        }
    
}

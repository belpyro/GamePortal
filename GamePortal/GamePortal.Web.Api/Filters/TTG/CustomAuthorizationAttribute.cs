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
using System.Security.Claims;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GamePortal.Web.Api.Filters.TTG
{

        public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
        {
        private string[] usersList;


        public CustomAuthorizationAttribute( params string[] users)
            {
            this.usersList = users;
            }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
                        CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {

            string id = HttpContext.Current.Request.Headers["id-token"];
            if (id == null || !usersList.Contains(id))
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

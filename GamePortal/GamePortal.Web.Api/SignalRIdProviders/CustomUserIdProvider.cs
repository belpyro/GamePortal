using IdentityServer3.Core.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Web.Api.SignalRIdProviders
{
    public class CustomUserIdProvider : IUserIdProvider
    {

        public string GetUserId(IRequest request)
        {

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.User != null && request.User.Identity != null)
            {
                return request.User.Identity.Name;
            }

            return null;
        }
    }
}
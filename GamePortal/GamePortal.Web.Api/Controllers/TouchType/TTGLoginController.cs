
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Kbalan.TouchType.Logic.Services;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    
    public class TTGLoginController : ApiController
    {
        private readonly IUserService _userService;

        public TTGLoginController(IUserService userService  )
        {
            this._userService = userService;
        }

        [HttpGet, Route("external/google")]
        public async Task<IHttpActionResult> GoogleLoginAsync()
        {
            var provider = Request.GetOwinContext().Authentication;
            var loginInfo = await provider.GetExternalLoginInfoAsync();

            if (loginInfo == null) return BadRequest();

            await _userService.RegisterExternalUser(loginInfo);
            return Ok();
        }
    }
}

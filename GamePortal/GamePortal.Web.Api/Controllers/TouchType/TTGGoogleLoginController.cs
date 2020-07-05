
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
    
    public class TTGGoogleLoginController : ApiController
    {
        private readonly IUserService _userService;

        public TTGGoogleLoginController(IUserService userService  )
        {
            this._userService = userService;
        }

        [HttpGet, Route("ttg/external/google")]
        public async Task<IHttpActionResult> GoogleLoginAsync()
        {
            var provider = Request.GetOwinContext().Authentication;
            var loginInfo = await provider.GetExternalLoginInfoAsync();

            if (loginInfo == null) return BadRequest();

            await _userService.RegisterExternalUser(loginInfo);
            return Ok();
        }

        [HttpGet, Route("ttg/external/vk")]
        public async Task<IHttpActionResult> VkLoginAsync()
        {
            var provider = Request.GetOwinContext().Authentication;
            var loginInfo = await provider.GetExternalLoginInfoAsync();

            if (loginInfo == null) return BadRequest();

            await _userService.RegisterExternalUser(loginInfo);
            return Ok();
        }
    }
}

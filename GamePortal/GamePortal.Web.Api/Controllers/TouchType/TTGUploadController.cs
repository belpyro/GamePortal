using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Services;
using Namotion.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/upload")]
    public class TTGUploadController : ApiController
    {
        private readonly IUploadService _uploadService;

        public TTGUploadController([NotNull]IUploadService uploadService)
        {
            this._uploadService = uploadService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> UploadAsync()
        {
            var file = HttpContext.Current.Request.Files[0];
            
            var result = await _uploadService.UploadAsync(file);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }
    }
}

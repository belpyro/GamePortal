using CSharpFunctionalExtensions;
using Kbalan.TouchType.Logic.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kbalan.TouchType.Logic.Services
{
    /// <summary>
    /// Service for Upload
    /// </summary>
    public interface IUploadService
    {
        /// <summary>
        /// file upload method interface
        /// </summary>
        /// <returns>Result</returns>
        Task<Result<String>> UploadAsync(HttpPostedFile file, string userId);

    }
}


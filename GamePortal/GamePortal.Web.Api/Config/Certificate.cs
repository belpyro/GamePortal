using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace GamePortal.Web.Api.Config
{
    static class Certificate
    {
        public static X509Certificate2 Get()
        {
            return new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\idsrv3test.pfx"), "idsrv3test");
        }
    }
}
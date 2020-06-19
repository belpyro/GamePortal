
using Ninject;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Interception;

namespace Kbalan.TouchType.Logic.Aspects
{
    class LoggerInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public LoggerInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            var logger = _kernel.Get<ILogger>();
            logger.Information($"{invocation.Request.Method.Name} method was required from {invocation.Request.Target}");
            invocation.Proceed();
            logger.Information(invocation.ReturnValue.ToString());
        }
    }
}

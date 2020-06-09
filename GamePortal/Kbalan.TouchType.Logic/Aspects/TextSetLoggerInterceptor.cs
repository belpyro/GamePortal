using Castle.DynamicProxy;
using Ninject;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Aspects
{
    class TextSetLoggerInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public TextSetLoggerInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            var logger = _kernel.Get<ILogger>();
            logger.Information($"{invocation.Method.Name} method was required from {invocation.InvocationTarget}");
            invocation.Proceed();
        }
    }
}

using Ninject;
using Ninject.Extensions.Interception;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Aspects
{
    class BattleshipLoggerInterceptor : SimpleInterceptor
    {
        private readonly ILogger _logger;

        public BattleshipLoggerInterceptor(ILogger logger)
        {
            this._logger = logger;
        }

        protected override void BeforeInvoke(IInvocation invocation)
        {
            base.BeforeInvoke(invocation);
            
            _logger.Information($" {invocation.Request.Method.Name} method was required from {invocation.Request.Context.Request.ParentRequest}");
        }

        protected override void AfterInvoke(IInvocation invocation)
        {
            base.AfterInvoke(invocation);

            _logger.Information($@" {invocation.Request.Method.Name} method returned value: {invocation.ReturnValue}");
        }
    }
}

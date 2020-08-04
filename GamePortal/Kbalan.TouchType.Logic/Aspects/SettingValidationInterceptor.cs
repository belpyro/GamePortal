
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Logic.Dto;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Interception;

namespace Kbalan.TouchType.Logic.Aspects
{
    /// <summary>
    /// Interceptor for SettingService Proxy. It includes PostValidation  Update method
    /// </summary>
    class SettingValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public SettingValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public void Intercept(IInvocation invocation)
        {
            //id null checking
            var userId = invocation.Request.Arguments.OfType<String>().SingleOrDefault();
            if (userId == null)
            {
                invocation.Proceed();
                return;
            }

            //model null checking
            var model = invocation.Request.Arguments.OfType<SettingDto>().SingleOrDefault() as SettingDto;
            if (model == null)
            {
                invocation.Proceed();
                return;
            }

            var settingValidator = _kernel.Get<IValidator<SettingDto>>();

            //Prevalidation for Update method
            if (invocation.Request.Method.Name.Equals("Update"))
            {
                var preValidationResult = settingValidator.Validate(model, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }
 
            //Implementation of validation for update method
            if ( invocation.Request.Method.Name.Equals("Update"))
             {
                //Cheking if user with id exist
                var userModel = _kernel.Get<TouchTypeGameContext>().ApplicationUsers.Include("Setting").SingleOrDefault(x => x.Id == (string)userId);
                if (userModel == null)
                {
                    invocation.ReturnValue = Result.Failure($"No user with id {userId} exist");
                    return;
                }

                //Replace model setting id from Dto to correct id from Db
                model.SettingId = userModel.Setting.SettingId;

                //Validation
                ValidationResult validationResult = settingValidator.Validate(model, ruleSet: "PostValidation");
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }


            invocation.Proceed();
        }
    }
}

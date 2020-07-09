using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Profiles;
using AliaksNad.Battleship.Logic.Services;
using AliaksNad.Battleship.Logic.Validators;
using AutoMapper;
using FluentValidation;
using Ninject.Modules;
using Castle.DynamicProxy;
using Ninject;
using AliaksNad.Battleship.Logic.Aspects;
using Serilog;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AliaksNad.Battleship.Logic.Services.Contracts;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using System.Collections.Generic;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.AspNetIdentity;

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            var logger = new LoggerProfile().CreateLogger();
            this.Bind<ILogger>().ToConstant(logger)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            this.Bind<BattleAreaContext>().ToSelf();

            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();
            this.Bind<IValidator<BattleAreaDto>>().To<BattleAreaDtoValidator>();

            this.Bind<IUserStore<IdentityUser>>().To<UserStore<IdentityUser>>();
            var user = this.Bind<UserManager<IdentityUser>>().ToMethod(ctx => 
            {
                var manager = new UserManager<IdentityUser>(ctx.Kernel.Get<IUserStore<IdentityUser>>());
                manager.UserValidator = new UserValidator<IdentityUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator() 
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };
                manager.EmailService = new BattleshipEmailService();

                manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();

                return manager;
            });

            var userServiceBinding = this.Bind<IUserService>().To<UserService>();
            userServiceBinding.Intercept().With<ValidationInterceptor>();
            userServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();

            var gameServiceBinding = this.Bind<IGameService>().To<GameService>();
            gameServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();

            this.Bind<ServerOption>().ToSelf();
        }
    }

    public class ServerOption : IdentityServerServiceFactory
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ServerOption(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public IdentityServerServiceFactory option()
        {
            var factory = new IdentityServerServiceFactory();

            var client = new Client
            {
                ClientId = "BattleshipWebClient",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "Battleship Web Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:44333" }
            };

            var user = new InMemoryUser()
            {
                Username = "user",
                Password = "666",
                Subject = "132-456-789"
            };

            factory
                .UseInMemoryScopes(StandardScopes.All)
                .UseInMemoryClients(new[] { client });
            //.UserService = new Registration<IUserService>(UserServiceFactory.Create());
            //.UseInMemoryUsers(new List<InMemoryUser>() { user });
            factory.UserService = new Registration<IdentityServer3.Core.Services.IUserService>(new AspNetIdentityUserService<IdentityUser, string>(_userManager));
            return factory;
        }
    }
}

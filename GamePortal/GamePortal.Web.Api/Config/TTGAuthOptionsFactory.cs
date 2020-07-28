using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.VKontakte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Web.Api.Config
{
    static public class TTGAuthOptionsFactory
    {
        public static IdentityServerBearerTokenAuthenticationOptions GetIdentityServerBearerTokenAuthenticationOptions()
        {
            return new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:10000/",
                ClientId = "TTGWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                SigningCertificate = Certificate.Get(),
                ValidationMode = ValidationMode.Local,
                IssuerName = "http://localhost:10000/",
                ValidAudiences = new[] { "http://localhost:10000/resources" }
            };
        }

        public static VKontakteAuthenticationOptions GetVKontakteAuthenticationOptions()
        {
            return new VKontakteAuthenticationOptions
            {
                ClientId = "7526371",
                ClientSecret = "Z3blscBduDFc17p8NpWw",
                AuthenticationType = "TTGVk",
                Scope = { "email" }
            };
        }

        public static GoogleOAuth2AuthenticationOptions GetGoogleAuthenticationOptions()
        {
            return new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "719719063561-v05dg5416mu8km1u2filstn03oqj98s4.apps.googleusercontent.com",
                ClientSecret = "n8sW2lGlSM7QsayPw97knojT",
                AuthenticationType = "TTGGoogle"
            };
        }
    }
}
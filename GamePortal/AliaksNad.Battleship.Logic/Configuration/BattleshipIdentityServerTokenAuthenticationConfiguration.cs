using IdentityServer3.AccessTokenValidation;


namespace AliaksNad.Battleship.Logic.Configuration
{
    public class BattleshipIdentityServerTokenAuthenticationConfiguration
    {
        public IdentityServerBearerTokenAuthenticationOptions Get()
        {
            return new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44313",
                ClientId = "BattleshipWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://localhost:44313",
                ValidAudiences = new[] { "https://localhost:44313/resources" }
            };
        }
    }
}

using IdentityServer3.AccessTokenValidation;


namespace AliaksNad.Battleship.Logic.Configuration
{
    public class BattleshipISAuthConfig
    {
        public IdentityServerBearerTokenAuthenticationOptions Get()
        {
            return new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://aliaksnad-battleship.azurewebsites.net",
                ClientId = "BattleshipWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://aliaksnad-battleship.azurewebsites.net",
                ValidAudiences = new[] { "https://aliaksnad-battleship.azurewebsites.net/resources" }
            };
        }
    }
}

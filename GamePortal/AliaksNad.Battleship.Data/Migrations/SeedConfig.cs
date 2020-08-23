using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AliaksNad.Battleship.Data.Migrations
{
    internal sealed class SeedConfig
    {
        public IdentityUser GetExampleUser()
        {
            return new IdentityUser { UserName = "Evil", PasswordHash = new PasswordHasher().HashPassword("999"), Email = "Evil@example.com" };
        } 
    }
}

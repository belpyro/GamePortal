using Microsoft.AspNet.Identity;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic
{
    internal class BattleshipEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // implementation for smtp client 
            // mailkit 
            //Mailgun 

            // SMS twilio

            return Task.CompletedTask;
        }
    }
}
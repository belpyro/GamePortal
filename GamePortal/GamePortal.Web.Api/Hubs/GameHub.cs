using AliaksNad.Battleship.Logic.Models.Game;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GamePortal.Web.Api.Hubs
{
    public interface IGameClient
    {
        Task GameStart(int AreaId);

        Task SendMessage(string msg);

        Task SendAreaId(int AreaId);
    }

    public class GameHub : Hub<IGameClient>
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendMessage(message);
        }

        public async Task SendAreaId(int AreaId)
        {
            await Clients.Others.SendAreaId(AreaId);
        }
    }
}
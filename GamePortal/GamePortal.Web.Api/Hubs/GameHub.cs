using AliaksNad.Battleship.Logic.Models.Game;
using Kbalan.TouchType.Logic.Dto;
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
        Task GameStart(BattleAreaDto dto);

        Task SendMessage(string msg);

        Task DeleteUser(string msg);

    }

    public class GameHub : Hub<IGameClient>
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendMessage(message);
        }
    }
}
using AliaksNad.Battleship.Logic.Models;

namespace AliaksNad.Battleship.Logic.Services
{
    interface IGameService
    {
        bool HitCheck(Point point);
    }
}

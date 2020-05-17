using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Web.Api.Models.Quoridor
{
    public static class RepoFromPrototypes
    {
        public static List<RegPlayer> _users = new List<RegPlayer>
    {
        new RegPlayer
        { Id = 1, FirstName = "Ivan", LastName = "Hromyko", DateOfBirth = Convert.ToDateTime("08.03.1990"), Email = "gromyko@gmail.com", UserName = "Ivan_gromyko", Password = "12345" },
        new RegPlayer {Id = 2, FirstName = "Oleg", LastName = "Sidorov", DateOfBirth = Convert.ToDateTime("03.01.1989"), Email = "grom", UserName = "Oleg_Sidr", Password = "54321"}
        };

        public static Dictionary<RegPlayer, int> _leaderBoard = new Dictionary<RegPlayer, int>
        {
            [new RegPlayer() { UserName = "Ivan" }] = 10,
            [new RegPlayer() { UserName = "Fedor" }] = 5,
        };
    }
}
using Bogus;
using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Services.User
{
    internal class UserFaker
    {
        private static Faker<UserDTO> _fakerList;
        public static Dictionary<UserDTO, int> _leaderBoard;
        static UserFaker()
        {
            _fakerList = new Faker<UserDTO>();
            _fakerList.RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Password, f => f.Random.Int(1000, 10000).ToString())
                .RuleFor(x => x.DateOfBirth, f => f.Date.Between(Convert.ToDateTime("1930, 01, 01"), DateTime.Now))
                .RuleFor(x => x.Avatar, f => f.Person.Avatar);

            _leaderBoard = new Dictionary<UserDTO, int>();

        }
        internal static List<UserDTO> GenerateList(int count = 6)
        {
            return _fakerList.Generate(count);
        }

    }
}

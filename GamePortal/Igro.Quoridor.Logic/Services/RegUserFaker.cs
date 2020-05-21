using Bogus;
using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic.Services
{
    internal class RegUserFaker
    {
        private static Faker<RegPlayerDTO> _fakerList;
        public static Dictionary<RegPlayerDTO, int> _leaderBoard;
        static RegUserFaker()
        {
            _fakerList = new Faker<RegPlayerDTO>();
            _fakerList.RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.UserName, f => f.Person.UserName)
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Email, f => f.Person.Email)
                .RuleFor(x => x.Password, f => f.Random.Int(1000, 10000).ToString())
                .RuleFor(x => x.DateOfBirth, f => f.Date.Between(Convert.ToDateTime("1930, 01, 01"), DateTime.Now))
                .RuleFor(x => x.Avatar, f => f.Person.Avatar);

            _leaderBoard = new Dictionary<RegPlayerDTO, int>();

        }
        internal static List<RegPlayerDTO> GenerateList(int count = 100)
        {
            return _fakerList.Generate(count);
        }

    }
}

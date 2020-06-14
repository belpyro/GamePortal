using AliaksNad.Battleship.Logic.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Services
{
    internal static class UserFaker
    {
        private static Faker<UserDto> _faker;

        static UserFaker()
        {
            _faker = new Faker<UserDto>();
            _faker.RuleFor(x => x.Id, a => a.IndexFaker)
                .RuleFor(x => x.Name, a => a.Person.FirstName)
                .RuleFor(x => x.Email, a => a.Person.Email)
                .RuleFor(x => x.Password, a => a.Random.String2(8, 8));
        }

        internal static IEnumerable<UserDto> Generate(int count = 100)
        {
            return _faker.Generate(count);
        }
    }
}

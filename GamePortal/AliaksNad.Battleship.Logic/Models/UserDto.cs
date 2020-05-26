using AliaksNad.Battleship.Logic.DTO;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<StatisticDto> Statistics { get; set; }
    }
}
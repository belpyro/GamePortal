using System.Collections.Generic;
using System.Data.SqlTypes;

namespace AliaksNad.Battleship.Data.Models
{
    public class UserDb : UserEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<StatisticDb> Statistics { get; set; }
    }
}
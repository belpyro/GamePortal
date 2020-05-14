using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public int LevelOfText { get; set; }
        public string Role { get; set; }
        public int MaxSpeedRecord { get; set; }
        public int NumberOfGamesPlayed { get; set; }
    }
}

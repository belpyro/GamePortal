using Kbalan.TouchType.Data.Models;

namespace Kbalan.TouchType.Logic.Dto
{
    public class SettingDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Password { get; set; }

        public LevelOfText LevelOfText { get; set; }

        public Role Role { get; set; }
    }
}

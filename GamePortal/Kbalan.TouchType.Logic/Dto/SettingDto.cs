using Kbalan.TouchType.Data.Models;

namespace Kbalan.TouchType.Logic.Dto
{
    public class SettingDto
    {
        public int SettingId { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public LevelOfText LevelOfText { get; set; } = LevelOfText.Easy;

        public Role Role { get; set; } = Role.User;

    }
}

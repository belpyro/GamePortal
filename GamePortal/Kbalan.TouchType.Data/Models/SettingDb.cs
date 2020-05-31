namespace Kbalan.TouchType.Data.Models
{
    public class SettingDb
    {
        public int SettingId { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public LevelOfText LevelOfText { get; set; } = LevelOfText.Easy;

        public Role Role { get; set; } = Role.User;

        public UserDb User { get; set; }
    }
}

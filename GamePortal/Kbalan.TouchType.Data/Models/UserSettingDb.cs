namespace Kbalan.TouchType.Data.Models
{
    public class UserSettingDb
    {
        public int Id { get; set; }

        public UserDb User { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Password { get; set; }

        public LevelOfText LevelOfText { get; set; } = LevelOfText.Easy;

        public Role Role { get; set; } = Role.User;
    }
}

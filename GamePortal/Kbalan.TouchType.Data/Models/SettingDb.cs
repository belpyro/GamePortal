namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for Setting Table. Setting table has PK - SettingId and FK - User(one-to-one with UserDb). Each User has one Setting Set and each Setting Set
    /// has one user whom this setting set belong to.
    /// </summary>
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

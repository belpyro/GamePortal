namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for Setting Table. Setting table has PK - SettingId and FK - User(one-to-one with UserDb). Each User has one Setting Set and each Setting Set
    /// has one user whom this setting set belong to.
    /// </summary>
    public class SettingDb
    {
        public string SettingId { get; set; }

        public string AvatarUrl { get; set; }

        public LevelOfText LevelOfText { get; set; } = LevelOfText.Easy;

        public ApplicationUser AppUser { get; set; }
    }
}

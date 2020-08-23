using Kbalan.TouchType.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Model for transfering Setting Set from SettingDb without related User
    /// </summary>
    public class SettingDto
    {
        public string SettingId { get; set; }

        public string Avatar { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LevelOfText LevelOfText { get; set; } = LevelOfText.Easy;

    }
}

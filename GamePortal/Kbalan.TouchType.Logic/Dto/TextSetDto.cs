using Kbalan.TouchType.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Model for transfering Text Set from TextSetDb
    /// </summary>
    public class TextSetDto
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string TextForTyping { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LevelOfText LevelOfText { get; set; }
    }
}

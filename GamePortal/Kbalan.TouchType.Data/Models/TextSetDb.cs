using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for Text Set Table from TouchTypeGame database. Text set has it's name, text for typing during game and Level of difficulty for this text
    /// </summary>
    public class TextSetDb:Entity
    {
        public string Name { get; set; }

        public string TextForTyping { get; set; }

        public LevelOfText LevelOfText { get; set; }

    }
}

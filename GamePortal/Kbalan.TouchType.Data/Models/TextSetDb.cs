using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    public class TextSetDb:Entity
    {
        public string Name { get; set; }

        public string TextForTyping { get; set; }

        public LevelOfText LevelOfText { get; set; }

    }
}

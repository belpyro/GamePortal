using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for Text Set Table mini version(without text)
    /// from TouchTypeGame database. Text set has it's name
    /// and Level of difficulty for this text
    /// </summary>
    public class TextSetDbmin
    {
        public string Name { get; set; }

        public LevelOfText LevelOfText { get; set; }
    }
}

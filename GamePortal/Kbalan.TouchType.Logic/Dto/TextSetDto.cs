using Kbalan.TouchType.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    public class TextSetDto
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string TextForTyping { get; set; }

        public LevelOfText LevelOfText { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Data.Models
{
    public class SingleGame
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string TextForTyping { get; set; }

        public string CurrentPartToType { get; set; }

        public int SymbolsToType { get; set; }

        public int SymbolsTyped { get; set; } = 0;

        public bool IsGameFinished { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Data.Models
{
    public class GameDb
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

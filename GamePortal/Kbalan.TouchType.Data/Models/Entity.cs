using System;

namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Class with most popular entites for TouchTypeGame models 
    /// </summary>
    public class Entity
    {
        public int Id { get; set; }

        public int? CreatorId { get; set; }

        public DateTime CreateOn { get; set; } = DateTime.UtcNow;

        public int? UpdateBy { get; set; }

        public DateTime? UpdateOn { get; set; }
    }
}

using System;

namespace AliaksNad.Battleship.Data.Models
{
    public class UserEntity
    {
        public int Id { get; set; }

        public int? CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
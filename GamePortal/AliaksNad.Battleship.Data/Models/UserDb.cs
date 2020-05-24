using System;

namespace AliaksNad.Battleship.Data.Models
{
    public class UserDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
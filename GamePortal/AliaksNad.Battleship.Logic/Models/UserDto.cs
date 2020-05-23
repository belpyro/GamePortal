using AliaksNad.Battleship.Logic.DTO;

namespace AliaksNad.Battleship.Logic.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
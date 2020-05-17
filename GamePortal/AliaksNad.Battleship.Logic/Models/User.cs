using AliaksNad.Battleship.Logic.DTO;

namespace AliaksNad.Battleship.Logic.Models
{
    public class User : LogInDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
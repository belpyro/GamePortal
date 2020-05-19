using Nmihay.BlackJack.Entities.Abstract;

namespace Nmihay.BlackJack.Entities
{
    public class GameEntity : BaseEntity
    {
        public string Title { get; set; }
        public int PlayerId { get; set; }
    }
}

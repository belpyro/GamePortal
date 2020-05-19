using System;

namespace Nmihay.BlackJack.Entities.Abstract
{
    /// <summary>
    /// Liskov Sabstitution Principle
    /// </summary>
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
    }
}

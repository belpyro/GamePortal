using System;

namespace Nmihay.BlackJack.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

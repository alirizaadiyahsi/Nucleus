using System;

namespace Nucleus.Core
{
    //todo: add creation/update time/user
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
using System;

namespace Nucleus.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public Guid CreatorId { get; set; }

        public Guid ModifierId { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
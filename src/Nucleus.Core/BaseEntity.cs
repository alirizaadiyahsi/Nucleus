using System;

namespace Nucleus.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid CreateUserId { get; set; }

        public Guid UpdateUserId { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
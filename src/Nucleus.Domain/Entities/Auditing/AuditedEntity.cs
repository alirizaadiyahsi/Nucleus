using System.ComponentModel.DataAnnotations.Schema;

namespace Nucleus.Domain.Entities.Auditing;

public abstract class AuditedEntity : EntityBase, IAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual Guid? CreatorUserId { get; set; }

    public virtual DateTime? ModificationTime { get; set; }

    public virtual Guid? ModifierUserId { get; set; }
}

public abstract class AuditedEntity<TUser> : AuditedEntity, IAudited<TUser>
{
    [ForeignKey("CreatorUserId")]
    public virtual TUser CreatorUser { get; set; }

    [ForeignKey("ModifierUserId")]
    public virtual TUser ModifierUser { get; set; }
}
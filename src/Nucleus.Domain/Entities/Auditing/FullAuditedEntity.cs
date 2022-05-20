using System.ComponentModel.DataAnnotations.Schema;

namespace Nucleus.Domain.Entities.Auditing;

public abstract class FullAuditedEntity : EntityBase, IFullAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual Guid? CreatorUserId { get; set; }

    public virtual DateTime? ModificationTime { get; set; }

    public virtual Guid? ModifierUserId { get; set; }

    public virtual bool IsDeleted { get; set; }

    public virtual Guid? DeleterUserId { get; set; }

    public virtual DateTime? DeletionTime { get; set; }
}

public abstract class FullAuditedEntity<TUser> : FullAuditedEntity, IFullAudited<TUser>
{
    [ForeignKey("CreatorUserId")]
    public virtual TUser CreatorUser { get; set; }

    [ForeignKey("ModifierUserId")]
    public virtual TUser ModifierUser { get; set; }

    [ForeignKey("DeleterUserId")]
    public virtual TUser DeleterUser { get; set; }
}
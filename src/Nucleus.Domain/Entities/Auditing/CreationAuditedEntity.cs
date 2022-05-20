using System.ComponentModel.DataAnnotations.Schema;

namespace Nucleus.Domain.Entities.Auditing;

public abstract class CreationAuditedEntity : EntityBase, ICreationAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual Guid? CreatorUserId { get; set; }
}

public abstract class CreationAuditedEntity<TUser> : CreationAuditedEntity, ICreationAudited<TUser>
{
    [ForeignKey("CreatorUserId")]
    public virtual TUser CreatorUser { get; set; }
}
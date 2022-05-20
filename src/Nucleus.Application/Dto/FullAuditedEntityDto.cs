using Nucleus.Domain.Entities.Auditing;

namespace Nucleus.Application.Dto;

public abstract class FullAuditedEntityDto : AuditedEntityDto, IFullAudited
{
    public virtual DateTime? DeletionTime { get; set; }

    public virtual bool IsDeleted { get; set; }

    public virtual Guid? DeleterUserId { get; set; }
}
using Nucleus.Domain.Entities.Auditing;

namespace Nucleus.Application.Dto;

public abstract class AuditedEntityDto : CreationAuditedEntityDto, IAudited
{
    public virtual DateTime? ModificationTime { get; set; }
    
    public virtual Guid? ModifierUserId { get; set; }
}
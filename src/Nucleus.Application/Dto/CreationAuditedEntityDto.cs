using Nucleus.Domain.Entities.Auditing;

namespace Nucleus.Application.Dto;

public abstract class CreationAuditedEntityDto : EntityDto, ICreationAudited
{
    public virtual DateTime CreationTime { get; set; }

    public virtual Guid? CreatorUserId { get; set; }
}
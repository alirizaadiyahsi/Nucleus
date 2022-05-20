namespace Nucleus.Domain.Entities.Auditing;

public interface ICreationAudited : IHasCreationTime
{
    Guid? CreatorUserId { get; set; }
}

public interface ICreationAudited<TUser> : ICreationAudited
{
    TUser CreatorUser { get; set; }
}
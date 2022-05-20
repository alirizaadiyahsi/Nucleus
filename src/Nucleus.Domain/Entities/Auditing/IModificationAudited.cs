namespace Nucleus.Domain.Entities.Auditing;

public interface IModificationAudited : IHasModificationTime
{
    Guid? ModifierUserId { get; set; }
}

public interface IModificationAudited<TUser> : IModificationAudited
{
    TUser ModifierUser { get; set; }
}
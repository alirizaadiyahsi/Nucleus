namespace Nucleus.Domain.Entities.Auditing;

public interface IDeletionAudited : IHasDeletionTime, ISoftDelete
{
    Guid? DeleterUserId { get; set; }
}

public interface IDeletionAudited<TUser> : IDeletionAudited
{
    TUser DeleterUser { get; set; }
}
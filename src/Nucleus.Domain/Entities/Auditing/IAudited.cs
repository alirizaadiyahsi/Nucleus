namespace Nucleus.Domain.Entities.Auditing;

public interface IAudited : ICreationAudited, IModificationAudited
{

}

public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
{

}
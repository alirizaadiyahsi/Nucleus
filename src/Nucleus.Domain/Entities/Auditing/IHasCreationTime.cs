namespace Nucleus.Domain.Entities.Auditing;

public interface IHasCreationTime
{
    DateTime CreationTime { get; set; }
}
namespace Nucleus.Domain.Entities.Auditing;

public interface IHasModificationTime
{
    DateTime? ModificationTime { get; set; }
}
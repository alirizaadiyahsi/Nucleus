namespace Nucleus.Domain.Entities.Auditing;

public interface IHasDeletionTime
{
    DateTime? DeletionTime { get; set; }
}
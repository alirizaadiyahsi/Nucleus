using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Nucleus.Domain.Entities.Auditing;

namespace Nucleus.Domain.Entities.Authorization;

public class Role : IdentityRole<Guid>, IFullAudited
{
    public bool IsSystemDefault { get; set; } = false;

    public DateTime CreationTime { get; set; }

    public Guid? CreatorUserId { get; set; }

    public DateTime? ModificationTime { get; set; }

    public Guid? ModifierUserId { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeleterUserId { get; set; }

    public DateTime? DeletionTime { get; set; }

    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; }

    [ForeignKey("ModifierUserId")]
    public virtual User ModifierUser { get; set; }

    [ForeignKey("DeleterUserId")]
    public virtual User DeleterUser { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
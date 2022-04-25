using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Common.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

/// <summary>
/// Application user.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Full name.
    /// </summary>
    public string FullName => StringUtils.JoinIgnoreEmpty(" ", LastName, FirstName);

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime? BirthDay { get; set; }

    /// <summary>
    /// Date of user creation.
    /// </summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>
    /// ID of <inheritdoc cref="Creator"/>.
    /// </summary>
    [ForeignKey("Creator")]
    public int? CreatorId { get; set; }

    /// <summary>
    /// Created by.
    /// </summary>
    public User Creator { get; set; }

    /// <summary>
    /// Last date of modify UTC.
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// ID of <inheritdoc cref="ModifiedBy"/>.
    /// </summary>
    [ForeignKey("ModifiedBy")]
    public int? ModifiedById { get; set; }

    /// <summary>
    /// Last modifier.
    /// </summary>
    public User ModifiedBy { get; set; }

    /// <summary>
    /// Date of deletion.
    /// </summary>
    public DateTime? RevokedOn { get; set; }

    /// <summary>
    /// ID of <inheritdoc cref="RevokedBy"/>.
    /// </summary>
    [ForeignKey("RevokedBy")]
    public int? RevokedById { get; set; }

    /// <summary>
    /// Revoked by.
    /// </summary>
    public User RevokedBy { get; set; }

    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}

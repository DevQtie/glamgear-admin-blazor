using System.ComponentModel.DataAnnotations;
using GlamGearAdmin.Models.AppModels;

namespace GlamGearAdmin.Models.SQLite;

public class Admin
{
    [Key]
    public string? UserID { get; set; }
    public string? Username { get; set; }
    [Required]
    [StringLength(100)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    public string? GivenName { get; set; }
    public string? MiddleName { get; set; }
    [Required]
    [StringLength(100)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    public string? FamilyName { get; set; }
    [Required]
    [StringLength(150, MinimumLength = 11)]
    public string? EmailAdd { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$")]
    public string? RoleType { get; set; }
    public NavSubmenu ActiveSubmenu { get; set; } = NavSubmenu.None;
}
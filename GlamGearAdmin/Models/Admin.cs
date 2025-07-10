using System.ComponentModel.DataAnnotations;

namespace GlamGearAdmin.Models;

public class Admin
{
    [Key]
    public string? UserID { get; set; }
    public string? Username { get; set; }
    public string? GivenName { get; set; }
    public string? MiddleName { get; set; }
    public string? FamilyName { get; set; }
    public string? EmailAdd { get; set; }
    public string? RoleType { get; set; }
}
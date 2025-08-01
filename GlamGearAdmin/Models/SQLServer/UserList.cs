using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

[Keyless]
public class UserListDM
{
  [Column("#")]
  public long Hash { get; set; }

  [Column("user_id")]
  public string? UserID { get; set; }

  [Column("role_type")]
  public string? RoleType { get; set; }

  [Column("pending_ver")]
  public bool PendingVer { get; set; }

  [Column("is_verified")]
  public bool IsVerified { get; set; }

  [Column("disp_name")]
  public string? DispName { get; set; }

  [Column("remarks")]
  public string? Remarks { get; set; }

  [Column("dt_registered")]
  public string? DtRegistered { get; set; }
}

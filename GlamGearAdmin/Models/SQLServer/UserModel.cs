using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

#region User list

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

[Keyless]
public class UserListSingleDM
{
  [Required]
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

  [Column("username")]
  public string? Username { get; set; }

  [Column("full_n")]
  public string? Fullname { get; set; }

  [Column("gender")]
  public string? Gender { get; set; }

  [Column("birthdate")]
  public string? Birthdate { get; set; }

  [Column("nationality")]
  public string? Nationality { get; set; }

  [Column("country")]
  public string? Country { get; set; }

  [Column("province")]
  public string? Province { get; set; }

  [Column("city_mun")]
  public string? CityMun { get; set; }

  [Column("brgy")]
  public string? Brgy { get; set; }

  [Column("unit_h_bldg_st")]
  public string? UnitHBldgSt { get; set; }

  [Column("vill_sub")]
  public string? VillSub { get; set; }

  [Column("zip_code")]
  public string? ZipCode { get; set; }

  [Column("source_of_fund")]
  public string? SourceOfFund { get; set; }

  [Column("emp_status")]
  public string? EmpStat { get; set; }

  [Column("employer")]
  public string? Employer { get; set; }

  [Column("occupation")]
  public string? Occupation { get; set; }

  [Column("remarks")]
  public string? Remarks { get; set; }

  [Column("dt_registered")]
  public string? DtRegistered { get; set; }

  [Column("dt_modified")]
  public string? DtModified { get; set; }

  [Column("verified_by")]
  public string? VerifiedBy { get; set; }
}

[Keyless]
public class UserListImageSingleDM
{
  [Column("fid")]
  public byte[]? FrontID { get; set; }

  [Column("bid")]
  public byte[]? BackID { get; set; }

  [Column("slf")]
  public byte[]? Selfie { get; set; }
}

#endregion User list

[Keyless]
public class UserRoles
{
  [Column("role_id")]
  public string? RoleID { get; set; }

  [Column("role_type")]
  public string? RoleType { get; set; }

  [Column("is_active")]
  public bool IsActive { get; set; }

}

[Keyless]
public class UserRemarks
{
  [Column("rem_ref_id")]
  public string? RemarkID { get; set; }

  [Column("remarks")]
  public string? Remarks { get; set; }
}
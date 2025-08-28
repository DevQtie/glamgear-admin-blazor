using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

[Keyless]
public class SimulateKYCImg
{
  [Required]
  public string? FrontImg { get; set; }

  public string? BackImg { get; set; }

  [Required]
  public string? SelfieImg { get; set; }
}

[Keyless]
public class SimulateProductImg
{
  [Required]
  public List<(string img, string name, long size)> ProductImgList { get; set; } = [];
}

[Keyless]
public class SimulateTestImg
{
  [Required]
  public List<(string img, string name, long size)> TestImgList { get; set; } = [];
}

[Keyless]
public class VerifyUser
{
  public string? SelectedRoleID { get; set; }

  public string? SelectedRole { get; set; }

  public string? SelectedRemarkID { get; set; }

  public string? SelectedRemark { get; set; }
}

#region ValidateComplexType; for reference only, but I decided to keep it and use it ;)

[Keyless]
public class VerifyRemRoleUser
{
  [Required]
  [ValidateComplexType]
  public UserListSingleDM UserListSingleDM { get; set; }

  public string? SelectedRoleID { get; set; }

  public string? SelectedRole { get; set; }

  public string? SelectedRemarkID { get; set; }

  public string? SelectedRemark { get; set; }

  public VerifyRemRoleUser() // for demonstration purposes only XD, I observed that it is not currently in use.
  {
    UserListSingleDM = new UserListSingleDM();
  }
}

#endregion ValidateComplexType
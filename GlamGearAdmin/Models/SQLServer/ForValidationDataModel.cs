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
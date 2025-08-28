using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

[Keyless]
public class ProductList
{
  [Column("#")]
  public long Hash { get; set; }

  [Column("prod_id")]
  public string? ProdID { get; set; }

  [Column("prod_data")]
  public byte[]? ProdData { get; set; }

  [Column("img_name")]
  public string? ImgName { get; set; }

  [Column(TypeName = "decimal(15, 2)")]
  public decimal Size { get; set; }

  [Column("dt_stamp")]
  public string? DtStamp { get; set; }
}

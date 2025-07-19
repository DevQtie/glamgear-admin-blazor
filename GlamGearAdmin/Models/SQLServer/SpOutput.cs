using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamGearAdmin.Models.SQLServer;

public class SqlOutput
{
  [Key]
  [Column("SP_OUTPUT")]
  public string SpOutput { get; set; } = string.Empty;
}

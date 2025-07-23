using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

[Keyless]
public class SqlOutput
{
  [Column("SP_OUTPUT")]
  public string? SpOutput { get; set; }
}

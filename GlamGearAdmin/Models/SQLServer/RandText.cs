using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GlamGearAdmin.Models.SQLServer;

public class RandText
{
  [Key]
  public int Id { get; set; }
  [Required]
  [StringLength(50)]
  [Column("random_text")]
  public string? RandomText { get; set; }
  [Column("dt_stamp")]
  public DateTime DtStamp { get; set; }
}
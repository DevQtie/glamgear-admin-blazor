using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Models.SQLServer;

[Keyless]
public class ProductPlainTextList // TODO: render plaintext first, then image, separate the image property to another model
{
  [Column("#")]
  public long Hash { get; set; }

  [Column("prod_id")]
  public string? ProdID { get; set; }

  [Column("prod_name")]
  public string? ProdName { get; set; }

  [Column(name: "orig_price", TypeName = "decimal(10, 2)")]
  public decimal OrigPrice { get; set; }

  [Column(name: "disc_price", TypeName = "decimal(10, 2)")]
  public decimal DiscPrice { get; set; }

  [Column("dis_perc")]
  public string? DiscPercent { get; set; }

  [Column("stock")]
  public string? Stock { get; set; }

  [Column("tags")]
  public string? PromoTags { get; set; }

  [Column("img")]
  public byte[]? ProdImg { get; set; }   // nullable, so you can still fetch text-only without populating

}

[Keyless]
public class ProductImgList
{
  [Column("prod_id")]
  public string? ProdID { get; set; }

  [Column("img")]
  public byte[]? ProdImg { get; set; }
}

[Keyless]
public class ProductMainForReview
{
  [Required]
  [Column("prod_name")]
  [StringLength(255)]
  public string? ProdName { get; set; }

  [Column(name: "orig_price", TypeName = "decimal(10, 2)")]
  public decimal OrigPrice { get; set; }

  [Column(name: "disc_price", TypeName = "decimal(10, 2)")]
  public decimal DiscPrice { get; set; }

  [Column("dis_perc")]
  public string? DiscPercent { get; set; }

  [Column("stock")]
  public int? Stock { get; set; }

  [Column("availability")]
  public bool Availability { get; set; }
}

[Keyless]
public class ProductPromoTagFR // for review (FR)
{
  [Column("promo_tag_id")]
  public string? PromoID { get; set; }

  [Column("tag_values")]
  public string TagValues { get; set; } = string.Empty;
}

[Keyless]
public class ProductSpecsFR // for review (FR)
{
  [Column("p_key_name_id")]
  public string? KeyNameID { get; set; }

  [Column("key_name")]
  public string? KeyName { get; set; }

  [Column("value")]
  public string? Value { get; set; }
}

[Keyless]
public class ProductVariantsFR // for review (FR)
{
  [Column("pv_id")]
  public string? ProdVarID { get; set; }

  [Column(name: "orig_price", TypeName = "decimal(10, 2)")]
  public decimal OrigPrice { get; set; }

  [Column(name: "disc_price", TypeName = "decimal(10, 2)")]
  public decimal DiscPrice { get; set; }

  [Column("dis_perc")]
  public string? DiscPercent { get; set; }

  [Column(name: "vat", TypeName = "decimal(10, 2)")]
  public decimal VAT { get; set; }

  [Column("stock")]
  public int? Stock { get; set; }

  [Column("availability")]
  public bool? Availability { get; set; }

  [Column("user_id_mod")]
  public string? UserIDModifier { get; set; }
}

[Keyless]
public class ProductVariantSpecsFR // for review (FR)
{
  [Column("pv_keyval_id")]
  public string? PVKeyValID { get; set; }

  [Column("pv_key_name_id")]
  public string? PVKeyNameID { get; set; }

  [Column("key_name")]
  public string? KeyName { get; set; }

  [Column("value")]
  public string? Value { get; set; }
}

[Keyless]
public class ProductImgFR // for review (FR)
{
  [Column("prod_img_id")]
  public string? ProdID { get; set; }

  [Column("img_data")]
  public byte[]? ProdImg { get; set; }

  [Column(name: "img_f_kbsize", TypeName = "decimal(10, 3)")]
  public decimal VAT { get; set; }
}

[Keyless]
public class ProductVarImgFR // for review (FR)
{
  [Column("pv_img_id")]
  public string? ProdID { get; set; }

  [Column("img_data")]
  public byte[]? ProdImg { get; set; }

  [Column(name: "img_f_kbsize", TypeName = "decimal(10, 3)")]
  public decimal VAT { get; set; }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ProductPlainTextList))]
internal partial class SourceGenerationContext : JsonSerializerContext // discontinued because DevQt faced some issues.
{
}

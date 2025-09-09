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

[Keyless]
public class ReviewThenModifyProduct
{
  [Required]
  [ValidateComplexType]
  public CompileProductProperties CompileProductProperties { get; set; }
  public int SelectedCategoryID { get; set; }
  public string? SelectedCategory { get; set; }
  public (int key, object value, object? status)[] ProdCategory = [(1, "Jewelry", null), (2, "Luxury Bag", null), (3, "Gadget", null)];
  public ReviewThenModifyProduct()
  {
    CompileProductProperties = new CompileProductProperties();
  }
}

[Keyless]
public class CompileProductProperties
{
  public ProductMainForReview? ProductMainForReview { get; set; }
  public ProductPromoTagFR? ProductPromoTagFR { get; set; }
  public ProductSpecsFR? ProductSpecsFR { get; set; }
  public ProductVariantsFR? ProductVariantsFR { get; set; }
  public ProductVariantSpecsFR? ProductVariantSpecsFR { get; set; }
  public ProductImgFR? ProductImgFR { get; set; }
  public ProductVarImgFR? ProductVarImgFR { get; set; }
  public List<ProductCategory> ProductCategory { get; set; } =
  [
    new() { ID = 1, Item = "Jewelry", State = false },
    new() { ID = 2, Item = "Luxury bag", State = false },
    new() { ID = 3, Item = "Gadget", State = false }
];
}

public class ProductCategory
{
  public int ID { get; set; }
  public string? Item { get; set; }
  public bool State { get; set; }
}

#endregion ValidateComplexType
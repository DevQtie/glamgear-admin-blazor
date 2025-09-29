using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Components;
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
    public int SelectedWarrantyTypeID { get; set; }
    public string? SelectedWarrantyType { get; set; }
    public string? ProdNameEntry { get; set; }
    public int ProdNameLC { get; set; }
    public int ProdStock { get; set; }
    public string ImageNotes { get; set; } = "1. Maximum 8 images can be uploaded.<br>2. Image size between 330x330 and 5000x5000 px. Max file size: 3MB.<br>3. Obscene image is strictly prohibited.";
    public string? TagRef { get; set; }

    [Parameter]
    public EventCallback<decimal> ValueChanged { get; set; }

    public string FormattedOrigPrice
    {
        get => CompileProductProperties.ProductMainForReview?.OrigPriceFormatted ?? "0.00"; // e.g., 1,000.00
        set => ParseAndSetOrigPrice(value);
    }

    public string FormattedDiscPrice
    {
        get => CompileProductProperties.ProductMainForReview?.DiscPriceFormatted ?? "0.00"; // e.g., 1,000.00
        set => ParseAndSetDiscPrice(value);
    }
    public void ParseAndSetOrigPrice(string input)
    {
        var cleaned = input.Replace(",", "");
        if (decimal.TryParse(cleaned, out var parsed))
        {
            CompileProductProperties.ProductMainForReview!.OrigPrice = parsed;
            ValueChanged.InvokeAsync(CompileProductProperties.ProductMainForReview.OrigPrice);
        }
    }
    public void ParseAndSetDiscPrice(string input)
    {
        var cleaned = input.Replace(",", "");
        if (decimal.TryParse(cleaned, out var parsed))
        {
            CompileProductProperties.ProductMainForReview!.DiscPrice = parsed;
            ValueChanged.InvokeAsync(CompileProductProperties.ProductMainForReview.DiscPrice);
        }
    }
    public ReviewThenModifyProduct()
    {
        CompileProductProperties = new CompileProductProperties();
    }
}

[Keyless]
public class CompileProductProperties
{
    public List<ProductPromoTagFR> Tags { get; set; } = [];
    public List<ProductPromoTagRefFR> TagRefList { get; set; } = [];
    public List<ProductImgFR> ProductImages { get; set; } = [];
    public List<ProductSpecsFR> ProductSpecs { get; set; } = [];
    public ProductDescription? ProductDescription { get; set; }
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
    public List<ProductWarrantyType> ProductWarrantyType { get; set; } =
    [
      new() { ID = 1, Item = "No Warranty", State = false },
    new() { ID = 2, Item = "NA", State = false }
    ];
}

public class ProductCategory
{
    public int ID { get; set; }
    public string? Item { get; set; }
    public bool State { get; set; }
}

public class ProductWarrantyType
{
    public int ID { get; set; }
    public string? Item { get; set; }
    public bool State { get; set; }
}

public class SpecsValidator : AbstractValidator<ProductSpecsFR>
{
    public SpecsValidator()
    {
        this.RuleFor(x => x.Value)
            .Custom(
                (value, context) =>
                {
                    if (string.IsNullOrWhiteSpace(value)) // Not limited to emptiness of the field
                    {
                        context.AddFailure($"{context.InstanceToValidate.Value} This field is required."); // Refactor this based on the type of validation check
                    }
                });
    }
}

public class SpecsContainerValidator : AbstractValidator<ReviewThenModifyProduct>
{
    public SpecsContainerValidator()
    {
        this.RuleForEach(x => x.CompileProductProperties.ProductSpecs).SetValidator(new SpecsValidator());
    }
}

#endregion ValidateComplexType
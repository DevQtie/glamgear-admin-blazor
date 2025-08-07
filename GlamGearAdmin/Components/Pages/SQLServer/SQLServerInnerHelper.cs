using System.Data;

static class SQLServerInnerHelper
{
  #region File Upload to database

  public static byte[]? TryExtractImageBytes(string? dataUri)
  {
    if (string.IsNullOrEmpty(dataUri) || !dataUri.Contains(','))
      return null;

    try
    {
      string base64 = dataUri[(dataUri.IndexOf(',') + 1)..];
      return Convert.FromBase64String(base64);
    }
    catch (FormatException)
    {
      // Log or handle invalid Base64 format
      return null;
    }
  }

  #endregion File Upload to database
  #region rpiAPSM_spManageUsersDataWOutput
  // Should be in the same chronological order
  public static Dictionary<string, object?> ManageUsersDataWOutputPageParams(
    object? userID = null,
    object? frontID = null,
    object? backID = null,
    object? selfie = null,
    object? functionKey = null,
    object? spOutput = null) // optional parameters
  {
    var pageParam = new Dictionary<string, object?>
    {
      ["user_id"] = userID,
      ["f_id"] = frontID,
      ["b_id"] = backID,
      ["selfie"] = selfie,
      ["function_key"] = functionKey,
      ["sp_output"] = spOutput
    };
    return pageParam;
  }

  public static (string Name, SqlDbType Type, int? Size)[] ManageUsersDataWithoutOutputParams()
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
        ("user_id", SqlDbType.VarChar, 50),
        ("f_id", SqlDbType.VarBinary, -1),
        ("b_id", SqlDbType.VarBinary, -1),
        ("selfie", SqlDbType.VarBinary, -1),
        ("function_key", SqlDbType.VarChar, 100),
        ("sp_output", SqlDbType.NVarChar, 100),
    };
    return paramDefs;
  }

  public static (string Name, SqlDbType Type, int? Size)[] ManageUsersDataWithOutputParams()
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
        ("user_id", SqlDbType.VarChar, 50),
        ("f_id", SqlDbType.VarBinary, -1),
        ("b_id", SqlDbType.VarBinary, -1),
        ("selfie", SqlDbType.VarBinary, -1),
        ("function_key", SqlDbType.VarChar, 100),
    };
    return paramDefs;
  }
  #endregion  rpiAPSM_spManageUsersDataWOutput

  #region rpiAPSM_spManageAdminProductsWOutput
  // Should be in the same chronological order
  public static Dictionary<string, object?> ManageAdminProductsWOutputPageParams(
    object? prodID = null,
    object? prodImg = null,
    object? imgName = null,
    object? size = null,
    object? functionKey = null,
    object? spOutput = null) // optional parameters
  {
    var pageParam = new Dictionary<string, object?>
    {
      ["prod_id"] = prodID,
      ["prod_img"] = prodImg,
      ["img_name"] = imgName,
      ["size"] = size,
      ["function_key"] = functionKey,
      ["sp_output"] = spOutput
    };
    return pageParam;
  }

  public static (string Name, SqlDbType Type, int? Size, byte? Precision, byte? Scale)[] ManageAdminProductsWithoutOutputParams()
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size, byte? Precision, byte? Scale)[]
    {
        ("prod_id", SqlDbType.VarChar, 50, null, null), // if size is not null, the precision and scale should be null.
        ("prod_img", SqlDbType.VarBinary, -1, null, null), // if size is not null, the precision and scale should be null.
        ("img_name", SqlDbType.VarChar, 255, null, null), // if size is not null, the precision and scale should be null.
        ("size", SqlDbType.Decimal, null, 15, 2),  // if precision is not null, the scale should be `0 <= value`, and the size should be null.
        ("function_key", SqlDbType.VarChar, 100, null, null), // if size is not null, the precision and scale should be null.
        ("sp_output", SqlDbType.NVarChar, 100, null, null), // if size is not null, the precision and scale should be null.
    };
    return paramDefs;
  }

  public static (string Name, SqlDbType Type, int? Size, byte? Precision, byte? Scale)[] ManageAdminProductsWithOutputParams()
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size, byte? Precision, byte? Scale)[]
    {
        ("prod_id", SqlDbType.VarChar, 50, null, null), // if size is not null, the precision and scale should be null.
        ("prod_img", SqlDbType.VarBinary, -1, null, null), // if size is not null, the precision and scale should be null.
        ("img_name", SqlDbType.VarChar, 255, null, null), // if size is not null, the precision and scale should be null.
        ("size", SqlDbType.Decimal, null, 15, 2),  // if precision is not null, the scale should be `0 <= value`, and the size should be null.
        ("function_key", SqlDbType.VarChar, 100, null, null), // if size is not null, the precision and scale should be null.
    };
    return paramDefs;
  }

  #endregion  rpiAPSM_spManageAdminProductsWOutput
}
using System.Data;

static class SQLServerInnerHelper
{
  #region rpiAPSM_spManageUsersDataWOutput
  // Should be in the same chronological order
  public static Dictionary<string, object?> RpiAPSMSpManageUsersDataWOutputPageParams(object? userID = null, object? functionKey = null, object? spOutput = null) // optional parameters
  {
    var pageParam = new Dictionary<string, object?>
    {
      ["user_id"] = userID,
      ["function_key"] = functionKey,
      ["sp_output"] = spOutput
    };
    return pageParam;
  }
  public static (string Name, SqlDbType Type, int? Size)[] RpiAPSMSpManageUsersDataWOutputParams()
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
        ("user_id", SqlDbType.VarChar, 100),
        ("function_key", SqlDbType.VarChar, 100),
        ("sp_output", SqlDbType.NVarChar, 100)
    };
    return paramDefs;
  }
  #endregion  rpiAPSM_spManageUsersDataWOutput
}
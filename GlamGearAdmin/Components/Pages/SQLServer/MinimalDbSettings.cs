using Microsoft.Data.SqlClient;

static class MinimalDbSettings
{
  #region FOR REFERENCE ONLY
  public static object[] FromSqlRawParamsObject(string[] paramNames, object?[] paramValues)
  {
    if (paramValues.Length != paramNames.Length)
      throw new ArgumentException("Number of names and values must match.");

    return [.. paramNames.Select((name, i) =>
        new SqlParameter(name, paramValues[i] ?? DBNull.Value)
    )];
  } // working method for reference; not currently in use

  public static string FromSqlRawSQL(string procName, string[] parameters)
  {
    return $"EXEC {procName} {string.Join(", ", parameters)}";
  } // working method for reference; not currently in use

  public static FormattableString FromSqlSQLParamStatic(string storedProcedure, string[] paramName, params object?[] parameters)
  {
    if (paramName.Length != parameters.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    SqlParameter[] sqlParameterparam =
    [
      new SqlParameter(paramName[0], parameters[0] ?? DBNull.Value),
      new SqlParameter(paramName[1], parameters[1] ?? DBNull.Value),
      new SqlParameter(paramName[2], parameters[2] ?? DBNull.Value),
    ];
    return $"EXEC {storedProcedure} {sqlParameterparam[0]}, {sqlParameterparam[1]}, {sqlParameterparam[2]}";
  } // manual approach reference
  #endregion

  public static FormattableString FromSqlSQLParamDynamic(string storedProcedure, string[] paramName, params object?[] parameters)
  {
    if (paramName.Length != parameters.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    SqlParameter[] sqlParameterparam =
    [
      new SqlParameter(paramName[0], parameters[0] ?? DBNull.Value),
      new SqlParameter(paramName[1], parameters[1] ?? DBNull.Value),
      new SqlParameter(paramName[2], parameters[2] ?? DBNull.Value),
    ];
    return $"EXEC {storedProcedure} {sqlParameterparam[0]}, {sqlParameterparam[1]}, {sqlParameterparam[2]}";
  }
}
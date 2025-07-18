using System.Data;
using Microsoft.Data.SqlClient;

static class MinimalDbSettings
{
  #region FOR REFERENCE ONLY
  public static object[] FromSqlRawParamsObject(string[] paramNames, object?[] paramValues)
  {
    return [.. paramNames.Select((name, i) =>
        new SqlParameter(name, paramValues[i] ?? DBNull.Value)
    )];
  } // working method for reference; not currently in use

  public static string FromSqlRawSQL(string spName, string[] parameters)
  {
    return $"EXEC {spName} {string.Join(", ", parameters)}";
  } // working method for reference; not currently in use

  public static FormattableString FromSqlSQLParamStatic(string storedProcedure, string[] paramName, params object?[] parameters)
  {
    SqlParameter[] sqlParameterparam =
    [
      new SqlParameter(paramName[0], parameters[0] ?? DBNull.Value),
      new SqlParameter(paramName[1], parameters[1] ?? DBNull.Value),
      new SqlParameter(paramName[2], parameters[2] ?? DBNull.Value),
    ];
    return $"EXEC {storedProcedure} {sqlParameterparam[0]}, {sqlParameterparam[1]}, {sqlParameterparam[2]}";
  } // manual approach reference

  public static FormattableString FromSqlSQLParamStaticWType(string spName, string[] paramNames, params object?[] parameters)
  { // reference: https://copilot.microsoft.com/shares/5sStMrEhNoKhRmy3beffQ
    SqlParameter[] sqlParameterparam =
    [
      new SqlParameter(paramNames[0], SqlDbType.Int)
      {
          Value = parameters[0] ?? DBNull.Value
      },
      new SqlParameter(paramNames[1], SqlDbType.VarChar, 50)
      {
          Value = parameters[1] ?? DBNull.Value
      },
      new SqlParameter(paramNames[2], SqlDbType.VarChar, 100)
      {
          Value = parameters[2] ?? DBNull.Value
      }
    ];

    FormattableString sqlQuery = $"EXEC {spName} {sqlParameterparam[0]}, {sqlParameterparam[1]}, {sqlParameterparam[2]}";

    return sqlQuery;
  }

  public static FormattableString FromSqlSQLParamStaticWOType(string spName, SqlParameter[] sqlParameter)
  {
    return $"EXEC {spName} {sqlParameter[0]}, {sqlParameter[1]}, {sqlParameter[2]}";
  }
  #endregion

  #region PRIMARY BUSINESS LOGIC
  public static SqlParameter[] FromSqlSQLParamArrayOfTuples((string Name, SqlDbType Type, int? Size)[] paramDefs, params object?[] parameters)
  {
    return [.. paramDefs
        .Select((def, i) =>
        {
          var param = def.Size is null
          ? new SqlParameter(def.Name, def.Type)
          : new SqlParameter(def.Name, def.Type, def.Size.Value);

          param.Value = parameters[i] ?? DBNull.Value;
          return param;
        })];
  }

  public static FormattableString FromSqlSQLParamLessDynamic(string storedProcedure, SqlParameter[] sqlParameter)
  {
    return sqlParameter.Length switch
    {
      // 0 => $"EXEC {storedProcedure}", // this isn't allowed here.
      1 => $"EXEC {storedProcedure} {sqlParameter[0]}",
      2 => $"EXEC {storedProcedure} {sqlParameter[0]}, {sqlParameter[1]}",
      3 => $"EXEC {storedProcedure} {sqlParameter[0]}, {sqlParameter[1]}, {sqlParameter[2]}",
      // ... you can add more here...
      _ => throw new ArgumentException("Too many parameters provided."),
    };
  }
  #endregion
}
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

  public static FormattableString FromSqlSQLParamLessDynamic(string storedProcedure, SqlParameter[] sqlParameters)
  {
    return sqlParameters.Length switch
    {
      // 0 => $"EXEC {storedProcedure}", // this isn't allowed here.
      1 => $"EXEC {storedProcedure} {sqlParameters[0]}",
      2 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}",
      3 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}",
      4 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}",
      5 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}",
      6 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}",
      7 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}",
      8 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}",
      9 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}",
      10 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}",
      11 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}",
      12 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}",
      13 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}",
      14 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}",
      15 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}",
      16 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}",
      17 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}",
      18 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}",
      19 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}",
      20 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}",
      21 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}",
      22 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}",
      23 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}",
      24 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}",
      25 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}",
      26 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}",
      27 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}",
      28 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}",
      29 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}",
      30 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}",
      31 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}",
      32 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}",
      33 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}",
      34 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}",
      35 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}",
      36 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}",
      37 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}",
      38 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}",
      39 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}, {sqlParameters[38]}",
      40 => $"EXEC {storedProcedure} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}, {sqlParameters[38]}, {sqlParameters[39]}",
      // ... you can add more here...
      _ => throw new ArgumentException("Too many parameters provided."),
    };
  }
  #endregion
}
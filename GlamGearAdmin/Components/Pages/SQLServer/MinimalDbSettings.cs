using System.Data;
using System.Runtime.CompilerServices;
using GlamGearAdmin.Data.SQLServer;
using Microsoft.Build.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

  public static FormattableString FromSqlSQLParamStatic(string spName, string[] paramName, params object?[] parameters)
  {
    SqlParameter[] sqlParameterparam =
    [
      new SqlParameter(paramName[0], parameters[0] ?? DBNull.Value),
      new SqlParameter(paramName[1], parameters[1] ?? DBNull.Value),
      new SqlParameter(paramName[2], parameters[2] ?? DBNull.Value),
    ];
    return $"EXEC {spName} {sqlParameterparam[0]}, {sqlParameterparam[1]}, {sqlParameterparam[2]}";
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

  public static async Task<string?> UsingCommandRef(BlazorSQLServerContext context, string spName)
  {
    /* In the requestor page origin, always ensure that this `await context.DisposeAsync();` is defined.
    Otherwise, close the connection here (but since there is already been defined, separate a method for that instance).*/
    using var command = context.Database.GetDbConnection().CreateCommand();
    command.CommandText = $"{spName}";
    command.CommandType = CommandType.StoredProcedure;

    // Input parameter
    var userParam = new SqlParameter("@filterByUser", "johndoe");
    command.Parameters.Add(userParam);

    // Output parameter
    var outputParam = new SqlParameter("@MsgOutput", SqlDbType.NVarChar, 100)
    {
      Direction = ParameterDirection.Output
    };
    command.Parameters.Add(outputParam);

    await context.Database.OpenConnectionAsync();
    using var reader = await command.ExecuteReaderAsync();
    // Map the result to your entity manually (optional)

    var message = outputParam.Value?.ToString();
    return message;
  }

  public static async Task<string?> UsingCommandWoOutput(BlazorSQLServerContext context, string spName, SqlParameter[] sqlParameter)
  {
    /* In the requestor page origin, always ensure that this `await context.DisposeAsync();` is defined.
    Otherwise, close the connection here (but since there is already been defined, separate a method for that instance).*/
    using var command = context.Database.GetDbConnection().CreateCommand();
    command.CommandText = $"{spName}";
    command.CommandType = CommandType.StoredProcedure;

    // Input parameter
    for (int i = 0; i < sqlParameter.Length; i++)
    {
      command.Parameters.Add(sqlParameter[i]);
    }

    await context.Database.OpenConnectionAsync();
    using var reader = await command.ExecuteReaderAsync();
    // Map the result to your entity manually (optional)
    string? spOutput;
    if (await reader.ReadAsync())
    {
      spOutput = reader["SP_OUTPUT"].ToString();
    }
    else
    {
      spOutput = null; // No rows returned
    }
    return spOutput;
  } // NOT WORKING RIGHT...
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

  public static FormattableString FromSqlSQLParamLessDynamic(string spName, SqlParameter[] sqlParameters)
  {
    return sqlParameters.Length switch
    {
      // 0 => $"EXEC {storedProcedure}", // this isn't allowed here.
      1 => $"EXEC {spName} {sqlParameters[0]}",
      2 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}",
      3 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}",
      4 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}",
      5 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}",
      6 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}",
      7 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}",
      8 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}",
      9 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}",
      10 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}",
      11 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}",
      12 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}",
      13 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}",
      14 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}",
      15 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}",
      16 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}",
      17 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}",
      18 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}",
      19 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}",
      20 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}",
      21 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}",
      22 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}",
      23 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}",
      24 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}",
      25 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}",
      26 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}",
      27 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}",
      28 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}",
      29 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}",
      30 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}",
      31 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}",
      32 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}",
      33 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}",
      34 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}",
      35 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}",
      36 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}",
      37 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}",
      38 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}",
      39 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}, {sqlParameters[38]}",
      40 => $"EXEC {spName} {sqlParameters[0]}, {sqlParameters[1]}, {sqlParameters[2]}, {sqlParameters[3]}, {sqlParameters[4]}, {sqlParameters[5]}, {sqlParameters[6]}, {sqlParameters[7]}, {sqlParameters[8]}, {sqlParameters[9]}, {sqlParameters[10]}, {sqlParameters[11]}, {sqlParameters[12]}, {sqlParameters[13]}, {sqlParameters[14]}, {sqlParameters[15]}, {sqlParameters[16]}, {sqlParameters[17]}, {sqlParameters[18]}, {sqlParameters[19]}, {sqlParameters[20]}, {sqlParameters[21]}, {sqlParameters[22]}, {sqlParameters[23]}, {sqlParameters[24]}, {sqlParameters[25]}, {sqlParameters[26]}, {sqlParameters[27]}, {sqlParameters[28]}, {sqlParameters[29]}, {sqlParameters[30]}, {sqlParameters[31]}, {sqlParameters[32]}, {sqlParameters[33]}, {sqlParameters[34]}, {sqlParameters[35]}, {sqlParameters[36]}, {sqlParameters[37]}, {sqlParameters[38]}, {sqlParameters[39]}",
      // ... you can add more here...
      _ => throw new ArgumentException("Too many parameters provided."),
    };
  }

  public static FormattableString FromSqlSQLParamDynamic(string spName, SqlParameter[] sqlParameters)
  {
    var format = "EXEC {0} " + string.Join(", ", sqlParameters.Select((p, index) => $"@{p.ParameterName.TrimStart('@')} = {{{index + 1}}}"));
    var parameters = sqlParameters.Cast<object>().Prepend(spName).ToArray();
    return FormattableStringFactory.Create(format, parameters);
  }

  public static async Task<string?> UsingCommand(BlazorSQLServerContext context, string spName, SqlParameter[] sqlParameter)
  {
    /* In the requestor page origin, always ensure that this `await context.DisposeAsync();` is defined.
    Otherwise, close the connection here (but since there is already been defined, separate a method for that instance).*/
    using var command = context.Database.GetDbConnection().CreateCommand();
    command.CommandText = $"{spName}";
    command.CommandType = CommandType.StoredProcedure;

    // Input parameter
    for (int i = 0; i < sqlParameter.Length; i++)
    {
      command.Parameters.Add(sqlParameter[i]);
    }

    // Output parameter
    var outputParam = new SqlParameter("sp_output", SqlDbType.NVarChar, 100)
    {
      Direction = ParameterDirection.Output
    };
    command.Parameters.Add(outputParam);

    await context.Database.OpenConnectionAsync();
    using var reader = await command.ExecuteReaderAsync();
    // Map the result to your entity manually (optional)
    reader.Close();
    string? result = outputParam.Value == DBNull.Value ? null : outputParam.Value?.ToString(); // I am getting empty string if I don't do this check.
    return result;
  }
  #endregion
}
using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Models.SQLServer;
using GlamGearAdmin.Data.SQLServer;
using System.Data;
using Microsoft.Data.SqlClient;

class SQLServerHelper(BlazorSQLServerContext context)
{
  private readonly BlazorSQLServerContext _context = context;

  /** Use AsNoTracking() for read-only queries to improve performance; always separate the read-only method with parameters from methods that need tracking.**/

  #region READ-ONLY (AsNoTracking) METHODS
  #region FOR REFERENCE ONLY
  public async Task<List<RandText>> GetRandTextFromSqlRawAsync(string storedProcedure, params object?[] parameters)
  {
    string[] paramNames = ["@id", "@random_text", "@function_key"];
    var sql = MinimalDbSettings.FromSqlRawSQL(storedProcedure, paramNames);
    var param = MinimalDbSettings.FromSqlRawParamsObject(paramNames, parameters);

    return await _context.RandText
        .FromSqlRaw(sql, param) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .AsNoTracking()
        .ToListAsync();
  } // working method for reference; not currently in use

  public async Task<List<RandText>> GetRandTextsFromSqlROAsync(string storedProcedure, params object?[] parameters)
  {
    SqlParameter[] sqlParameter =
    [
      new SqlParameter("id", SqlDbType.Int)
    {
        Value = parameters[0] ?? DBNull.Value
    },
    new SqlParameter("random_text", SqlDbType.VarChar, 50)
    {
        Value = parameters[1] ?? DBNull.Value
    },
    new SqlParameter("function_key", SqlDbType.VarChar, 100)
    {
      Value = parameters[2] ?? DBNull.Value
    }
    ];

    if (parameters.Length != sqlParameter.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    var sqlParam = MinimalDbSettings.FromSqlSQLParamStaticWOType(storedProcedure, sqlParameter);

    return await _context.RandText
        .FromSql(sqlParam) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .AsNoTracking() // please read for reference: [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
        .ToListAsync();
  }
  #endregion

  public async Task<List<RandText>> GetRandTextsWOParamAsync(string spName)
  {
    // use AsNoTracking for read-only queries to improve performance
    return await _context.RandText
        .FromSql($"EXEC {spName}") // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .AsNoTracking() // please read for reference: [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
        .ToListAsync();
  } // working method for reference; not currently in use

  public async Task<List<RandText>> GetRandTextsFromSqlReadOnlyAsync(string spName, params object?[] parameters)
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
        ("id", SqlDbType.Int, null),
        ("random_text", SqlDbType.VarChar, 50),
        ("function_key", SqlDbType.VarChar, 100)
    };

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuples(paramDefs, parameters);

    if (parameters.Length != sqlParameter.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    return await _context.RandText
        .FromSql(sqlParam) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .AsNoTracking() // please read for reference: [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
        .ToListAsync();
  }
  #endregion

  #region NON-READ-ONLY METHODS
  public async Task<List<RandText>> GetRandTextsFromSqlAsync(string storedProcedure, params object?[] parameters)
  {
    string[] paramNames = ["id", "random_text", "function_key"];

    if (parameters.Length != paramNames.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    var sqlParam = MinimalDbSettings.FromSqlSQLParamStatic(storedProcedure, paramNames, parameters);
    return await _context.RandText
        .FromSql(sqlParam) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .ToListAsync();
  }
  #endregion
}
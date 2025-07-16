using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Models.SQLServer;
using GlamGearAdmin.Data.SQLServer;

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
        .ToListAsync();
  } // working method for reference; not currently in use
  #endregion

  public async Task<List<RandText>> GetRandTextsAsync(FormattableString spName)
  {
    // use AsNoTracking for read-only queries to improve performance
    return await _context.RandText
        .FromSql(spName) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .AsNoTracking() // please read for reference: [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
        .ToListAsync();
  }
  public async Task<List<RandText>> GetRandTextsFromSqlROAsync(string storedProcedure, params object?[] parameters)
  {
    string[] paramNames = ["id", "random_text", "function_key"];

    if (parameters.Length != paramNames.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    Console.WriteLine($"SEE LOGS: {MinimalDbSettings.FromSqlRawSQL(storedProcedure, paramNames)}");

    var sqlParam = MinimalDbSettings.FromSqlSQLParamStatic(storedProcedure, paramNames, parameters);

    return await _context.RandText
        .FromSql(sqlParam) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
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

    Console.WriteLine($"SEE LOGS: {MinimalDbSettings.FromSqlRawSQL(storedProcedure, paramNames)}");

    var sqlParam = MinimalDbSettings.FromSqlSQLParamStatic(storedProcedure, paramNames, parameters);

    return await _context.RandText
        .FromSql(sqlParam) // The FromSql and FromSqlInterpolated methods are safe against SQL injection, and always integrate parameter data as a separate SQL parameter. To read more: [Passing parameters](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver#passing-parameters)
        .ToListAsync();
  }
  #endregion
}
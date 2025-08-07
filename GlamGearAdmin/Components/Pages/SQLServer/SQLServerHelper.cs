using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Models.SQLServer;
using GlamGearAdmin.Data.SQLServer;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

class SQLServerHelper(BlazorSQLServerContext context)
{
  private readonly BlazorSQLServerContext _context = context;

  /** Use AsNoTracking() for read-only queries to improve performance; always separate the read-only method with parameters from methods that need tracking.**/

  #region FOR REFERENCE ONLY
  #region READ-ONLY (AsNoTracking) METHODS
  public async Task<List<RandText>> GetListRandTextFromSqlRawAsync(string spName, params object?[] parameters)
  {
    string[] paramNames = ["@id", "@random_text", "@function_key"];
    var sql = MinimalDbSettings.FromSqlRawSQL(spName, paramNames);
    var param = MinimalDbSettings.FromSqlRawParamsObject(paramNames, parameters);

    return await _context.RandText
        .FromSqlRaw(sql, param)
        .AsNoTracking()
        .ToListAsync();
  } // working method for reference; not currently in use

  public async Task<RandText?> GetRandTextFromSqlRawAsync(string spName, params object?[] parameters)
  {
    string[] paramNames = ["@id", "@random_text", "@function_key"];
    var sql = MinimalDbSettings.FromSqlRawSQL(spName, paramNames);
    var param = MinimalDbSettings.FromSqlRawParamsObject(paramNames, parameters);

    var result = await _context.RandText
        .FromSqlRaw(sql, param)
        .AsNoTracking()
        .ToListAsync();

    return result.FirstOrDefault();
  } // working method for reference; not currently in use

  public async Task<SqlOutput?> GetSqlOutputFromSqlRawAsync(string spName, params object?[] parameters)
  {
    string[] paramNames = ["@id", "@random_text", "@function_key"];
    var sql = MinimalDbSettings.FromSqlRawSQL(spName, paramNames);
    var param = MinimalDbSettings.FromSqlRawParamsObject(paramNames, parameters);

    var result = await _context.SqlOutput
        .FromSqlRaw(sql, param)
        .AsNoTracking()
        .ToListAsync();

    return result.FirstOrDefault();
  } // working method for reference; not currently in use

  public async Task<List<RandText>> GetRandTextsFromSqlROAsync(string spName, params object?[] parameters)
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

    var sqlParam = MinimalDbSettings.FromSqlSQLParamStaticWOType(spName, sqlParameter);

    return await _context.RandText
        .FromSql(sqlParam)
        .AsNoTracking()
        .ToListAsync();
  }

  public async Task<string?> CreDelUpdRandTextOutputAsync(string spName, params object?[] parameters)
  {
    /* In the requestor page origin, always ensure that this `await context.DisposeAsync();` is defined.
    Otherwise, close the connection here (but since there is already been defined, separate a method for that instance).*/
    using var command = _context.Database.GetDbConnection().CreateCommand();
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

    await _context.Database.OpenConnectionAsync();
    using var reader = await command.ExecuteReaderAsync();
    // Map the result to your entity manually (optional)

    var message = outputParam.Value?.ToString();
    return message;
  } // reference

  public async Task<List<RandText>> GetRandTextsWOParamAsync(string spName)
  {
    // use AsNoTracking for read-only queries to improve performance
    return await _context.RandText
        .FromSql($"EXEC {spName}")
        .AsNoTracking()
        .ToListAsync();
  } // working method for reference; not currently in use

  public async Task<List<RandText>> GetRandTextsFromSqlReadOnlyAsync(string spName, params object?[] parameters)
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
        ("id", SqlDbType.Int, null),
        ("random_text", SqlDbType.VarChar, 50),
        ("function_key", SqlDbType.VarChar, 100),
        ("sp_output", SqlDbType.NVarChar, 100)
    };

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuples(paramDefs, parameters);

    if (parameters.Length != sqlParameter.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    return await _context.RandText
        .FromSql(sqlParam)
        .AsNoTracking()
        .ToListAsync();
  }

  public async Task<RandText?> GetRandTextsFromSqlAsync(string spName, params object?[] parameters)
  {
    var paramDefs = new (string Name, SqlDbType Type, int? Size)[]
    {
      ("id", SqlDbType.Int, null),
      ("random_text", SqlDbType.VarChar, 50),
      ("function_key", SqlDbType.VarChar, 100),
      ("sp_output", SqlDbType.NVarChar, 100)
    };

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuples(paramDefs, parameters);

    if (parameters.Length != sqlParameter.Length)
    {
      throw new ArgumentException("Parameters count mismatch.");
    }

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    var result = await _context.RandText
        .FromSql(sqlParam)
        .AsNoTracking()
        .ToListAsync();

    return result.FirstOrDefault();
  }

  #endregion READ-ONLY (AsNoTracking) METHODS

  #region NON-READ-ONLY METHODS

  public async Task<string?> CreDelUpdStringOutputAsync(string spName, params object?[] parameters)
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

    // var result = await MinimalDbSettings.UsingCommand(_context, spName, sqlParameter);
    // return result;
    return await MinimalDbSettings.UsingCommand(_context, spName, sqlParameter);
  } // reference

  public async Task<SqlOutput?> CreDelUpdRandTextsFromSqlAsync(string spName, params object?[] parameters)
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

    FormattableString sqlParam = MinimalDbSettings.FromSqlSQLParamDynamic(spName, sqlParameter);

    var result = await _context.SqlOutput
        .FromSql(sqlParam)
        .ToListAsync();

    return result.FirstOrDefault();
  }

  public async Task<List<RandText>> GetListRandTextsFromSqlAsync(string spName, params object?[] parameters)
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
        .ToListAsync();
  }

  #endregion NON-READ-ONLY METHODS

  #endregion FOR REFERENCE ONLY

  #region READ-ONLY (AsNoTracking) METHODS

  #region LIST METHODS

  public async Task<List<UserListDM>> UserListFromSqlReadOnlyAsyncList(string spName, Dictionary<string, object?> parameters)
  {
    var paramDefs = SQLServerInnerHelper.ManageUsersDataWithoutOutputParams();

    SqlParameter[]? sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDict(paramDefs, parameters);

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    return await _context.UserListDM
        .FromSql(sqlParam)
        .AsNoTracking()
        .ToListAsync();
  }

  #endregion LIST METHODS

  #region SINGLE-VALUE METHODS

  public async Task<UserListDM?> UserListFromSqlReadOnlyAsync(string spName, Dictionary<string, object?> parameters)
  {
    var paramDefs = SQLServerInnerHelper.ManageUsersDataWithoutOutputParams();

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDict(paramDefs, parameters);

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    var result = await _context.UserListDM
        .FromSql(sqlParam)
        .AsNoTracking()
        .ToListAsync();

    return result.FirstOrDefault();
  }

  #endregion SINGLE-VALUE METHODS

  #endregion READ-ONLY (AsNoTracking) METHODS

  #region NON-READ-ONLY METHODS

  #region LIST METHODS

  public async Task<List<UserListDM>> UserListFromSqlAsyncList(string spName, Dictionary<string, object?> parameters)
  {
    var paramDefs = SQLServerInnerHelper.ManageUsersDataWithoutOutputParams();

    SqlParameter[]? sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDict(paramDefs, parameters);

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    return await _context.UserListDM
        .FromSql(sqlParam)
        .ToListAsync();
  }

  #endregion LIST METHODS

  #region SINGLE-VALUE METHODS

  public async Task<UserListSingleDM?> UserListFromSqlAsync(string spName, Dictionary<string, object?> parameters)
  {
    var paramDefs = SQLServerInnerHelper.ManageUsersDataWithoutOutputParams();

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDict(paramDefs, parameters);

    var sqlParam = MinimalDbSettings.FromSqlSQLParamLessDynamic(spName, sqlParameter);

    var result = await _context.UserListSingleDM
        .FromSql(sqlParam)
        .ToListAsync();

    return result.FirstOrDefault();
  }

  public async Task<string?> VerifyUserStringOutputAsync(string spName, Dictionary<string, object?> parameters) // outside of testing, this should be used in user verification
  {
    var paramDefs = SQLServerInnerHelper.ManageUsersDataWithOutputParams();

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDict(paramDefs, parameters);

    return await MinimalDbSettings.UsingCommand(_context, spName, sqlParameter);
  }

  public async Task<string?> SimulateProdImgStringOutputAsync(string spName, Dictionary<string, object?> parameters)
  {
    var paramDefs = SQLServerInnerHelper.ManageAdminProductsWithOutputParams();

    SqlParameter[] sqlParameter = MinimalDbSettings.FromSqlSQLParamArrayOfTuplesAndDictPreScale(paramDefs, parameters);

    return await MinimalDbSettings.UsingCommand(_context, spName, sqlParameter);
  }

  #endregion SINGLE-VALUE METHODS

  #endregion NON-READ-ONLY METHODS
}
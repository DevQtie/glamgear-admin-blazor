using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Models.SQLServer;
using GlamGearAdmin.Data.SQLServer;

class SQLServerHelper(BlazorSQLServerContext context) : IAsyncDisposable
{
  private readonly BlazorSQLServerContext _context = context;

  public async Task<List<RandText>> GetRandTextsAsync(string storedProcedureWOParam)
  {
    return await _context.RandText
        .FromSqlRaw(storedProcedureWOParam)
        .AsNoTracking() // please read for reference: [Tracking vs. No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
        .ToListAsync();
  }

  public async ValueTask DisposeAsync()
  {
    if (_context is not null)
    {
      await _context.DisposeAsync();
      Console.WriteLine("Executed!");
    }
  }
}
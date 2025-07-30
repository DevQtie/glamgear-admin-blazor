namespace GlamGearAdmin.Components
{
  public class InternetCheckerService(HttpClient http)
  {
    private readonly HttpClient _http = http;

    public async Task<bool> IsOnlineAsync()
    {
      try
      {
        using var response = await _http.GetAsync("https://www.gstatic.com/generate_204");
        return response.IsSuccessStatusCode;
      }
      catch
      {
        return false;
      }
    }
  }
}
namespace GlamGearAdmin.Components;

public static class GlobalMethod
{
    public static string? DbDataToImg(byte[]? imgBytes)
    {
        if (imgBytes is null || imgBytes.Length == 0)
        {
            return null;
        }
        else
        {
            return $"data:image/png;base64,{Convert.ToBase64String(imgBytes)}";
        }
    }
}
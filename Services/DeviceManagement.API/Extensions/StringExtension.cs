namespace DeviceManagement.API.Extensions;

public static class StringExtension
{
    public static string ToCamelCase(this string stringValue)
    {
        return Char.ToLower(stringValue.First()) + stringValue.Substring(1);
    }
}

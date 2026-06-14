namespace Weblu.Application.Helpers;

public static class NameBuilder
{
    public static string BuildOrderName(string serviceName, string methodName)
    {
        return $"{serviceName} + {methodName}";
    }
}
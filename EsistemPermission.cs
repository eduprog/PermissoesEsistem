public record EsistemPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
    public static string NameForEsistem(string action, string resource) => $"PermissionsEsistem.{resource}.{action}";
}
using System.Reflection;

public static class Util
{
    public static IEnumerable<string> ListarConstantes(object obj)
    {
        return obj.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly)  // Filtra apenas as constantes
            .Select(f => $"{f.Name} = {f.GetRawConstantValue()}");
    }


public static Dictionary<string, List<string>> ListarClassesConstantes(object obj)
    {
        var constantesPorClasse = new Dictionary<string, List<string>>();
        var tipo = obj.GetType();
        
        var constantes = tipo.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly)  // Filtra apenas as constantes
            .Select(f => $"{f.Name} = {f.GetRawConstantValue()}")
            .ToList();

        constantesPorClasse.Add(tipo.Name, constantes);
        return constantesPorClasse;
    }
    public static List<string> ListarConstantes2(object obj)
    {
        var constantesFormatadas = new List<string>();
        var tipo = obj.GetType();
        
        // Obtém o valor da constante `Resource`
        var resourceConst = tipo.GetField("Resource", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        var resourceValue = resourceConst?.GetRawConstantValue()?.ToString() ?? "UnknownResource";

        // Obtém as demais constantes e as formata conforme solicitado
        var constantes = tipo.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.Name != "Resource")  // Filtra constantes excluindo `Resource`
            .Select(f => $"PermissionsEsistem.{resourceValue}.{f.GetRawConstantValue()}")
            .ToList();

        constantesFormatadas.AddRange(constantes);
        return constantesFormatadas;
    }
    public static List<string> ListarTodasPermissoes(Type interfaceType)
    {
        var permissoesFormatadas = new List<string>();
        var assembly = Assembly.GetExecutingAssembly();

        // Busca todas as classes que implementam a interface IPermissionEsistem no assembly
        var tiposImplementamInterface = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass);

        foreach (var tipo in tiposImplementamInterface)
        {
            // Tenta obter o valor da constante `Resource`
            var resourceConst = tipo.GetField("Resource", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var resourceValue = resourceConst?.GetRawConstantValue()?.ToString() ?? "UnknownResource";

            // Obtém e formata as outras constantes (excluindo `Resource`)
            var constantes = tipo.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.Name != "Resource")
                .Select(f => $"PermissionsEsistem.{resourceValue}.{f.GetRawConstantValue()}");

            permissoesFormatadas.AddRange(constantes);
        }

        return permissoesFormatadas;
    }


    public static List<string> ListarTodasPermissoes(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesFormatadas = new List<string>();
        var assembly = Assembly.GetExecutingAssembly();

        // Busca todas as classes que implementam a interface IPermissionEsistem no assembly
        var tiposImplementamInterface = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && (!excluirClassesAbstratas || !t.IsAbstract));

        foreach (var tipo in tiposImplementamInterface)
        {
            // Tenta obter o valor da constante `Resource`
            var resourceConst = tipo.GetField("Resource", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var resourceValue = resourceConst?.GetRawConstantValue()?.ToString() ?? "UnknownResource";

            // Obtém e formata as outras constantes (excluindo `Resource`)
            var constantes = tipo.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && f.Name != "Resource")
                .Select(f => $"EsistemPermission.{resourceValue}.{f.GetRawConstantValue()}");

            permissoesFormatadas.AddRange(constantes);
        }

        return permissoesFormatadas;
    }


    public static List<string> ListarTodasPermissoesFormatadas(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesFormatadas = new List<string>();
        var assembly = Assembly.GetExecutingAssembly();

        // Busca todas as classes que implementam a interface IPermissionEsistem no assembly
        var tiposImplementamInterface = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && (!excluirClassesAbstratas || !t.IsAbstract));

        foreach (var tipo in tiposImplementamInterface)
        {
            // Tenta obter o valor da constante `Resource`
            var resourceConst = tipo.GetField("Resource", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var resourceValue = resourceConst?.GetRawConstantValue()?.ToString() ?? "UnknownResource";

            // Obtém as permissões a partir da classe base
            var permissaoBase = (dynamic)Activator.CreateInstance(tipo);
            var permissoes = permissaoBase.Permissoes();

            // Formata as permissões
            foreach (var permissao in permissoes)
            {
                var nome = EsistemPermission.NameForEsistem(permissao.Action, resourceValue);
                permissoesFormatadas.Add($"{nome} - {permissao.Action} - {permissao.Description}");
            }
        }

        return permissoesFormatadas;
    }

public static PermissoesGerais ListarTodasPermissoesToObj(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesGerais = new PermissoesGerais();
        var assembly = Assembly.GetExecutingAssembly();

        // Busca todas as classes que implementam a interface IPermissionEsistem no assembly
        var tiposImplementamInterface = assembly.GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && (!excluirClassesAbstratas || !t.IsAbstract));

        foreach (var tipo in tiposImplementamInterface)
        {
            // Tenta obter o valor da constante `Resource`
            var resourceConst = tipo.GetField("Resource", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var resourceValue = resourceConst?.GetRawConstantValue()?.ToString() ?? "UnknownResource";

            // Obtém as permissões a partir da classe base
            var permissaoBase = (dynamic)Activator.CreateInstance(tipo);
            var permissoes = permissaoBase.Permissoes();

            // Adiciona as permissões ao objeto permissoesGerais
            foreach (var permissao in permissoes)
            {
                var nome = EsistemPermission.NameForEsistem(permissao.Action, resourceValue);
                permissoesGerais.Permissoes.Add(new PermissaoRegistro
                {
                    Nome = nome,
                    Action = permissao.Action,
                    Description = permissao.Description
                });
            }
        }

        return permissoesGerais;
    }


}

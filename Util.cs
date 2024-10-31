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

    public static PermissoesGeraisSimples ListarTodasPermissoesToObj(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesGerais = new PermissoesGeraisSimples();
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
                permissoesGerais.PermissoesEsistem.Add(new PermissaoRegistro
                {
                    Permissao = nome,
                    Acao = permissao.Action,
                    Descricao = permissao.Description
                });
            }
        }

        return permissoesGerais;
    }

    public static PermissoesGeraisSimples ListarTodasPermissoesPorResource(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesGerais = new PermissoesGeraisSimples();
        var permissoesAgrupadas = new Dictionary<string, List<PermissaoRegistro>>();

        // Obtendo todos os tipos que implementam a interface e não são abstratos
        var tipos = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => interfaceType.IsAssignableFrom(t) &&
                        (excluirClassesAbstratas ? !t.IsAbstract : true));

        foreach (var tipo in tipos)
        {
            var instancia = Activator.CreateInstance(tipo) as IPermissionEsistem;
            if (instancia != null)
            {
                var permissoes = instancia.Permissoes();

                // Agrupa as permissões
                foreach (var permissao in permissoes)
                {
                    var resource = permissao.Resource;
                    var action = permissao.Action;

                    // Cria a entrada se ainda não existir
                    if (!permissoesAgrupadas.ContainsKey(resource))
                    {
                        permissoesAgrupadas[resource] = new List<PermissaoRegistro>();
                    }

                    // Adiciona a permissão com a estrutura desejada
                    permissoesAgrupadas[resource].Add(new PermissaoRegistro
                    {
                        Permissao = EsistemPermission.NameFor(permissao.Action, permissao.Resource),
                        Acao = action,
                        Descricao = permissao.Description
                    });
                }
            }
        }

        // Adicionando a estrutura ao objeto PermissoesGerais
        foreach (var resource in permissoesAgrupadas.Keys)
        {
            // Configura as permissões agrupadas por resource
            var resourcePermissoes = permissoesAgrupadas[resource];
            permissoesGerais.GetType().GetProperty(resource)?.SetValue(permissoesGerais, resourcePermissoes);
        }

        return permissoesGerais;
    }
    public static PermissoesAgrupadasPorResource ListarTodasPermissoesAgrupadasPorResource(Type interfaceType, bool excluirClassesAbstratas)
    {
        var permissoesGerais = new PermissoesAgrupadasPorResource();

        // Obtendo todos os tipos que implementam a interface e não são abstratos
        var tipos = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => interfaceType.IsAssignableFrom(t) &&
                        (excluirClassesAbstratas ? !t.IsAbstract : true));

        foreach (var tipo in tipos)
        {
            var instancia = Activator.CreateInstance(tipo) as IPermissionEsistem;
            if (instancia != null)
            {
                var permissoes = instancia.Permissoes();

                // Agrupa as permissões por recurso
                foreach (var permissao in permissoes)
                {
                    var resourceName = permissao.Resource;

                    // Procura um recurso existente
                    var resource = permissoesGerais.PermissoesEsistem.FirstOrDefault(r => r.NomeRecurso == resourceName);
                    if (resource == null)
                    {
                        // Cria um novo recurso se não existir
                        resource = new Recurso { NomeRecurso = resourceName };
                        permissoesGerais.PermissoesEsistem.Add(resource);
                    }

                    // Adiciona a permissão ao recurso
                    resource.Permissoes.Add(new PermissaoRegistro
                    {
                        Permissao = EsistemPermission.NameFor(permissao.Action, permissao.Resource),
                        Acao = permissao.Action,
                        Descricao = permissao.Description
                    });
                }
            }
        }

        return permissoesGerais;
    }




    // Método para gravar o arquivo JSON
    public static void GravarArquivoJson(string directoryPath, string fileName, string content)
    {
        // Verifica se o diretório existe e, se não, cria
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Caminho completo do arquivo
        var filePath = Path.Combine(directoryPath, fileName);

        // Grava o conteúdo no arquivo com codificação UTF-8
        File.WriteAllText(filePath, content, System.Text.Encoding.UTF8);
        Console.WriteLine($"Permissões gerais foram salvas em {filePath}");
    }

}

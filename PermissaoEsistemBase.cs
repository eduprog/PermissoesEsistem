public abstract class PermissaoEsistemBase<TEntity> : IPermissionEsistem
{

    public const string Incluir = nameof(Incluir);
    public const string Visualizar = nameof(Visualizar);
    public const string Editar = nameof(Editar);
    public const string Excluir = nameof(Excluir);
    public const string Buscar = nameof(Buscar);

    private static string ResourceName() => typeof(TEntity).Name;

    private readonly List<EsistemPermission> _permissions =
    [
        new EsistemPermission($"{Incluir} {ResourceName()}", Incluir, ResourceName()),
        new EsistemPermission($"{Visualizar} {ResourceName()}", Visualizar, ResourceName()),
        new EsistemPermission($"{Editar} {ResourceName()}", Editar, ResourceName()),
        new EsistemPermission($"{Excluir} {ResourceName()}", Excluir, ResourceName()),
        new EsistemPermission($"{Buscar} {ResourceName()}", Buscar, ResourceName())
    ];

    public IReadOnlyCollection<EsistemPermission> Permissoes() => _permissions;

    /// <summary>
    /// adiciona a permissão diretamente no resource
    /// </summary>
    /// <param name="permissao"></param>
    public void AdicionaPermissao(string permissao)
    {
        _permissions.Add(new EsistemPermission($"{permissao} {ResourceName()}", permissao, ResourceName()));
    }

    /// <summary>
    /// Adiciona a permissão diretamente no resource de acordo com necessidade do sistema
    /// </summary>
    /// <param name="permissao"></param>
    public void AdicionaPermissao(EsistemPermission permissao)
    {
        _permissions.Add(permissao);
    }
}

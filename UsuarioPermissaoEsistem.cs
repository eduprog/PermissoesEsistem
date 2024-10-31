public class UsuarioPermissaoEsistem : PermissaoEsistemBase<Usuarios>
{
    public const string Resource = nameof(Usuarios);
    public const string AtivarDesativar = nameof(AtivarDesativar);
    public const string AlterarPermissao = nameof(AlterarPermissao);
    public UsuarioPermissaoEsistem()
    {
        AdicionaPermissao(new EsistemPermission("Ativa ou Desativa usuarios do sistema. Usado geralmente por administradores",AtivarDesativar,Resource,true));
        AdicionaPermissao(AlterarPermissao);
    }
}

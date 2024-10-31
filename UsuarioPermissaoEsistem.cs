public class UsuarioPermissaoEsistem : PermissaoEsistemBase<Usuarios>
{
    public const string Resource = nameof(Usuarios);
    public const string AtivarDesativar = nameof(AtivarDesativar);
    public const string AlterarPermissao = nameof(AlterarPermissao);
    public UsuarioPermissaoEsistem()
    {
        AdicionaPermissao(AtivarDesativar);
        AdicionaPermissao(AlterarPermissao);
    }
}

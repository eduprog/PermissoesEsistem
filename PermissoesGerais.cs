public class PermissoesGeraisInicio
{
    public PessoaPermissaoEsistem PessoaPermissaoEsistem { get; set; } = new PessoaPermissaoEsistem();
    public UsuarioPermissaoEsistem UsuarioPermissaoEsistem { get; set; } = new UsuarioPermissaoEsistem();
    public CargosPessoaPermissaoEsistem CargosPessoaPermissaoEsistem { get; set; } = new CargosPessoaPermissaoEsistem();

}



public class PermissoesGerais
{
    public List<PermissaoRegistro> Permissoes { get; set; } = new List<PermissaoRegistro>();
}

public class PermissaoRegistro
{
    public string Nome { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
}
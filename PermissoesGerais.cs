public class PermissoesGeraisInicio
{
    public PessoaPermissaoEsistem PessoaPermissaoEsistem { get; set; } = new PessoaPermissaoEsistem();
    public UsuarioPermissaoEsistem UsuarioPermissaoEsistem { get; set; } = new UsuarioPermissaoEsistem();
    public CargosPessoaPermissaoEsistem CargosPessoaPermissaoEsistem { get; set; } = new CargosPessoaPermissaoEsistem();

}



public class PermissoesGeraisSimples
{
    public List<PermissaoRegistro> PermissoesEsistem { get; set; } = new List<PermissaoRegistro>();
}


public class PermissaoRegistro
{
    public string Permissao { get; set; }
    public string Acao { get; set; }
    public string Descricao { get; set; }
}


public class PermissoesAgrupadasPorResource
{
    public List<Recurso> PermissoesEsistem { get; set; } = new();
}


public class Recurso
{
    public string NomeRecurso { get; set; }
    public List<PermissaoRegistro> Permissoes { get; set; } = new();
}



namespace QuickType
{

    public partial class PermissoesEsistem
    {
        public List<PermissoesGerais> PermissoesGerais { get; set; }
    }

    public partial class PermissoesGerais
    {
        public string NomeRecurso { get; set; }
        public List<Permissoe> Permissoes { get; set; }
    }

    public partial class Permissoe
    {
        public string Permissao { get; set; }
        public string Acao { get; set; }
        public string Descricao { get; set; }
    }
}

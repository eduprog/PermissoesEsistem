using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

public class Program
{

    public static void Main()
    {
        var permissoesPessoa = new PessoaPermissaoEsistem();
        var constantesPorClasse = Util.ListarClassesConstantes(permissoesPessoa).ToList();
        // foreach (var entry in constantesPorClasse)
        // {
        //     Console.WriteLine($"Classe: {entry.Key}");
        //     for (int i = 0; i < entry.Value.Count; i++)
        //     {
        //         Console.WriteLine($"  Constante: {entry.Value[i]}");
        //     }
        // }

        // var constantes = Util.ListarConstantes2(permissoesPessoa).ToList();
        // foreach (var item in constantes)
        // {
        //     Console.WriteLine(item);
        // }

        // Console.WriteLine("");

        var usuarioPermission = new UsuarioPermissaoEsistem();
        


        var permissoesGerais = Util.ListarTodasPermissoesToObj(typeof(IPermissionEsistem),true);

        foreach (var permissao in permissoesGerais.Permissoes)
        {
            Console.WriteLine($"{permissao.Nome} / {permissao.Action} / {permissao.Description}");
        }

        var jsonResult = JsonSerializer.Serialize(permissoesGerais, new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine(jsonResult);

    }





}

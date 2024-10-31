using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

public class Program
{

    public static void Main()
    {

        var permissoesGerais = Util.ListarTodasPermissoesAgrupadasPorResource(typeof(IPermissionEsistem),true);

        var jsonResult = JsonSerializer.Serialize(permissoesGerais, new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine(jsonResult);

       // Caminho do diretório onde o JSON será salvo
        var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
        var fileName = "permissoesgerais.json";
        
        Console.WriteLine($"Gerando o arquivo em {Path.Combine(directoryPath,fileName)}");

       // Grava o resultado no arquivo
        Util.GravarArquivoJson(directoryPath, fileName, jsonResult);

    }





}

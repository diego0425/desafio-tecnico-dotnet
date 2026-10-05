using System.Globalization;
using System.Text.Json;
using Desafio01.Comissoes.Models;
using Desafio01.Comissoes.Services;

var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "vendas.json");

if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Arquivo vendas.json não encontrado.");
    return;
}

var json = await File.ReadAllTextAsync(caminhoArquivo);
var opcoesJson = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var dados = JsonSerializer.Deserialize<DadosVendas>(json, opcoesJson);

if (dados is null)
{
    Console.WriteLine("Não foi possível ler os dados de vendas.");
    return;
}

var service = new ComissaoService();
var resultados = service.CalcularPorVendedor(dados.Vendas);
var cultura = new CultureInfo("pt-BR");

Console.WriteLine("=== COMISSÕES POR VENDEDOR ===\n");

foreach (var resultado in resultados)
{
    Console.WriteLine($"Vendedor: {resultado.Vendedor}");
    Console.WriteLine($"Total vendido: {resultado.TotalVendas.ToString("C", cultura)}");
    Console.WriteLine($"Comissão: {resultado.Comissao.ToString("C", cultura)}");
    Console.WriteLine();
}

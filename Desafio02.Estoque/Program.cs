using System.Text.Json;
using Desafio02.Estoque.Enums;
using Desafio02.Estoque.Models;
using Desafio02.Estoque.Services;

var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "estoque.json");

if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Arquivo estoque.json não encontrado.");
    return;
}

var json = await File.ReadAllTextAsync(caminhoArquivo);
var opcoesJson = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var dados = JsonSerializer.Deserialize<DadosEstoque>(json, opcoesJson);

if (dados is null)
{
    Console.WriteLine("Não foi possível ler os dados de estoque.");
    return;
}

var estoqueService = new EstoqueService(dados.Estoque);

while (true)
{
    Console.WriteLine("\n=== MOVIMENTAÇÃO DE ESTOQUE ===");
    Console.Write("Código do produto: ");

    if (!int.TryParse(Console.ReadLine(), out var codigoProduto))
    {
        Console.WriteLine("Código inválido.");
        continue;
    }

    var produto = estoqueService.BuscarProduto(codigoProduto);
    if (produto is null)
    {
        Console.WriteLine("Produto não encontrado.");
        continue;
    }

    Console.WriteLine($"Produto: {produto.DescricaoProduto}");
    Console.WriteLine($"Estoque atual: {produto.Estoque}");

    Console.Write("Tipo (1 - Entrada / 2 - Saída): ");
    if (!int.TryParse(Console.ReadLine(), out var tipoInformado) ||
        !Enum.IsDefined(typeof(TipoMovimentacao), tipoInformado))
    {
        Console.WriteLine("Tipo de movimentação inválido.");
        continue;
    }

    Console.Write("Quantidade: ");
    if (!int.TryParse(Console.ReadLine(), out var quantidade) || quantidade <= 0)
    {
        Console.WriteLine("Quantidade inválida.");
        continue;
    }

    Console.Write("Descrição da movimentação: ");
    var descricao = Console.ReadLine() ?? string.Empty;

    try
    {
        var movimentacao = estoqueService.Movimentar(
            codigoProduto,
            quantidade,
            (TipoMovimentacao)tipoInformado,
            descricao);

        Console.WriteLine("\nMovimentação realizada com sucesso.");
        Console.WriteLine($"Identificador: {movimentacao.Id}");
        Console.WriteLine($"Produto: {movimentacao.Produto}");
        Console.WriteLine($"Tipo: {movimentacao.Tipo}");
        Console.WriteLine($"Estoque final: {movimentacao.EstoqueFinal}");
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }

    Console.Write("\nDeseja realizar outra movimentação? (S/N): ");
    if (!string.Equals(Console.ReadLine(), "S", StringComparison.OrdinalIgnoreCase))
        break;
}

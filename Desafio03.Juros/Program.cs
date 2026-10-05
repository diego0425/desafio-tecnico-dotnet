using System.Globalization;
using Desafio03.Juros.Services;

var cultura = new CultureInfo("pt-BR");

Console.WriteLine("=== CÁLCULO DE JUROS ===\n");
Console.Write("Digite o valor: R$ ");

if (!decimal.TryParse(
        Console.ReadLine(),
        NumberStyles.Number,
        cultura,
        out var valor))
{
    Console.WriteLine("Valor inválido.");
    return;
}

Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");

if (!DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        cultura,
        DateTimeStyles.None,
        out var vencimento))
{
    Console.WriteLine("Data inválida.");
    return;
}

try
{
    var jurosService = new JurosService();
    var resultado = jurosService.Calcular(valor, vencimento, DateTime.Today);

    Console.WriteLine();
    Console.WriteLine($"Valor original: {resultado.ValorOriginal.ToString("C", cultura)}");
    Console.WriteLine($"Dias em atraso: {resultado.DiasAtraso}");
    Console.WriteLine($"Juros: {resultado.Juros.ToString("C", cultura)}");
    Console.WriteLine($"Valor total: {resultado.ValorTotal.ToString("C", cultura)}");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

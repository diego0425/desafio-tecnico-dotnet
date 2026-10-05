using Desafio03.Juros.Models;

namespace Desafio03.Juros.Services;

public class JurosService
{
    private const decimal TaxaDiaria = 0.025m;

    public ResultadoJuros Calcular(decimal valor, DateTime vencimento, DateTime dataCalculo)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

        var vencimentoNormalizado = vencimento.Date;
        var dataCalculoNormalizada = dataCalculo.Date;

        if (vencimentoNormalizado >= dataCalculoNormalizada)
        {
            return new ResultadoJuros
            {
                DiasAtraso = 0,
                ValorOriginal = valor,
                Juros = 0m,
                ValorTotal = valor
            };
        }

        var diasAtraso = (dataCalculoNormalizada - vencimentoNormalizado).Days;
        var juros = valor * TaxaDiaria * diasAtraso;

        return new ResultadoJuros
        {
            DiasAtraso = diasAtraso,
            ValorOriginal = valor,
            Juros = juros,
            ValorTotal = valor + juros
        };
    }
}

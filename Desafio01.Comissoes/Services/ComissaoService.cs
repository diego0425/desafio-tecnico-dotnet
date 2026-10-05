using Desafio01.Comissoes.Models;

namespace Desafio01.Comissoes.Services;

public class ComissaoService
{
    public IReadOnlyList<ResultadoComissao> CalcularPorVendedor(IEnumerable<Venda> vendas)
    {
        ArgumentNullException.ThrowIfNull(vendas);

        return vendas
            .GroupBy(venda => venda.Vendedor)
            .Select(grupo => new ResultadoComissao
            {
                Vendedor = grupo.Key,
                TotalVendas = grupo.Sum(venda => venda.Valor),
                Comissao = grupo.Sum(venda => CalcularComissao(venda.Valor))
            })
            .OrderBy(resultado => resultado.Vendedor)
            .ToList();
    }

    public decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 0)
            throw new ArgumentOutOfRangeException(nameof(valorVenda), "O valor da venda não pode ser negativo.");

        if (valorVenda < 100m)
            return 0m;

        if (valorVenda < 500m)
            return valorVenda * 0.01m;

        return valorVenda * 0.05m;
    }
}

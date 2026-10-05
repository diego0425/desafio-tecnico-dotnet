using Desafio02.Estoque.Enums;

namespace Desafio02.Estoque.Models;

public class Movimentacao
{
    public long Id { get; init; }
    public int CodigoProduto { get; init; }
    public string Produto { get; init; } = string.Empty;
    public TipoMovimentacao Tipo { get; init; }
    public int Quantidade { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public int EstoqueFinal { get; init; }
    public DateTime Data { get; init; }
}

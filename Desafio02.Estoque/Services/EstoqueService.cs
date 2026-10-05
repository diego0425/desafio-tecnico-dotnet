using Desafio02.Estoque.Enums;
using Desafio02.Estoque.Models;

namespace Desafio02.Estoque.Services;

public class EstoqueService
{
    private readonly List<Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = [];
    private long _proximoId = 1;

    public EstoqueService(IEnumerable<Produto> produtos)
    {
        ArgumentNullException.ThrowIfNull(produtos);
        _produtos = produtos.ToList();
    }

    public IReadOnlyCollection<Movimentacao> Movimentacoes => _movimentacoes.AsReadOnly();

    public Movimentacao Movimentar(
        int codigoProduto,
        int quantidade,
        TipoMovimentacao tipo,
        string descricao)
    {
        if (quantidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da movimentação é obrigatória.", nameof(descricao));

        var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto)
            ?? throw new KeyNotFoundException("Produto não encontrado.");

        if (tipo == TipoMovimentacao.Saida && produto.Estoque < quantidade)
            throw new InvalidOperationException("Estoque insuficiente para realizar a saída.");

        produto.Estoque = tipo switch
        {
            TipoMovimentacao.Entrada => produto.Estoque + quantidade,
            TipoMovimentacao.Saida => produto.Estoque - quantidade,
            _ => throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de movimentação inválido.")
        };

        var movimentacao = new Movimentacao
        {
            Id = _proximoId++,
            CodigoProduto = produto.CodigoProduto,
            Produto = produto.DescricaoProduto,
            Tipo = tipo,
            Quantidade = quantidade,
            Descricao = descricao.Trim(),
            EstoqueFinal = produto.Estoque,
            Data = DateTime.Now
        };

        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }

    public Produto? BuscarProduto(int codigoProduto) =>
        _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
}

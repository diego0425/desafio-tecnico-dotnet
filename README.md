# Desafio Técnico .NET - Target Sistemas

Solução organizada em três projetos Console, um para cada item do desafio.

## Estrutura

```text
DesafioTarget.sln
Desafio01.Comissoes/
Desafio02.Estoque/
Desafio03.Juros/
```

## Projetos

### Desafio 01 - Comissões
Lê `vendas.json`, aplica a regra de comissão por venda e exibe o total vendido e a comissão por vendedor.

### Desafio 02 - Estoque
Lê `estoque.json` e permite registrar entradas e saídas de produtos, gerando um identificador numérico único por movimentação e retornando o estoque final.

### Desafio 03 - Juros
Recebe um valor e uma data de vencimento e calcula os juros referentes aos dias de atraso.

> Interpretação adotada no desafio 3: 2,5% ao dia de forma simples sobre o valor original, pois o enunciado não especifica capitalização composta.

## Executar

Na raiz do repositório:

```bash
dotnet run --project Desafio01.Comissoes
dotnet run --project Desafio02.Estoque
dotnet run --project Desafio03.Juros
```

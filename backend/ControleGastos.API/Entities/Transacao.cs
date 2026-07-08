using ControleGastos.API.Enums;

namespace ControleGastos.API.Entities;

public class Transacao
{
    public int Id { get; set; }

    // Descricao livre informada pelo usuario, como "Salario" ou "Mercado".
    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    // Indica se o valor entra como receita ou sai como despesa.
    public TipoTransacao Tipo { get; set; }

    // Quando a tela nao envia uma data, usa a data e hora do cadastro.
    public DateTime Data { get; set; } = DateTime.Now;

    // Chave estrangeira que liga a transacao a uma pessoa.
    public int PessoaId { get; set; }

    public Pessoa? Pessoa { get; set; }
}

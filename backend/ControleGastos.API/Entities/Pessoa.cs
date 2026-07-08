namespace ControleGastos.API.Entities;

public class Pessoa
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int Idade { get; set; }

    // Guarda todas as receitas e despesas vinculadas a esta pessoa.
    public List<Transacao> Transacoes { get; set; } = [];
}

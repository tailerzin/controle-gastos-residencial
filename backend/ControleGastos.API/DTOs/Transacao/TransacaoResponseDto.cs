using ControleGastos.API.Enums;

namespace ControleGastos.API.DTOs.Transacao;

public class TransacaoResponseDto
{
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public TipoTransacao Tipo { get; set; }

    public DateTime Data { get; set; }

    public int PessoaId { get; set; }
}
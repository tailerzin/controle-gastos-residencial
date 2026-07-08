namespace ControleGastos.DTOs;

public class ConsultaTotaisDto
{
    public List<PessoaResumoDto> Pessoas { get; set; } = new();

    public decimal TotalReceitas { get; set; }

    public decimal TotalDespesas { get; set; }

    public decimal SaldoGeral { get; set; }
}
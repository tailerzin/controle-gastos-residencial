using ControleGastos.API.DTOs.Transacao;

namespace ControleGastos.API.Interfaces.Services;

public interface ITransacaoService
{
    Task<TransacaoResponseDto> AddAsync(CreateTransacaoDto dto);

    Task<List<TransacaoResponseDto>> GetAllAsync();
}
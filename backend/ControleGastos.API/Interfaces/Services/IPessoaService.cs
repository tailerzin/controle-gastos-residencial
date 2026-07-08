using ControleGastos.API.DTOs.Pessoa;
using ControleGastos.API.Entities;
using ControleGastos.DTOs;

namespace ControleGastos.API.Interfaces.Services;

public interface IPessoaService
{
    Task<PessoaResponseDto> AddAsync(Pessoa pessoa);

    Task<List<PessoaResponseDto>> GetAllAsync();

    Task<ConsultaTotaisDto> ObterTotaisAsync();

    Task<bool> DeleteAsync(int id);
}
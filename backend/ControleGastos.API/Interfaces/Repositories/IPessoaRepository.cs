using ControleGastos.API.Entities;
namespace ControleGastos.API.Interfaces.Repositories;

public interface IPessoaRepository
{
    Task<Pessoa> AddAsync(Pessoa pessoa);
    Task<List<Pessoa>> ObterComTransacoesAsync();
    Task<Pessoa?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Pessoa>> GetAllAsync();
}
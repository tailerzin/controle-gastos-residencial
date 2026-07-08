using ControleGastos.API.Entities;

namespace ControleGastos.API.Interfaces.Repositories;

public interface ITransacaoRepository
{
    Task<Transacao> AddAsync(Transacao transacao);

    Task<List<Transacao>> GetAllAsync();
}
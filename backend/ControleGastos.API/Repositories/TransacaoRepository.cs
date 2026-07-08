using ControleGastos.API.Data;
using ControleGastos.API.Entities;
using ControleGastos.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> AddAsync(Transacao transacao)
    {
        // Salva uma nova receita ou despesa no banco.
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }

    public async Task<List<Transacao>> GetAllAsync()
    {
        // Inclui a pessoa vinculada para permitir consultas mais completas.
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .ToListAsync();
    }
}

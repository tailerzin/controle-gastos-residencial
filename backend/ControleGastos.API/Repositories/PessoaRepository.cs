using ControleGastos.API.Data;
using ControleGastos.API.Entities;
using ControleGastos.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa> AddAsync(Pessoa pessoa)
    {
        // Adiciona a pessoa ao contexto e confirma a gravacao no banco.
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<List<Pessoa>> GetAllAsync()
    {
        // Busca todas as pessoas cadastradas.
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa?> GetByIdAsync(int id)
    {
        // Procura uma pessoa pela chave primaria.
        return await _context.Pessoas.FindAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Primeiro busca a pessoa para evitar erro ao tentar remover algo inexistente.
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
            return false;

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();

        return true;
    }

    // Carrega pessoas junto com suas transacoes para calcular os totais.
    public async Task<List<Pessoa>> ObterComTransacoesAsync()
    {
        return await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();
    }
}

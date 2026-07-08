using ControleGastos.API.Entities;
using ControleGastos.API.Interfaces.Repositories;
using ControleGastos.API.Interfaces.Services;
using ControleGastos.DTOs;
using ControleGastos.API.Enums;
using ControleGastos.API.DTOs.Pessoa;

namespace ControleGastos.API.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _repository;

    public PessoaService(IPessoaRepository repository)
    {
        _repository = repository;
    }

    public async Task<PessoaResponseDto> AddAsync(Pessoa pessoa)
    {
        var resultado = await _repository.AddAsync(pessoa);

        // Devolve um DTO para nao expor diretamente a entidade do banco.
        return new PessoaResponseDto
        {
            Id = resultado.Id,
            Nome = resultado.Nome,
            Idade = resultado.Idade
        };
    }

    public async Task<List<PessoaResponseDto>> GetAllAsync()
    {
        var pessoas = await _repository.GetAllAsync();

        // Lista todas as pessoas cadastradas em formato de resposta da API.
        return pessoas.Select(p => new PessoaResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        }).ToList();
    }

    // Calcula receitas, despesas e saldo por pessoa e tambem o resumo geral.
    public async Task<ConsultaTotaisDto> ObterTotaisAsync()
    {
        var pessoas = await _repository.ObterComTransacoesAsync();

        var pessoasResumo = pessoas.Select(p =>
        {
            var receitas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);

            var despesas = p.Transacoes
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);

            return new PessoaResumoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                TotalReceitas = receitas,
                TotalDespesas = despesas,
                Saldo = receitas - despesas
            };
        }).ToList();

        return new ConsultaTotaisDto
        {
            Pessoas = pessoasResumo,
            TotalReceitas = pessoasResumo.Sum(p => p.TotalReceitas),
            TotalDespesas = pessoasResumo.Sum(p => p.TotalDespesas),
            SaldoGeral = pessoasResumo.Sum(p => p.Saldo)
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Retorna false quando o cadastro nao existe, permitindo resposta 404.
        return await _repository.DeleteAsync(id);
    }
}

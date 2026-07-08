using ControleGastos.API.DTOs.Transacao;
using ControleGastos.API.Entities;
using ControleGastos.API.Enums;
using ControleGastos.API.Interfaces.Repositories;
using ControleGastos.API.Interfaces.Services;

namespace ControleGastos.API.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<List<TransacaoResponseDto>> GetAllAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();

        // Converte as entidades do banco para DTOs usados na resposta da API.
        return transacoes.Select(t => new TransacaoResponseDto
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = t.Tipo,
            Data = t.Data,
            PessoaId = t.PessoaId
        }).ToList();
    }

    public async Task<TransacaoResponseDto> AddAsync(CreateTransacaoDto dto)
    {
        // Garante que a transacao sempre pertence a uma pessoa cadastrada.
        var pessoa = await _pessoaRepository.GetByIdAsync(dto.PessoaId);

        if (pessoa == null)
        {
            throw new Exception("Pessoa nao encontrada.");
        }

        // Regra de negocio: menores de 18 anos podem cadastrar apenas despesas.
        if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
        {
            throw new Exception("Menores de idade podem cadastrar apenas despesas.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            PessoaId = dto.PessoaId
        };

        var resultado = await _transacaoRepository.AddAsync(transacao);

        // Retorna somente os dados que o frontend precisa exibir.
        return new TransacaoResponseDto
        {
            Id = resultado.Id,
            Descricao = resultado.Descricao,
            Valor = resultado.Valor,
            Tipo = resultado.Tipo,
            Data = resultado.Data,
            PessoaId = resultado.PessoaId
        };
    }
}

using ControleGastos.API.DTOs.Pessoa;
using ControleGastos.API.Entities;
using ControleGastos.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.DTOs;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _service;

    public PessoasController(IPessoaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> Add(CreatePessoaDto dto)
    {
        // Recebe os dados do formulario e monta a entidade para salvar.
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        var novaPessoa = await _service.AddAsync(pessoa);

        return CreatedAtAction(nameof(GetAll), new { id = novaPessoa.Id }, novaPessoa);
    }

    [HttpGet]
    public async Task<ActionResult<List<Pessoa>>> GetAll()
    {
        // Retorna todos os cadastros de pessoas.
        var pessoas = await _service.GetAllAsync();

        return Ok(pessoas);
    }

    [HttpGet("totais")]
    public async Task<ActionResult<ConsultaTotaisDto>> ObterTotais()
    {
        // Entrega o resumo financeiro usado na tela principal.
        var totais = await _service.ObterTotaisAsync();

        return Ok(totais);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        // Remove a pessoa e suas transacoes vinculadas pelo cascade delete.
        var removido = await _service.DeleteAsync(id);

        if (!removido)
            return NotFound("Pessoa nao encontrada.");

        return NoContent();
    }
}

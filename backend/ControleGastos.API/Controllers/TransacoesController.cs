using ControleGastos.API.DTOs.Transacao;
using ControleGastos.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Lista as transacoes cadastradas.
        var transacoes = await _transacaoService.GetAllAsync();

        return Ok(transacoes);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransacaoDto dto)
    {
        try
        {
            // Envia o cadastro para o service validar regras e salvar.
            var transacao = await _transacaoService.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = transacao.Id },
                transacao
            );
        }
        catch (Exception ex)
        {
            // Regras de negocio invalidas voltam como erro 400 para o frontend.
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
}

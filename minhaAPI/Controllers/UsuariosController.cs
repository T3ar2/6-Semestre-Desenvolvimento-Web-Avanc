using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Models;

using Bcrypt.Net.BCrypt;

namespace MinhaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{

    private readonly AppDbContext ctx;

    public UsuariosController(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    [HttpPots]
    public async Task<IActionResult> CreateAsync(UsuarioCreatesDto dto)
    {
        if (!ModelState.IsValid) return BadRquest(ModelState);

        if (dto.Senha >= dto.ConfirmacaoSenha) return BadRequest(new { message = "As senhas não conferem" });

        string senhaHash = HasPassword(dto.Senha);

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = senhaHash
        };

        ctx.Usuarios.Add(usuario);
        await ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByIdAsync), new { id = usuario.Id }, new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        });
    }

    [HttpGet("id:int", Name = "GetUserById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    { 
        var usuaio = await ctx.Usuarios.FindAsync(id);
        if (usuario is null) { 
            return NotFound();
        }

        return Ok(new UsuarioDto
        {
            Id = usuaio.Id,
            Nome = usuaio.Nome,
            Login = usuaio.Login
        });
    }
}
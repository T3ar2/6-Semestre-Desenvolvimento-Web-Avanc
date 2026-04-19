using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.DTOs;
using MinhaApi.Models;

using static BCrypt.Net.BCrypt;

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

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UsuarioCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (dto.Senha != dto.ConfirmarSenha) return BadRequest(new { message = "As senhas não conferem" });

        string senhaHash = HashPassword(dto.Senha);

        var usuario = new Usuario{
            Nome = dto.Nome,
            Login = dto.Login,
            Senha = senhaHash  
        };

        ctx.Usuarios.Add(usuario);
        await ctx.SaveChangesAsync();

        return CreatedAtRoute("GetUserById", new {id = usuario.Id}, new UsuarioDto{
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login,
        });
    }

    [HttpGet("id:int", Name = "GetUserById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    { 
        var usuario = await ctx.Usuarios.FindAsync(id);
        if (usuario is null) { 
            return NotFound();
        }

        return Ok(new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        });
    }
}
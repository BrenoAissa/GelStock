using GelStock.Api.DTOs.Usuario;
using GelStock.Api.Models;
using GelStock.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GelStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDto>>> GetTodos()
        {
            try
            {
                var usuarios = await _usuarioService.ListarTodosUsuariosAsync();
                var usuariosDto = usuarios.Select(u => new UsuarioResponseDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Sobrenome = u.Sobrenome,
                    Parentesco = u.Parentesco
                }).ToList();

                return Ok(usuariosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar usuários", erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterUsuarioPorIdAsync(id);
                var usuarioDto = new UsuarioResponseDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Parentesco = usuario.Parentesco
                };

                return Ok(usuarioDto);
            }
            catch (GelStock.Api.Exceptions.UsuarioNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar usuário", erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> Post([FromBody] UsuarioCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Sobrenome = dto.Sobrenome,
                    Parentesco = dto.Parentesco
                };

                var usuarioCriado = await _usuarioService.CriarUsuarioAsync(usuario);
                var usuarioDto = new UsuarioResponseDto
                {
                    Id = usuarioCriado.Id,
                    Nome = usuarioCriado.Nome,
                    Sobrenome = usuarioCriado.Sobrenome,
                    Parentesco = usuarioCriado.Parentesco
                };

                return CreatedAtAction(nameof(GetPorId), new { id = usuarioDto.Id }, usuarioDto);
            }
            catch (GelStock.Api.Exceptions.UsuarioJaExisteException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar usuário", erro = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> Put(int id, [FromBody] UsuarioUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != dto.Id)
                {
                    return BadRequest(new { mensagem = "ID da URL não corresponde ao ID do DTO" });
                }

                var usuario = new Usuario
                {
                    Id = dto.Id,
                    Nome = dto.Nome,
                    Sobrenome = dto.Sobrenome,
                    Parentesco = dto.Parentesco
                };

                var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(usuario);
                var usuarioDto = new UsuarioResponseDto
                {
                    Id = usuarioAtualizado.Id,
                    Nome = usuarioAtualizado.Nome,
                    Sobrenome = usuarioAtualizado.Sobrenome,
                    Parentesco = usuarioAtualizado.Parentesco
                };

                return Ok(usuarioDto);
            }
            catch (GelStock.Api.Exceptions.UsuarioNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar usuário", erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var usuarioExcluido = await _usuarioService.ExcluirUsuarioAsync(id);
                return Ok(new { mensagem = "Usuário deletado com sucesso", usuarioId = usuarioExcluido.Id });
            }
            catch (GelStock.Api.Exceptions.UsuarioNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao excluir usuário", erro = ex.Message });
            }
        }
    }
}

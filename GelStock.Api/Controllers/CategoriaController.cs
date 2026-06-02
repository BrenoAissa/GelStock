using GelStock.Api.DTOs.Categoria;
using GelStock.Api.Models;
using GelStock.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GelStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;
        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaResponseDto>>> GetTodos()
        {
            try
            {
                var categorias = await _categoriaService.ListarTodasCategoriasAsync();
                var categoriasDto = categorias.Select(c => new CategoriaResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).ToList();

                return Ok(categoriasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar categorias", erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponseDto>> GetPorId(int id)
        {
            try
            {
                var categoria = await _categoriaService.BuscarCategoriaPorIdAsync(id);
                var categoriaDto = new CategoriaResponseDto
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
                };
                return Ok(categoriaDto);
            }
            catch (GelStock.Api.Exceptions.CategoriaNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar categoria", erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponseDto>> Post([FromBody] CategoriaCreateDto categoriaCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoria = new Categoria
                {
                    Nome = categoriaCreateDto.Nome
                };

                var categoriaCriada = await _categoriaService.CriarCategoriaAsync(categoria);
                var categoriaDto = new CategoriaResponseDto
                {
                    Id = categoriaCriada.Id,
                    Nome = categoriaCriada.Nome
                };

                return CreatedAtAction(nameof(GetPorId), new { id = categoriaDto.Id }, categoriaDto);
            }
            catch (GelStock.Api.Exceptions.CategoriaJaExisteException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar categoria", erro = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaResponseDto>> Put(int id, [FromBody] CategoriaUpdateDto dto)
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

                var categoriaAtualizada = new Categoria
                {
                    Id = dto.Id,
                    Nome = dto.Nome
                };

                var categoriaAtualizado = await _categoriaService.AtualizarCategoriaAsync(categoriaAtualizada);
                var categoriaDto = new CategoriaResponseDto
                {
                    Id = categoriaAtualizado.Id,
                    Nome = categoriaAtualizado.Nome
                };

                return Ok(categoriaDto);
            }
            catch (GelStock.Api.Exceptions.CategoriaNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (GelStock.Api.Exceptions.CategoriaJaExisteException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar categoria", erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var categoriaExcluida = await _categoriaService.ExcluirCategoriaAsync(id);
                return Ok(new { mensagem = "Categoria deletado com sucesso", categoriaId = categoriaExcluida.Id });
            }
            catch (GelStock.Api.Exceptions.CategoriaNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao excluir categoria", erro = ex.Message });
            }
        }
    }
}
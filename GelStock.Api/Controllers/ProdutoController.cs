using GelStock.Api.DTOs.Produto;
using GelStock.Api.Models;
using GelStock.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GelStock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProdutoResponseDto>>> GetTodos()
        {
            try
            {
                var produtos = await _produtoService.ListarTodosItensAsync();
                var produtosDto = produtos.Select(p => new ProdutoResponseDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Tipo = p.Tipo,
                    Fabricante = p.Fabricante,
                    Quantidade = p.Quantidade,
                    CategoriaId = p.categoriaId,
                    UsuarioId = p.usuarioId
                }).ToList();

                return Ok(produtosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar produtos", erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> GetPorId(int id)
        {
            try
            {
                var produto = await _produtoService.BuscarItemPorIdAsync(id);
                var produtoDto = new ProdutoResponseDto
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Tipo = produto.Tipo,
                    Fabricante = produto.Fabricante,
                    Quantidade = produto.Quantidade,
                    CategoriaId = produto.categoriaId,
                    UsuarioId = produto.usuarioId
                };

                return Ok(produtoDto);
            }
            catch (GelStock.Api.Exceptions.ProdutoNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar produto", erro = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> Post([FromBody] ProdutoCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var produto = new Produto
                {
                    Nome = dto.Nome,
                    Tipo = dto.Tipo,
                    Fabricante = dto.Fabricante,
                    Quantidade = dto.Quantidade,
                    categoriaId = dto.CategoriaId,
                    usuarioId = dto.UsuarioId
                };

                var produtoCriado = await _produtoService.CriarItemAsync(produto);
                var produtoDto = new ProdutoResponseDto
                {
                    Id = produtoCriado.Id,
                    Nome = produtoCriado.Nome,
                    Tipo = produtoCriado.Tipo,
                    Fabricante = produtoCriado.Fabricante,
                    Quantidade = produtoCriado.Quantidade,
                    CategoriaId = produtoCriado.categoriaId,
                    UsuarioId = produtoCriado.usuarioId
                };

                return CreatedAtAction(nameof(GetPorId), new { id = produtoDto.Id }, produtoDto);
            }
            catch (GelStock.Api.Exceptions.ProdutoJaExisteException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar produto", erro = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> Put(int id, [FromBody] ProdutoUpdateDto dto)
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

                var produto = new Produto
                {
                    Id = dto.Id,
                    Nome = dto.Nome,
                    Tipo = dto.Tipo,
                    Fabricante = dto.Fabricante,
                    Quantidade = dto.Quantidade,
                    categoriaId = dto.CategoriaId,
                    usuarioId = dto.UsuarioId
                };

                var produtoAtualizado = await _produtoService.AtualizarItemAsync(produto);
                var produtoDto = new ProdutoResponseDto
                {
                    Id = produtoAtualizado.Id,
                    Nome = produtoAtualizado.Nome,
                    Tipo = produtoAtualizado.Tipo,
                    Fabricante = produtoAtualizado.Fabricante,
                    Quantidade = produtoAtualizado.Quantidade,
                    CategoriaId = produtoAtualizado.categoriaId,
                    UsuarioId = produtoAtualizado.usuarioId
                };

                return Ok(produtoDto);
            }
            catch (GelStock.Api.Exceptions.ProdutoNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar produto", erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var produtoExcluido = await _produtoService.ExcluirItemAsync(id);
                return Ok(new { mensagem = "Produto deletado com sucesso", produtoId = produtoExcluido.Id });
            }
            catch (GelStock.Api.Exceptions.ProdutoNaoExisteException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao deletar produto", erro = ex.Message });
            }
        }
    }
}
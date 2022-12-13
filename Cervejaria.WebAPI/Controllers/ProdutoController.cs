using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cervejaria.Domain;
using Cervejaria.Domain.repositories;
using Cervejaria.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cervejaria.WebAPI.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController()
        {
            _produtoRepository = new ProdutoRepository();
        }

        [HttpPost]
        public IActionResult PostProduto(Produto novoProduto)
        {
            try
            {
                _produtoRepository.CadastrarProduto(novoProduto);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            try
            {
                return Ok(_produtoRepository.BuscarTodosProdutos());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("ativos")]
        public IActionResult GetProdutosAtivos()
        {
            try
            {
                return Ok(_produtoRepository.BuscarTodosProdutosAtivos());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("id")]
        public IActionResult GetProdutoPorId(int idProduto)
        {
            try
            {
                return Ok(_produtoRepository.BuscarProdutoPorId(idProduto));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public IActionResult PutProduto(Produto produtoEditado)
        {
            try
            {
                _produtoRepository.EditarProduto(produtoEditado);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPatch]
        public IActionResult PatchGerenciarEstoque( Produto produtoEditado)
        {
            try
            {
                _produtoRepository.GerenciarEstoque(produtoEditado);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPatch("alterar-disponibilidade")]
        public IActionResult PatchAtivarDesativarProduto(Produto produtoEditado)
        {
            try
            {
                _produtoRepository.AtivarEDesativarProduto(produtoEditado);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

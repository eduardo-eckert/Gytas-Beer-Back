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
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController()
        {
            _pedidoRepository = new PedidoRepository();
        }

        [HttpPost]
        public IActionResult PostPedido(Pedido novoPedido)
        {
            try
            {
                _pedidoRepository.RealizarPedido(novoPedido);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public IActionResult GetPedidos()
        {
            try
            {
                return Ok(_pedidoRepository.BuscarTodosPedidos());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPatch]
        public IActionResult PatchAtualizarStatus(int idPedido, Status status)
        {
            try
            {
                _pedidoRepository.AtualizarStatus(idPedido, status);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("id")]
        public IActionResult GetPedidoPorId(int idPedido)
        {
            try
            {
                return Ok(_pedidoRepository.BuscarPedidoPorId(idPedido));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpDelete]
        public IActionResult ExcluirPedido([FromQuery] int idProduto)
        {
            try
            {
                _pedidoRepository.ExcluirPedido(idProduto);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

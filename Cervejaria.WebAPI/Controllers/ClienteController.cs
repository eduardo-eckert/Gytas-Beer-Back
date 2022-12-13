using Cervejaria.Domain;
using Cervejaria.Domain.Exceptions;
using Cervejaria.Domain.repositories;
using Cervejaria.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cervejaria.WebAPI.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController()
        {
            _clienteRepository = new ClienteRepository();
        }

        [HttpPost]
        public IActionResult PostCliente(Cliente novoCliente)
        {
            try
            {
                _clienteRepository.CadastrarCliente(novoCliente);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                return Ok(_clienteRepository.BuscarTodosClientes());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("id")]
        public IActionResult GetClientePorId(int idCliente)
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientePorId(idCliente));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        public IActionResult PutCliente(Cliente clienteEditado)
        {
            try
            {
                _clienteRepository.EditarCliente(clienteEditado);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete]
        public IActionResult ExcluirCliente(string cpf)
        {
            try
            {
                _clienteRepository.ExcluirCliente(cpf);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

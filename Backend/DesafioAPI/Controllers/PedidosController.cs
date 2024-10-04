using Domain.PedidoRoot.Entity;
using InterfaceApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _pedidoService.GetPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            return pedido is null ? NotFound() : Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            try
            {
                var createdPedido = await _pedidoService.CreatePedidoAsync(pedido);
                return CreatedAtAction(nameof(Get), new { id = createdPedido.Id }, createdPedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id) return BadRequest();
            await _pedidoService.UpdatePedidoAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pedidoService.DeletePedidoAsync(id);
            return NoContent();
        }
    }

}

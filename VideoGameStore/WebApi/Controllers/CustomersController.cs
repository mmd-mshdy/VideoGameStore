using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Customers.Command.Create;
using VideoGameStore.Application.Customers.Command.Delete;
using VideoGameStore.Application.Customers.Command.Update;
using VideoGameStore.Application.Customers.Query;
using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ISender _sender;
        public CustomersController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var id = await _sender.Send(dto);
            return Ok(new { CustomerId = id });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = new GetCustomerByIdQuery(id);
            var customer = await  _sender.Send(dto);
            return dto ==null ? NotFound() : Ok(customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand dto)
        {
            var customer = await _sender.Send(dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteCustomerCommand(id));
            return NoContent();
        }


    }
}

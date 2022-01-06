using Microsoft.AspNetCore.Mvc;
using PracticePackAPI.Models;
using PracticePackAPI.Services;

namespace PracticePackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly iService<Address> _addressService;

        public AddressesController(iService<Address> AddressService)
        {
            _addressService = AddressService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Address>> GetAddresses()
        {
            return _addressService.GetAllWithData().ToArray();
        }

        [HttpGet("filter/{identity}")]
        public ActionResult<IEnumerable<Address>> GetFilterAddresses(Guid identity)
        {
            return _addressService.GetAllWithData(u => u.UserId == identity).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            return await _addressService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            return await _addressService.Create(address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> UpdateAddress(Guid id, Address address)
        {
            if (id != address.Id)
                return BadRequest();

            var updatedEntity = await _addressService.Update(address);

            if (updatedEntity == null)
                return NotFound();

            return updatedEntity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            if (await _addressService.GetById(id) == null)
                return NotFound();

            await _addressService.Delete(id);

            return NoContent();
        }
    }
}
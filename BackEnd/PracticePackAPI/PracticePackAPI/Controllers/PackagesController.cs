using Microsoft.AspNetCore.Mvc;
using PracticePackAPI.Models;
using PracticePackAPI.Services;

namespace PracticePackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly iService<Package> _packageService;

        public PackagesController(iService<Package> PackageService)
        {
            _packageService = PackageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Package>> GetPackages()
        {
            return _packageService.GetAllWithData().ToArray();
        }

        [HttpGet("filter/{identity}")]
        public ActionResult<IEnumerable<Package>> GetFilterPackages(Guid identity)
        {
            return _packageService.GetAllWithData(p => p.UserId == identity).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(Guid id)
        {
            return await _packageService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage(Package package)
        {
            return await _packageService.Create(package);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Package>> UpdatePackage(Guid id, Package package)
        {
            if(id != package.Id)
                return BadRequest();

            var updatedEntity = await _packageService.Update(package);

            if(updatedEntity == null)
                return NotFound();

            return updatedEntity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)
        {
            if(await _packageService.GetById(id) == null)
                return NotFound();

            await _packageService.Delete(id);

            return NoContent();
        }
    }
}
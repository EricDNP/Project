using Microsoft.AspNetCore.Mvc;
using PracticePackAPI.Models;
using PracticePackAPI.Services;

namespace PracticePackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly iService<Branch> _branchService;

        public BranchesController(iService<Branch> BranchService)
        {
            _branchService = BranchService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Branch>> GetBranches()
        {
            return _branchService.GetAllWithData().ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(Guid id)
        {
            return await _branchService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Branch>> CreateBranch(Branch branch)
        {
            return await _branchService.Create(branch);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Branch>> UpdateBranch(Guid id, Branch branch)
        {
            if(id != branch.Id)
                return BadRequest();

            var updatedEntity = await _branchService.Update(branch);

            if(updatedEntity == null)
                return NotFound();

            return updatedEntity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            if(await _branchService.GetById(id) == null)
                return NotFound();

            await _branchService.Delete(id);

            return NoContent();
        }
    }
}
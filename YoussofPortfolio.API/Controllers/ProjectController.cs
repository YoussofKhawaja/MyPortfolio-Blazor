using Microsoft.AspNetCore.Mvc;
using YoussofPortfolio.API.Interfaces;

namespace YoussofPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject project;
        public ProjectController(IProject project)
        {
            this.project = project;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await project.GetProjectsAsync();
            return Ok(projects);
        }
    }
}

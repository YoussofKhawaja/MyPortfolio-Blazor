using Microsoft.EntityFrameworkCore;
using YoussofPortfolio.API.Interfaces;
using YoussofPortfolio.API.Database;
using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Services
{
    public class ProjectService : IProject
    {
        private readonly DataContext dataContext;
        public ProjectService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<List<Project>> GetProjectsAsync()
        {
            return await dataContext.Project.ToListAsync();
        }
    }
}

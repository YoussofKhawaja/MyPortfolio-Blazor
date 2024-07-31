using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Interfaces
{
    public interface IProject
    {
        Task<List<Project>> GetProjectsAsync();
    }
}

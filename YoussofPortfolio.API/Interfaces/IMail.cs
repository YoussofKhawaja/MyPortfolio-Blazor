using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Interfaces
{
    public interface IMail
    {
        Task<bool> SendEmailAsync(Contact contact);
        Task<bool> SendEmailMeAsync(Contact contact);
    }
}

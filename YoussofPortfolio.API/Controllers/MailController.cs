using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using YoussofPortfolio.API.Interfaces;
using YoussofPortfolio.API.Models;

namespace YoussofPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMail mail;
        public MailController(IMail mail)
        {
            this.mail = mail;
        }

        [HttpPost]
        [Route("SendEmail")]
        [EnableRateLimiting("clientLimit")]
        public async Task<IActionResult> SendEmail([FromBody] Contact contact)
        {
            if (await mail.SendEmailAsync(contact) && await mail.SendEmailMeAsync(contact))
            {
                return Ok("Email sent");
            }
       
            return BadRequest("Email not sent");
        }
    }
}

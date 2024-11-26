using EmailApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(IConfiguration configuration)  
        {
            _emailService = new EmailService(configuration);  
        }

        [HttpPost("send-with-attachment")]
        public IActionResult SendEmailWithAttachment([FromBody] EmailRequest emailRequest)
        {
            try
            {
                string pdfPath = @"C:\Users\Ragu\HeshyaVK.pdf";

                _emailService.SendEmailWithAttachment(emailRequest.ToEmail, emailRequest.Subject, emailRequest.Body, pdfPath);
                return Ok("Email with attachment sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }
    }
}

public class EmailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}

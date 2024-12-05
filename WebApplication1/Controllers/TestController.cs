using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using WebApplication1.Views.Mails;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(SmtpClient client, Microsoft.AspNetCore.Components.Web.HtmlRenderer htmlRenderer) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Send(string body)
        {
            var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var dictionary = new Dictionary<string, object?>
                {
                    { "Nom", "Ly" },
                    { "Prenom", "Khun" },
                };

                var parameters = Microsoft.AspNetCore.Components.ParameterView.FromDictionary(dictionary);
                var output = await htmlRenderer.RenderComponentAsync<Inscription>(parameters);

                return output.ToHtmlString();
            });

            var mail = new MailMessage
            {
                Subject = "test",
                Body = html,
                IsBodyHtml = true,
                From = new MailAddress("test@khunly.be", "Khun"),
                
            };
            mail.To.Add("lykhun@gmail.com");
            mail.To.Add("avetissian52@gmail.com");
            try
            {

                await client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {

            }
            return Ok(body);
        }
    }
}

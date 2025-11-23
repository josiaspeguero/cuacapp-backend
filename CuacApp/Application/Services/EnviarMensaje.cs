
using System.Net;
using System.Net.Mail;

namespace CuacApp.Application.Services
{
    public class EnviarMensaje : IEnviarMensaje
    {
        private readonly IConfiguration _configuration;

        public EnviarMensaje(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        async Task<(bool status, string message)> IEnviarMensaje.GetEnviarMensaje(string receptor, string titulo, string contenido)
        {
            try
            {
                var emisor = _configuration.GetValue<string>("SMTP_CLIENT:USER");
                var pass = _configuration.GetValue<string>("SMTP_CLIENT:PASSWORD");
                var host = _configuration.GetValue<string>("SMTP_CLIENT:HOST");
                var port = _configuration.GetValue<int>("SMTP_CLIENT:PORT");

                using (var smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;

                    smtpClient.Credentials = new NetworkCredential(emisor, pass);

                    using (var cuerpo = new MailMessage(emisor!, receptor, titulo, contenido))
                    {
                        await smtpClient.SendMailAsync(cuerpo);
                    }
                }
                return (true, "Correo enviado correctamentr");
            }
            catch (SmtpException smtpEx)
            {
                return (false, $"Ha ocurrido un error: {smtpEx}");
            }
            catch (Exception ex)
            {
                return (false, $"Ha ocurrido un error: {ex}");
            }
        }
    }
}

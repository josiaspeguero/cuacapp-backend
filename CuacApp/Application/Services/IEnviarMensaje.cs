namespace CuacApp.Application.Services
{
    public interface IEnviarMensaje
    {
        Task<(bool status, string message)> GetEnviarMensaje(string receptor, string titulo, string contenido);
    }
}

using PlataformaEventosTech_API.Models;

namespace PlataformaEventosTech_API.Repositories.Interfaces
{
    public interface IPlataformaRepository
    {
        Task<List<Evento>> GetAll();

        Task<Evento> GetById(int id);

        Task Add(Evento evento);

        Task Update(Evento evento);

        Task Delete(int id);
    }
}

using PlataformaEventosTech_API.Models;
using PlataformaEventosTech_API.Repositories.Interfaces;

namespace PlataformaEventosTech_API.Services
{
    /// <summary>
    /// PlataformaService - classe de serviço responsável
    /// por toda a lógica de negócios relacionada a eventos
    /// </summary>
    public class PlataformaService
    {
        /// <summary>
        /// Repository de eventos - responsável por acessar os 
        /// dados dos eventos cadastrados no banco de dados
        /// </summary> 

        private readonly IPlataformaRepository _plataformaRepo;

        /// <summary>
        /// Construtor da classe - recebe o repository de eventos
        /// via injeção de dependência
        /// </summary>
        /// <param name="plataformaRepo"></param>

        public PlataformaService(IPlataformaRepository plataformaRepo)
        {
            _plataformaRepo = plataformaRepo;
        }

        /// <summary>
        /// Listar todos os eventos - chama o metódo GetAll do 
        /// repository para obter a lista de eventos do banco de dados
        /// </summary>
        /// <returns></returns>

        public async Task<List<Evento>> Listar() => await _plataformaRepo.GetAll();

        /// <summary>
        /// Obter um evento por id - chama o método GetById
        /// do repository para obter um evento com base no id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<Evento> ObterPorId(int id) => await _plataformaRepo.GetById(id);

        /// <summary>
        /// Criar um novo evento - Chama o método Add
        /// do repository para adicionar um novo evento ao banco de dados
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>

        public async Task Criar(Evento evento) => await _plataformaRepo.Add(evento);

        /// <summary>
        /// Atualizar um evento existente - chama
        /// o método Update do repository
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public async Task Atualizar(Evento evento) => await _plataformaRepo.Update(evento);

        /// <summary>
        /// Deletar um evento por id -  Chama
        /// o método Delete do repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Deletar(int id) => await _plataformaRepo.Delete(id);

    }
}

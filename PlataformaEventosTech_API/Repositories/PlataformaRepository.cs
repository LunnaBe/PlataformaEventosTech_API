using Microsoft.EntityFrameworkCore;
using PlataformaEventosTech_API.Data;
using PlataformaEventosTech_API.Models;
using PlataformaEventosTech_API.Repositories.Interfaces;

namespace PlataformaEventosTech_API.Repositories
{
    /// <summary>
    /// PlataformaRepository - classe de repositório
    /// responsável por acessor os dados dos eventos no banco de dados
    /// </summary>
    public class PlataformaRepository : IPlataformaRepository
    {
        /// <summary>
        /// AppDbContext - contexto do banco de dados - responsável por
        /// gerenciar a conexão com o banco de dados e fornecer acesso
        /// is tabelas e entidades do banco de dados
        /// </summary> 
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor da classe - recebeu o contexto do banco de dados 
        /// </summary>
        /// <param name="context"></param>"
        public PlataformaRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// O metódo GetAll é responsável 
        /// por retornar uma lista de todos os eventos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<List<Evento>> GetAll() => await _context.Eventos.ToListAsync();


        /// <summary>
        /// GetById é responsável por retornar um evento específico por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Evento> GetById(int id) => await _context.Eventos.FindAsync(id);

        /// <summary>
        /// Add é responsável por adicionar um novo 
        /// evento ao banco de dados
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public async Task Add(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update é responsável por atualizar um evento
        /// existente no banco de dados
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public async Task Update(Evento evento)
        {
            try
            {
                var evento_existente = await _context.Eventos.FindAsync(evento.Id);

                //Coloca um aviso genérico caso o evento não seja encontrado
                if (evento_existente == null)
                {
                    throw new Exception("Evento não encontrado");
                }

                //Atualiza os valores do objeto existente com os novo objeto
                _context.Entry(evento_existente).CurrentValues.SetValues(evento);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o evento");
            }

        }

        /// <summary>
        /// Delete é responsável por excluir um evento
        /// do banco de dados com base no id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            try
            {
                var evento = await _context.Eventos.FindAsync(id);

                if (evento == null)
                {
                    throw new Exception("Evento não encontrado");
                }
                _context.Eventos.Remove(evento); 
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar o evento");
            }
        }

    }
}

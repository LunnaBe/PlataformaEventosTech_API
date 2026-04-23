using Microsoft.AspNetCore.Mvc;
using PlataformaEventosTech_API.Models;
using PlataformaEventosTech_API.Services;

namespace PlataformaEventosTech_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlataformaController : ControllerBase
    {
        /// <summary>
        /// Instancia do serviço de plataforma - responsável por toda 
        /// a lógica de negócios relacionada a plataforma
        /// </summary>

        private readonly PlataformaService _service;


        /// <summary>
        /// Construtor da classe - recebe o serviço de 
        /// plataforma via injeção de dependência
        /// </summary> 
        /// <param name="service"></param>

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET: api/evento- retorna uma lista de eventos cadastrados no banco de dados
        /// </summary>
        /// 
        /// <remarks> 
        /// Uma requisição que retorna uma lista de eventos de uma plataforma, cadastrados no banco de dados, 
        /// onde cada evento possui as seguintes propriedades: Id, Nome, Conteudo, Data_Hora, Localizacao, Preco. 
        /// 
        /// Observação: O endpoint deve retornar uma lista de eventos, 
        /// ou um status de erro apropriado se nenhum evento for encontrado ou se a requisição for inválida.
        /// </remarks>
        /// 
        /// <returns></returns>
        /// <response code="200">Evento encontrado com sucesso</response>
        /// <response code="204">Nenhum conteúdo para retornar</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="409">Conflito de dados</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var produtos = await _service.Listar();
            return Ok(produtos);
        }

        /// <summary>
        /// GET: api/evento/{id} - Retorna um evento específico por ID
        /// </summary>
        /// 
        /// <remarks>
        /// Exemplo de resposta de sucesso:
        /// {
        ///     "id": 0,
        ///     "nome": "IAM TECH DAY",
        ///     "conteudo": "O IAM Tech Day está de volta em SP trazendo em 2 dias o melhor conteúdo de IAM por especialistas 
        ///     e profissionais do mercado.",
        ///     "data_hora": "2024-06-01 18:00:00",
        ///     "localizacao": "São Paulo",
        ///     "preco": 100.0
        /// }
        /// 
        /// Obervação: O endpoint deve retornar o evento correspondente ao ID fornecido, 
        /// ou um status de erro apropriado se o evento não for encontrado ou se a requisição for inválida.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns> 
        /// <response code="200">Evento encontrado com sucesso</response>
        /// <response code="204">Nenhum conteúdo para retornar</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="409">Conflito de dados</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {    
                var evento = await _service.ObterPorId(id);

                if (evento == null)
                {
                    return NotFound($"O evento com ID {id} não foi encontrado");
                }
               
                return Ok(new {evento});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao buscar o evento");
            }
        }

        /// <summary>
        /// POST: api/evento - Cria um novo evento no banco de dados
        /// </summary>
        /// 
        /// <remarks>
        /// Requisitos de validação:
        /// - Nome: obrigatório, máximo de 100 caracteres
        /// - Conteudo: obrigatório, máximo de 500 caracteres
        /// - Data_Hora: obrigatório, deve ser uma data futura
        /// - Localizacao: obrigatório, máximo de 200 caracteres
        /// - Preco: obrigatório, deve ser um valor positivo
        /// 
        /// Observação: O ID do evento é gerado automaticamente pelo banco de dados e não deve ser fornecido na requisição de criação.
        /// </remarks>
        /// 
        /// <param name="evento"></param>
        /// 
        /// <returns></returns> 
        /// <response code="200">Evento encontrado com sucesso</response>
        /// <response code="201">Evento criado com sucesso</response>
        /// <response code="204">Nenhum conteúdo para retornar</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="409">Conflito de dados</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Evento evento)
        {
           
            try
            {
                if (evento == null)
                {
                    return BadRequest(new { mensagem = "Dados do evento não foram fornecidos" });
                }

                await _service.Criar(evento);
                return CreatedAtAction(nameof(GetById), new { id = evento.Id }, evento);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o evento no banco de dados");
            }
        }

        /// <summary>
        /// PUT: api/evento/{id} - Atualiza um evento existente por ID
        /// </summary>
        /// 
        /// <remarks>
        /// Observação: O endpoint PUT é utilizado para atualizar um evento existente por ID. 
        /// Ele recebe o ID do evento a ser atualizado na URL e os dados atualizados do evento no corpo da requisição.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <param name="evento"></param>
        /// 
        /// <returns></returns>
        /// <response code="200">Evento encontrado com sucesso</response>
        /// <response code="204">Nenhum conteúdo para retornar</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="409">Conflito de dados</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Evento evento)
        {
            try
            {
                var existente = await _service.ObterPorId(id);

                if (id != evento.Id)
                    return BadRequest("O ID do evento no corpo da requisição deve corresponder ao ID na URL");

                

                if (existente == null)
                    return NotFound($"O curso com ID {id} não foi encontrado");

                await _service.Atualizar(evento);
                return Ok("Evento atualizado na plataforma com sucesso!!");

            } catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao validar os dados: {ex.Message}");
            }
                 
        }

        /// <summary>
        /// DELETE: api/evento/{id} - deleta um evento por ID
        /// </summary>
        /// 
        /// <remarks> 
        /// Observação: O endpoint DELETE é utilizado para deletar um evento existente por ID. 
        /// Ele recebe o ID do evento a ser deletado na URL.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// <response code="200">Evento encontrado com sucesso</response>
        /// <response code="204">Nenhum conteúdo para retornar</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { mensagem = "Id negativo inválido" });
            }

            try
            {
                var evento = await _service.ObterPorId(id);

                if (evento == null)
                {
                    return NotFound(new { mensagem = $"O evento com ID {id} não foi encontrado" });
                }

                await _service.Deletar(id);
                return Ok(new { mensagem = "Evento deletado!!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar o evento");
            }
        }
    }
}


# 🚀 Plataforma Eventos Tech API
Esta é uma API robusta desenvolvida em ASP.NET Core para o gerenciamento de eventos tecnológicos. A aplicação permite realizar operações completas de CRUD (Create, Read, Update, Delete) com validações de dados e tratamento de erros.

# 🛠 Tecnologias Utilizadas
- Runtime: .NET 8.0 (ou superior)

- Linguagem: C#

- Framework: ASP.NET Core Web API

- Documentação: Swagger (OpenAPI)

- Padrão de Arquitetura: Service Layer com Injeção de Dependência

## Passos
### 1. Clonar o repositório:
```bash
git clone https://github.com/seu-usuario/PlataformaEventosTech_API.git 
cd PlataformaEventosTech_API
```

### 2. Restaurar dependências:
```bash
dotnet restore
```
### 3.Executar a aplicação:
```bash
dotnet run
```
### 4.Acessar a documentação:
Após iniciar, a API estará disponível em http://localhost:5000 (ou porta configurada). Acesse o Swagger para testar os endpoints em:
```bash
http://localhost:{porta}/swagger/index.html
```
# 📡 Documentação da API
Todos os endpoints utilizam o prefixo base: api/Plataforma

| Método | Endpoint | Descrição |
|-----------|--------------|--------------|
|GET	| /api/Plataforma |	Lista todos os eventos cadastrados. |
|GET	| /api/Plataforma/{id} |	Busca os detalhes de um evento específico por ID. |
|POST | /api/Plataforma	| Cadastra um novo evento no sistema. |
|PUT	| /api/Plataforma/{id}	| Atualiza os dados de um evento existente. |
|DELETE |	/api/Plataforma/{id} | Remove um evento do banco de dados. |

### Respostas Comuns
- **200 OK:** Sucesso na operação.

- **201 Created:** Recurso criado com sucesso (retornado no POST).

- **400 Bad Request:** Dados inválidos ou erro de validação.

- **404 Not Found:** Evento não localizado.

- **500 Internal Server Error:** Erro inesperado no servidor.

### Regras de Negócio para Cadastro (POST)
- **Nome:** Obrigatório, máximo de 100 caracteres.

- **Conteúdo:** Obrigatório, máximo de 500 caracteres.

- **Data_Hora:** Obrigatório, deve ser uma data futura.

- **Localização:** Obrigatório, máximo de 200 caracteres.

- **Preço:** Obrigatório e deve ser um valor positivo.

- **ID:** Não deve ser enviado no POST (gerado automaticamente).


# 🛡️ Tratamento de Erros
A API possui blocos **try-catch** em seus métodos principais para garantir que falhas internas não exponham dados sensíveis e retornem mensagens amigáveis ao usuário via Status Code 500.

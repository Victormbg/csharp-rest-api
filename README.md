# C# REST API Template (.NET 8 + SQLite)

Um template mínimo e profissional de API REST em C# construído com .NET 8, Entity Framework Core e SQLite, pré-configurado para desenvolvimento instantâneo no GitHub Codespaces e VS Code.

Este projeto implementa uma **Arquitetura em Camadas (N-Layer)** limpa, separando o tratamento HTTP, autenticação OAuth2 / JWT, filtros de validação, tratamento global de erros, lógica de negócios, modelos de domínio e acesso ao banco de dados.
---

## 🛠️ Tecnologias Utilizadas

- **.NET 8 (LTS):** Framework robusto para desenvolvimento backend de alta performance.
- **ASP.NET Core Web API:** Estrutura para construção de serviços REST baseados em Controllers.
- **JWT Bearer Authentication:** Autenticação via tokens JWT (OAuth 2.0 Client Credentials Flow).
- **Entity Framework Core 8:** ORM para mapeamento e manipulação do banco de dados relacional.
- **SQLite:** Banco de dados relacional leve embutido diretamente na aplicação em arquivo local (`Data/banco.db`).
- **GitHub Codespaces & DevContainers:** Ambiente de desenvolvimento totalmente automatizado em nuvem via VS Code.

---

## 📂 Estrutura do Projeto

A arquitetura do projeto segue o padrão **N-Layer** para separação clara de responsabilidades:

```text
.
├── .devcontainer/         # Configurações de automação do GitHub Codespaces e VS Code
│   ├── devcontainer.json # Instalação do SDK, extensões e pacotes do container
│   └── settings.json     # Ajustes do VS Code (C# Dev Kit e oculta pastas bin/obj)
├── Controllers/          # Camada de Apresentação (Endpoints REST HTTP)
│   ├── AuthController.cs # Emissão de tokens OAuth 2.0 (/token)
│   └── ProdutosController.cs # CRUD de produtos protegido
├── Filters/              # Filtros de Ação e Validação pré-execução
│   ├── ApiKeyFilter.cs   # Validação da header x-api-key
│   └── ValidacaoProdutoFilter.cs # Validação do payload de entrada
├── Middlewares/          # Middlewares de pipeline HTTP
│   └── TratamentoErrosMiddleware.cs # Interceptador global de exceções (400, 401, 404, 500)
├── Services/             # Camada de Negócio (Lógica da aplicação e chamadas do EF Core)
│   └── ProdutoService.cs
├── Models/               # Camada de Domínio (Entidades da aplicação)
│   └── Produto.cs
├── Data/                 # Camada de Acesso a Dados e Infraestrutura
│   ├── AppDbContext.cs   # Contexto do Entity Framework Core
│   ├── DatabaseConfig.cs # Métodos de extensão para inicialização e conexão do SQLite
│   └── banco.db          # Arquivo físico do banco SQLite
├── appsettings.json      # Configurações de ambiente, credenciais e Connection Strings
├── Program.cs            # Ponto de entrada (Bootstrapper, Middlewares e Injeção de Dependências)
├── csharp-rest-api.csproj # Configurações do projeto .NET e pacotes NuGet
└── .gitignore            # Regras para ignorar arquivos de compilação e banco local
```

---

## 🔐 Autenticação e Segurança

A API é protegida por duas camadas simultâneas de segurança:

1. OAuth 2.0 (Client Credentials Flow): Para obter o token JWT, envie uma requisição POST /token com client_id, client_secret e grant_type=client_credentials.
2. Double Header Requirement: Todas as chamadas para os endpoints de negócio (/api/produtos) exigem os headers:
   - Authorization: Bearer <access_token>
   - x-api-key: <sua_api_key>

---

## 🚀 Como Executar

1. Restaurar dependências:
   dotnet restore

2. Executar a aplicação:
   dotnet run

3. Obter Token de Acesso (POST /token):
   curl -X POST http://localhost:8080/token \
     -H "Content-Type: application/x-www-form-urlencoded" \
     -d "grant_type=client_credentials&client_id=csharp-rest-api-dev&client_secret=c2VjX2Rldl85ZjhhM2IxYzJkNGU1ZjZhN2I4YzlkMGUxZjJhM2I0Yw=="

4. Consumir a API Protegida (GET /api/produtos):
   curl -X GET http://localhost:8080/api/produtos \
     -H "Authorization: Bearer SEU_TOKEN_AQUI" \
     -H "x-api-key: ZGV2X2tleV83YTlmMmM4ZTRiMWQ2YTNmNWUwYjJjNGQ2YThmMWUzYg=="

---

## 🔍 Como Inspecionar o Banco SQLite

### Opção 1: Via CLI no Terminal (sqlite3)
Caso precise inspecionar as tabelas e dados diretamente no terminal do Linux/Codespaces:

1. Acesse o arquivo de banco de dados:
   ```bash
   sqlite3 Data/banco.db
   ```

2. Listar todas as tabelas do banco:
   ```sql
   .tables
   ```

3. Visualizar a estrutura (schema) da tabela de produtos:
   ```sql
   .schema Produtos
   ```

4. Consultar os dados gravados:
   ```sql
   SELECT * FROM Produtos;
   ```

5. Para sair do CLI do SQLite:
   ```sql
   .exit
   ```
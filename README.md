# C# REST API Template (.NET 8 + SQLite)

Um template mínimo e profissional de API REST em C# construído com .NET 8, Entity Framework Core e SQLite, pré-configurado para desenvolvimento instantâneo no GitHub Codespaces e VS Code.

Este projeto implementa uma **Arquitetura em Camadas (N-Layer)** limpa, separando o tratamento HTTP, filtros de validação, lógica de negócios, modelos de domínio e acesso ao banco de dados.

---

## 🛠️ Tecnologias Utilizadas

* **.NET 8 (LTS):** Framework robusto para desenvolvimento backend de alta performance.
* **ASP.NET Core Web API:** Estrutura para construção de serviços REST baseados em Controllers.
* **Entity Framework Core 8:** ORM para mapeamento e manipulação do banco de dados relacional.
* **SQLite:** Banco de dados relacional leve embutido diretamente na aplicação em arquivo local (`Data/banco.db`).
* **GitHub Codespaces & DevContainers:** Ambiente de desenvolvimento totalmente automatizado em nuvem via VS Code.

---

## 📂 Estrutura do Projeto

A arquitetura do projeto segue o padrão **N-Layer** para separação clara de responsabilidades:

```text
.
├── .devcontainer/        # Configurações de automação do GitHub Codespaces e VS Code
│   ├── devcontainer.json # Instalação do SDK, extensões e pacotes do container
│   └── settings.json     # Ajustes do VS Code (C# Dev Kit e oculta pastas bin/obj)
├── Controllers/          # Camada de Apresentação (Recebe requisições HTTP e retorna respostas)
│   └── ProdutosController.cs
├── Filters/              # Middlewares e Filtros de validação pré-execução
│   └── ValidacaoProdutoFilter.cs
├── Services/             # Camada de Negócio (Lógica da aplicação e chamadas do EF Core)
│   └── ProdutoService.cs
├── Models/               # Camada de Domínio (Entidades da aplicação)
│   └── Produto.cs
├── Data/                 # Camada de Acesso a Dados e Infraestrutura
│   ├── AppDbContext.cs   # Contexto do Entity Framework Core
│   ├── DatabaseConfig.cs # Métodos de extensão para inicialização e conexão do SQLite
│   └── banco.db          # Arquivo físico do banco SQLite
├── appsettings.json      # Configurações de ambiente e Connection Strings
├── Program.cs            # Ponto de entrada (Bootstrapper e Injeção de Dependências)
├── csharp-rest-api.csproj # Configurações do projeto .NET e pacotes NuGet
└── .gitignore            # Regras para ignorar arquivos de compilação e banco local
```

---

## 🚀 Como Executar o Projeto

No terminal do VS Code / Codespaces, utilize os comandos abaixo conforme a sua necessidade:

### 1. Execução Normal
Para rodar a aplicação na porta 8080:
```bash
dotnet run
```

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

---

## 📌 Endpoints da API

* **GET** `/api/produtos` — Lista todos os produtos cadastrados.
* **POST** `/api/produtos` — Cadastra um novo produto.

### Exemplo de Payload para `POST`:
```json
{
  "nome": "Teclado Mecânico",
  "preco": 250.00
}
```
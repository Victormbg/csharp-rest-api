# C# REST API Template (.NET 8 + SQLite)

Template mínimo e profissional para desenvolvimento de APIs REST em C# utilizando GitHub Codespaces, Entity Framework Core e SQLite.

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
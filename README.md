# 🏍️ MotoConnect API

> RESTful API desenvolvida em ASP.NET Core, seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD), com integração ao Oracle Database. Este projeto é uma refatoração do Challenge FIAP 2025, com foco em modularidade, testabilidade e boas práticas de Clean Code.

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)

## 📋 Sobre

O MotoConnect é uma solução robusta para gerenciamento completo de motocicletas, abrangendo o controle de veículos, usuários e histórico de manutenções. Esta API foi refatorada para aderir aos princípios de Clean Architecture e DDD, garantindo uma base de código mais organizada, escalável e de fácil manutenção. Ela fornece uma interface backend completa com suporte a operações CRUD para todas as entidades do sistema, com foco em segurança, performance e clareza de código.

## 📦 Tecnologias Utilizadas

- **ASP.NET Core 9**
- **Oracle Database** (via Oracle.EntityFrameworkCore)
- **Entity Framework Core**
- **Swagger/OpenAPI** (Swashbuckle)
- **C#**
- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **Value Objects**
- **Princípios SOLID**

## 🔗 Endpoints Disponíveis

### 🔧 Vehicles

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/Vehicles` | Listar todas as motos | `200 OK` |
| POST | `/api/Vehicles` | Criar nova moto | `201 Created`, `400 Bad Request` |
| GET | `/api/Vehicles/{id}` | Buscar moto por ID | `200 OK`, `404 Not Found` |
| PUT | `/api/Vehicles/{id}` | Atualizar moto | `204 No Content`, `400 Bad Request`, `404 Not Found`, `500 Internal Server Error` |
| DELETE | `/api/Vehicles/{id}` | Deletar moto | `204 No Content`, `404 Not Found` |

### 👤 Users

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/User` | Listar usuários | `200 OK` |
| POST | `/api/User` | Criar usuário | `201 Created` |
| GET | `/api/User/{id}` | Buscar usuário por ID | `200 OK`, `404 Not Found` |
| PUT | `/api/User/{id}` | Atualizar usuário | `204 No Content`, `400 Bad Request`, `404 Not Found` |
| DELETE | `/api/User/{id}` | Deletar usuário | `204 No Content`, `404 Not Found` |

### 🛠️ MaintenanceHistories

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| GET | `/api/Histories` | Listar manutenções | `200 OK` |
| POST | `/api/Histories` | Criar registro de manutenção | `201 Created` |
| GET | `/api/Histories/{id}` | Detalhar manutenção | `200 OK`, `404 Not Found` |
| PUT | `/api/Histories/{id}` | Atualizar manutenção | `204 No Content`, `400 Bad Request`, `404 Not Found` |
| DELETE | `/api/Histories/{id}` | Remover manutenção | `204 No Content`, `404 Not Found` |

## ▶️ Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seuusuario/seurepo.git
   ```

2. **Configuração do Banco de Dados:**
   - Certifique-se de ter acesso a um banco de dados Oracle.
   - Atualize a string de conexão no arquivo `src/Api/appsettings.json` com suas credenciais e detalhes do banco de dados:
     ```json
     "ConnectionStrings": {
         "Oracle": "Data Source=<seu_servidor>:<sua_porta>/<seu_servico>;User ID=<seu_usuario>;Password=<sua_senha>;"
     }
     ```
   - Navegue até o diretório `src/Infrastructure` e aplique as migrações para criar o esquema do banco de dados:
     ```bash
     dotnet ef database update --project Infrastructure.csproj --startup-project ../Api/Api.csproj
     ```

3. **Execute os comandos:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/Api/Api.csproj --launch-profile "https"
   ```

4. **Acesse a documentação Swagger:**
   Com a aplicação em execução, acesse `https://localhost:5214/swagger` (ou a porta configurada) no seu navegador para explorar os endpoints da API.

## 📁 Estrutura do Projeto

```
. (raiz do projeto)
├── src
│   ├── Api             # Camada de Apresentação (Controllers, Program.cs, appsettings.json)
│   ├── Application     # Camada de Aplicação (DTOs, Services, Interfaces de Serviços)
│   ├── Domain          # Camada de Domínio (Entidades, Value Objects, Interfaces de Repositório)
│   └── Infrastructure  # Camada de Infraestrutura (Contexto EF Core, Mapeamentos, Repositórios)
├── chalenge-moto-connect.sln # Arquivo de solução do Visual Studio
└── README.md         # Este arquivo
```

## 👨‍💻 Desenvolvedores

- **Mateus H. Souza** - RM: 558424
- **Cauan Passos** - RM: 555466

## 📄 Licença

Este projeto possui licença para uso educacional como parte de avaliação acadêmica.


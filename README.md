# 🏍️ MotoConnect API - V 0.1.1 - CP 02

> RESTful API desenvolvida em ASP.NET Core com integração ao Oracle Database, como parte do Challenge FIAP 2025.

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Oracle](https://img.shields.io/badge/Oracle-F80000?style=flat-square&logo=oracle&logoColor=white)](https://www.oracle.com/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)

## 📋 Sobre

O MotoConnect é uma solução para gerenciamento completo de motocicletas, permitindo o controle de veículos, usuários e histórico de manutenções. Esta API fornece uma interface backend robusta com suporte completo a operações CRUD para todas as entidades do sistema.

## 📦 Tecnologias Utilizadas

- **ASP.NET Core 8**
- **Oracle Database** (via Oracle.EntityFrameworkCore)
- **Entity Framework Core**
- **Swagger** (Swashbuckle)
- **C#**

## 🔗 Endpoints Disponíveis

### 🔧 Vehicles

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/Vehicles` | Listar todas as motos |
| POST | `/api/Vehicles` | Criar nova moto |
| GET | `/api/Vehicles/{id}` | Buscar moto por ID |
| PUT | `/api/Vehicles/{id}` | Atualizar moto |
| DELETE | `/api/Vehicles/{id}` | Deletar moto |

### 👤 Users

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/User` | Listar usuários |
| POST | `/api/User` | Criar usuário |
| GET | `/api/User/{id}` | Buscar usuário por ID |
| PUT | `/api/User/{id}` | Atualizar usuário |
| DELETE | `/api/User/{id}` | Deletar usuário |

### 🛠️ MaintenanceHistories

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/MaintenanceHistories` | Listar manutenções |
| POST | `/api/MaintenanceHistories` | Criar registro de manutenção |
| GET | `/api/MaintenanceHistories/{id}` | Detalhar manutenção |
| PUT | `/api/MaintenanceHistories/{id}` | Atualizar manutenção |
| DELETE | `/api/MaintenanceHistories/{id}` | Remover manutenção |

## ▶️ Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seuusuario/seurepo.git
   ```

2. **Configure a ConnectionString** no `appsettings.json` com suas credenciais Oracle:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=seu_datasource;"
     }
   }
   ```

3. **Execute os comandos:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

4. **Acesse a documentação Swagger:**
   ```
   https://localhost:5214/swagger
   ```

## 📁 Estrutura do Projeto

```
/Controllers          # Controllers REST (Vehicle, User, Maintenance)
/Models               # Entidades do domínio
/Data                 # Contexto Oracle (DbContext)
/Migrations           # Scripts de criação via EF Core
Program.cs            # Configuração de endpoints e serviços
```

## 👨‍💻 Desenvolvedores

- **Mateus H. Souza** - RM: 558424
- **Gustavo Lazzuri** - RM: 556772  
- **Cauan Passos** - RM: 555466

## 📄 Licença

Este projeto possui licença para uso educacional como parte de avaliação acadêmica.

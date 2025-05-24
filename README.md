# 🏍️ MotoConnect API

> RESTful API desenvolvida em ASP.NET Core com integração ao Oracle Database, como parte do Challenge FIAP 2025.

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
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
| GET | `/api/Histories` | Listar manutenções |
| POST | `/api/Histories` | Criar registro de manutenção |
| GET | `/api/Histories/{id}` | Detalhar manutenção |
| PUT | `/api/Histories/{id}` | Atualizar manutenção |
| DELETE | `/api/Histories/{id}` | Remover manutenção |

## ▶️ Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seuusuario/seurepo.git
   ```

2. já está pré configurado o acesso com minhas credenciais e as tabelas já estão criadas no banco de dados

3. **Execute os comandos:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

4. **Acesse a documentação Swagger:** O app está em modo Development por isso rode com o método HTTP e será redirecionado automaticamente, caso acesse com HTTPS deverá inserir a URL manual
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
- **Lucas Fialho** - RM: 557884
- **Cauan Passos** - RM: 555466

## 📄 Licença

Este projeto possui licença para uso educacional como parte de avaliação acadêmica.

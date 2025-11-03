# 🏍️ Moto Connect - Challenge (4ª Entrega)

> API RESTful desenvolvida em ASP.NET Core 8.0, seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD), com foco em boas práticas REST, segurança, observabilidade e Machine Learning.

[![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)
[![xUnit](https://img.shields.io/badge/Tests-xUnit-000000?style=flat-square&logo=xunit)](https://xunit.net/)

## 📋 Sobre

O Moto Connect é uma solução para gerenciamento de motocicletas. Esta versão da API foi atualizada para a 4ª entrega do Challenge, incorporando requisitos avançados de desenvolvimento, DevOps e Arquiteturas Disruptivas.

## ✨ Requisitos da 4ª Entrega Implementados

| Requisito | Pontuação | Status |
| :--- | :--- | :--- |
| **Health Checks** | 10 pts | Implementado no endpoint `/health`. |
| **Versionamento da API** | 10 pts | Implementado via URL (`/api/v{version}/...`). |
| **Segurança da API (JWT)** | 25 pts | Implementado autenticação via Bearer Token (JWT). |
| **Integração ML.NET** | 25 pts | Adicionado endpoint de predição de manutenção (`/api/v1/ml/predict-maintenance`). |
| **Testes Unitários com xUnit** | 30 pts | Projeto de testes (`challenge-moto-connect.Tests`) adicionado com testes unitários e de integração. |
| **Estrutura para IoT/Visão Computacional** | - | Adicionado endpoint `/api/telemetry` e entidade `TelemetryData`. |
| **Preparação para DevOps (App Service)** | - | Criados `script_bd.sql` e `azure_cli_scripts.md` para deploy na Azure. |

## 📦 Tecnologias Utilizadas

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **JWT Bearer Authentication**
- **ML.NET** (Machine Learning)
- **xUnit & Moq** (Testes)
- **Swagger/OpenAPI**
- **Clean Architecture & DDD**

## 🔗 Endpoints Principais

| Método | Endpoint | Descrição | Segurança |
| :--- | :--- | :--- | :--- |
| **POST** | `/api/v1/auth/login` | Gera o token JWT para acesso. | **Livre** |
| **GET** | `/health` | Verifica a saúde da API e do banco de dados. | **Livre** |
| **POST** | `/api/v1/ml/predict-maintenance` | Predição de necessidade de manutenção (ML.NET). | **JWT** |
| **POST** | `/api/telemetry` | Recebe dados de telemetria (IoT/Visão Computacional). | **Livre** |
| **GET** | `/api/v1/users` | Lista usuários (com paginação e HATEOAS). | **JWT** |
| **GET** | `/api/v1/vehicles` | Lista veículos (com paginação e HATEOAS). | **JWT** |
| **GET** | `/api/v1/histories` | Lista históricos (com paginação e HATEOAS). | **JWT** |

## 🔑 Como Obter e Usar o Token JWT

1.  **Obter o Token:**
    Faça uma requisição `POST` para o endpoint de login:
    - **URL:** `/api/v1/auth/login`
    - **Body (JSON):**
      ```json
      {
        "email": "seu_email@exemplo.com",
        "password": "sua_senha"
      }
      ```
    *Nota: A lógica de autenticação no `AuthController` é uma simulação. Para testes, use qualquer email/senha não vazios.*

2.  **Usar o Token:**
    Para acessar os endpoints protegidos, inclua o token retornado no cabeçalho `Authorization` da sua requisição:
    ```
    Authorization: Bearer <SEU_TOKEN_JWT>
    ```

## ▶️ Como Executar

1.  **Restaure as dependências:**
    ```bash
    dotnet restore
    ```

2.  **Execute a aplicação:**
    ```bash
    dotnet run --project src/Api/Api.csproj
    ```

3.  **Acesse a documentação Swagger:**
    Acesse `https://localhost:<porta>/swagger` para testar os endpoints.

## 🧪 Execução dos Testes

Para executar os testes unitários e de integração, siga os passos abaixo:

1.  **Navegue até o diretório raiz do projeto:**
    ```bash
    cd challenge-moto-connect
    ```

2.  **Execute os testes:**
    ```bash
    dotnet test
    ```

## 📁 Arquivos de Apoio para DevOps

- `script_bd.sql`: Script DDL para criação da nova tabela `TelemetryData`.
- `azure_cli_scripts.md`: Estrutura de comandos Azure CLI para provisionamento de App Service e SQL Server.

## 👨‍💻 Desenvolvedores

- **Mateus H. Souza** - RM: 558424
- **Cauan Passos** - RM: 555466
- **Lucas Fialho** - RM: 557884
(Mantendo os nomes originais)

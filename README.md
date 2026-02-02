# 💳 Payments Backend Challenge

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)](https://www.docker.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)

> Solução completa do **[PicPay Simplificado Backend Challenge](https://github.com/PicPay/picpay-desafio-backend)** desenvolvida com .NET 10, aplicando Clean Architecture, Domain-Driven Design (DDD) e princípios SOLID.

## Sobre o Projeto

Este projeto foi desenvolvido como solução para o desafio técnico do PicPay, implementando uma plataforma de pagamentos simplificada onde usuários podem depositar e realizar transferências de dinheiro. A solução demonstra a aplicação prática de padrões arquiteturais modernos, boas práticas de desenvolvimento e código limpo.

**Desafio Original:** [PicPay Simplificado - Backend Challenge](https://github.com/PicPay/picpay-desafio-backend)

###  Funcionalidades Principais

- ✅ **Registro de Usuários** - Cadastro com validação de CPF e email únicos
- ✅ **Dois Tipos de Usuário** - Clientes (podem enviar e receber) e Lojistas (apenas recebem)
- ✅ **Autenticação Segura** - Hash de senhas utilizando ASP.NET Identity
- ✅ **Gestão de Carteiras** - Criação automática de carteira digital para cada usuário
- ✅ **Processamento de Pagamentos** - Sistema transacional com rollback automático
- ✅ **Validação de Negócio** - Regras de domínio aplicadas através de políticas e serviços
- ✅ **Autorização Externa** - Integração com serviço externo de autorização
- ✅ **Notificações** - Sistema de notificação de transações
- ✅ **Containerização Completa** - Docker e Docker Compose prontos para uso

## Arquitetura

O projeto segue rigorosamente os princípios de **Clean Architecture** e **SOLID**, garantindo código testável, manutenível e escalável.

```
Payments.Backend.Challenge/
│
├── 📁 Payments.Backend.Challenge.API/          # Camada de Apresentação
│   ├── Controllers/                            # Endpoints REST
│   ├── Dockerfile                              # Containerização da API
│   └── Program.cs                              # Configuração e DI
│
├── 📁 Payments.Backend.Challenge.Application/  # Camada de Aplicação
│   ├── UseCases/                              # Casos de uso (orquestradores)
│   ├── DTOs/                                  # Objetos de transferência de dados
│   └── Interfaces/                            # Contratos da aplicação
│
├── 📁 Payments.Backend.Challenge.Domain/       # Camada de Domínio (Core)
│   ├── Entities/                              # Entidades ricas do domínio
│   ├── Services/                              # Serviços e políticas de domínio
│   ├── Interfaces/                            # Contratos do domínio
│   └── Enums/                                 # Enumerações
│
├── 📁 Payments.Backend.Challenge.Infrastructure/ # Camada de Infraestrutura
│   ├── Persistence/                            # Contexto do EF Core
│   ├── Repositories/                           # Implementação de repositórios
│   ├── Security/                               # Implementação de segurança
│   ├── Services/                               # Serviços externos
│   └── Migrations/                             # Migrações do banco
│
└── docker-compose.yml                          # Orquestração de containers
```

### Princípios SOLID Aplicados

Este projeto foi desenvolvido com **rigorosa aplicação dos princípios SOLID**:

#### **S** - Single Responsibility Principle 
Cada classe tem uma única responsabilidade bem definida:
- `RegisterUser` - Orquestra apenas o registro de usuários
- `PaymentCoordinator` - Coordena exclusivamente o fluxo de pagamentos
- `UserUniquesPolicy` - Valida apenas a unicidade de usuários
- `PaymentService` - Executa somente a lógica de transferência

#### **O** - Open/Closed Principle 
Código aberto para extensão, fechado para modificação através de abstrações:
```csharp
// Interfaces permitem trocar implementações sem alterar use cases
IPasswordHasher passwordHasher
IExternalAuthorizationMock authorizationService
```

#### **L** - Liskov Substitution Principle 
Todas as implementações podem ser substituídas por suas abstrações:
```csharp
// Qualquer implementação de IUserRepository funciona perfeitamente
IUserRepository userRepository
```

#### **I** - Interface Segregation Principle 
Interfaces específicas e enxutas, sem métodos desnecessários:
```csharp
public interface IRegisterUser
{
    Task<OperationResultDto<RegisterUserResponseDto>> ExecuteAsync(RegisterUserRequestDto request);
}
```

#### **D** - Dependency Inversion Principle 
Dependência de abstrações, não de implementações concretas:
```csharp
public class RegisterUser(
    IUserRepository userRepository,        // ← Abstração
    IWalletRepository walletRepository,    // ← Abstração
    UserUniquesPolicy userUniquesPolicy,
    IPasswordHasher passwordHasher,        // ← Abstração
    ILogger<RegisterUser> logger)
```

### Padrões e Práticas Aplicadas

- ✅ **Clean Architecture** - Separação de responsabilidades em camadas
- ✅ **Domain-Driven Design (DDD)** - Modelagem rica do domínio
- ✅ **SOLID Principles** - Código coeso, desacoplado e testável
- ✅ **Repository Pattern** - Abstração da camada de dados
- ✅ **Unit of Work** - Gerenciamento transacional
- ✅ **Dependency Injection** - Inversão de controle nativa do .NET
- ✅ **Policy Pattern** - Validações e regras de negócio encapsuladas
- ✅ **Fail-Fast Validation** - Validações no domínio

##  Tecnologias Utilizadas

### Core
- **.NET 10** - Framework principal
- **C# 12** - Linguagem de programação
- **ASP.NET Core** - Framework web minimalista

### Persistência
- **Entity Framework Core 10** - ORM
- **SQL Server 2022** - Banco de dados relacional

### DevOps
- **Docker** - Containerização da aplicação
- **Docker Compose** - Orquestração de containers
- **Multi-stage Build** - Otimização de imagens Docker

### Ferramentas
- **Dependency Injection** - Injeção de dependências nativa
- **Logging** - Microsoft.Extensions.Logging
- **ASP.NET Identity** - Hash de senhas

## Docker 

A aplicação está **completamente dockerizada**, facilitando o setup e testes. Com apenas um comando, você sobe toda a infraestrutura!

###  O que está incluído?

- **API .NET** - Aplicação completa com todas as camadas
- **SQL Server 2022** - Banco de dados em container
- **Migrations Automáticas** - Banco criado automaticamente na inicialização
- **Health Checks** - Verificação de saúde dos serviços
- **Volumes Persistentes** - Dados não são perdidos ao parar os containers

###  Executando com Docker (Recomendado)

**Pré-requisitos:**
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

**1. Clone o repositório**
```bash
git clone https://github.com/PauloAnjos22/payments-backend-challenge.git
cd payments-backend-challenge
```

**2. Configure a senha do banco (opcional)**

Crie um arquivo `.env` na raiz do projeto:
```env
SQL_SERVER_MANAGEMENT_DB_PASSWORD=SuaSenhaForte123!
```

Ou use a senha padrão já configurada no `docker-compose.yml`.

**3. Suba os containers**
```bash
docker compose up --build
```


A aplicação estará disponível em: `http://localhost:5000`

O banco de dados estará acessível em: `localhost:1433`
- **Usuário:** `sa`
- **Senha:** A definida no `.env` ou padrão do compose


###  Vantagens da Dockerização

✅ **Setup em segundos** - Não precisa instalar SQL Server ou .NET localmente  
✅ **Ambiente consistente** - Funciona igual em Windows, Mac e Linux  
✅ **Isolamento** - Não interfere com outras aplicações  
✅ **Fácil compartilhamento** - Outros desenvolvedores podem testar facilmente  
✅ **CI/CD Ready** - Pronto para pipelines de integração contínua  



##  Endpoints da API

###  Usuários

#### **POST** `/api/user` - Registrar Novo Usuário

**Request:**
```json
{
  "fullName": "João Silva",
  "cpf": "12345678901",
  "email": "joao.silva@email.com",
  "password": "SenhaSegura123",
  "type": 1
}
```

**Tipos de Usuário:**
- `1` - **Customer** (Cliente - pode enviar e receber pagamentos)
- `2` - **StoreOwner** (Lojista - pode apenas receber pagamentos)

**Response 200 (Success):**
```json
{
  "success": true,
  "data": {
    "userId": "123",
    "walletId": "456"
  },
  "error": null
}
```

**Response 400 (Error):**
```json
{
  "success": false,
  "data": null,
  "error": "Duplicated email is not allowed."
}
```

### Pagamentos

#### **POST** `/api/payment` - Realizar Transferência

**Request:**
```json
{
  "payer": 1,
  "payee": 2,
  "value": 100.00
}
```

**Response 200 (Success):**
```json
{
  "success": true,
  "error": null
}
```

**Response 400 (Error):**
```json
{
  "success": false,
  "error": "Insufficient balance."
}
```

## Regras de Negócio Implementadas

### Registro de Usuário

✅ **CPF/CNPJ único** - Não pode haver duplicação  
✅ **Email único** - Cada email pode ser usado apenas uma vez  
✅ **Senha hasheada** - Armazenamento seguro com ASP.NET Identity  
✅ **Carteira automática** - Cada usuário recebe uma carteira com saldo inicial de R$ 15,00  
✅ **Validação de campos** - Todos os campos obrigatórios são validados  

### Processamento de Pagamento

✅ **Apenas Clientes podem enviar** - Lojistas só podem receber  
✅ **Validação de saldo** - Verifica saldo suficiente antes da transação  
✅ **Autorização externa** - Consulta serviço externo antes de processar  
✅ **Transações ACID** - Rollback automático em caso de erro  
✅ **Notificações assíncronas** - Enviadas após conclusão (não bloqueiam a transação)  
✅ **Proteção contra auto-transferência** - Usuário não pode transferir para si mesmo  

##  Destaques Técnicos

###  Segurança
- Hash de senhas com **ASP.NET Identity PasswordHasher**
- Validação rigorosa de entrada de dados
- Proteção contra duplicação de registros
- Tratamento seguro de exceptions

### Confiabilidade
- Tratamento robusto de exceções em todas as camadas
- Logging estruturado de operações críticas
- Transações ACID no banco de dados
- Unit of Work pattern para garantir consistência
- Health checks nos containers Docker

### Código Limpo
- Separação clara de responsabilidades (Clean Architecture)
- Injeção de dependências em toda a aplicação
- Código testável e manutenível
- Validações fail-fast no domínio
- Entidades ricas com comportamento encapsulado

### DevOps
- Containerização completa com Docker
- Multi-stage build otimizado
- Migrations automáticas na inicialização
- Configuração via variáveis de ambiente
- Volumes persistentes para dados

## Aprendizados e Evolução

Este projeto faz parte da minha jornada de aprendizado em arquitetura de software e boas práticas de desenvolvimento backend, áreas com as quais busco estar constantemente alinhado. Tenho plena consciência de que a solução apresentada não é perfeita e que existem pontos que podem e devem ser aprimorados. Ainda assim, o projeto reflete de forma fiel meu nível atual de conhecimento, minhas decisões técnicas conscientes e minha preocupação com organização, clareza e evolução do código.

Durante o desenvolvimento, priorizei aplicar conceitos que venho estudando e consolidando, ciente de que o aprendizado em engenharia de software é contínuo. Como próximo passo, planejo evoluir esta solução com a inclusão de testes unitários, prática que venho estudando e pretendo aplicar em versões futuras dos meus projetos.

Conhecimentos aprofundados:

- **Clean Architecture** e separação de camadas
- **Domain-Driven Design (DDD)** com entidades ricas
- **Princípios SOLID** aplicados de forma prática
- **Padrões de projeto** (Repository, Unit of Work, Policy)
- **Entity Framework Core** e gerenciamento de transações
- **Containerização** com Docker e Docker Compose


##  Licença

Este projeto foi desenvolvido para fins educacionais e de portfólio. Sinta-se livre para explorar o código!

##  Autor

**Paulo Anjos**

- GitHub: [@PauloAnjos22](https://github.com/PauloAnjos22)
- LinkedIn: [Paulo Anjos](https://www.linkedin.com/in/paulo-anjos-33a882200/) 

## Agradecimentos

- [PicPay](https://github.com/PicPay) pelo desafio técnico inspirador
- Comunidade .NET pelas excelentes ferramentas e documentação





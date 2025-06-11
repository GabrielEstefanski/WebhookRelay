# Webhook Relay Service

## Descrição

Este projeto é um serviço backend para receber webhooks de APIs externas e processá-los de forma assíncrona, resiliente e escalável. Utiliza Hangfire para execução de jobs em background, com retry automático e Dead Letter Queue (DLQ) para tratamento de falhas. A arquitetura segue princípios de Clean Architecture para fácil manutenção e testes.

---

## Funcionalidades Principais

- Recepção de webhooks via API REST.
- Processamento assíncrono dos eventos com Hangfire.
- Retry automático configurado para falhas temporárias.
- Dead Letter Queue para persistir payloads que falharam após retries.
- Logging estruturado com Serilog.
- Dashboard Hangfire para monitorar jobs em execução e filas.
- Docker Compose para ambiente de desenvolvimento com SQL Server e Hangfire.

---

## Estrutura do Projeto

- **WebhookRelay.Domain**: Entidades, exceções e contratos (interfaces).
- **WebhookRelay.Application**: Serviços de aplicação, lógica de negócios, DTOs, Jobs.
- **WebhookRelay.Infrastructure**: Persistência, integração com Hangfire, DLQ, logging.
- **WebhookRelay.Web**: API REST, middleware (ex: tratamento global de exceções).
- **WebhookRelay.Tests**: Testes unitários e de integração.

---

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- Editor de código (ex: VS Code, Visual Studio)

---

## Como rodar localmente

### 1. Clone o repositório

```bash
git clone https://github.com/GabrielEstefanski/WebhookRelay.git
cd seu-repositorio

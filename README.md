# Controle de Gastos Residencial

Aplicação Full Stack para controle de receitas e despesas de pessoas de uma residência. O projeto é composto por uma API desenvolvida em ASP.NET Core com Entity Framework Core e SQLite, além de um frontend em React com TypeScript.

## Funcionalidades

- Cadastro de pessoas com nome e idade.
- Cadastro de receitas e despesas vinculadas a uma pessoa.
- Validação para impedir idade negativa, ID inválido e valores menores ou iguais a zero.
- Regra de negócio: menores de 18 anos podem cadastrar apenas despesas.
- Resumo geral com total de receitas, despesas e saldo.
- Resumo individual por pessoa.
- Exclusão de pessoa com remoção automática das transações vinculadas.

## Tecnologias

- ASP.NET Core
- Entity Framework Core
- SQLite
- React
- TypeScript
- Vite
- Axios

## Estrutura

```text
backend/
  ControleGastos.API/
    Controllers/
    Data/
    DTOs/
    Entities/
    Repositories/
    Services/
frontend/
  src/
    api/
    components/
    pages/
```

## Como executar

### Backend

```bash
cd backend/ControleGastos.API
dotnet restore
dotnet ef database update
dotnet run
```

A API utiliza SQLite e a connection string está configurada no arquivo `appsettings.json`.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

O frontend está configurado para consumir a API local. Caso a porta do backend seja alterada, ajuste a URL base em `frontend/src/api/api.ts`.

## Informações

O projeto utiliza um `.gitignore` para evitar o versionamento de arquivos temporários, dependências, builds e configurações específicas do ambiente de desenvolvimento.

## Status

Projeto desenvolvido como desafio técnico e utilizado para estudo de desenvolvimento Full Stack, integração entre frontend e backend e persistência de dados com Entity Framework Core.

# Controle de Gastos Residencial

Aplicacao para controlar receitas e despesas de pessoas de uma residencia. O projeto possui uma API em ASP.NET Core com SQLite e um frontend em React com TypeScript.

## Funcionalidades

- Cadastro de pessoas com nome e idade.
- Cadastro de receitas e despesas vinculadas a uma pessoa.
- Validacao para impedir idade negativa, ID invalido e valores menores ou iguais a zero.
- Regra de negocio: menores de 18 anos podem cadastrar apenas despesas.
- Resumo geral com total de receitas, despesas e saldo.
- Resumo individual por pessoa.
- Exclusao de pessoa com remocao das transacoes vinculadas.

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

Por padrao, a API usa SQLite com a connection string em `appsettings.json`.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

O frontend foi configurado para chamar a API local. Se a porta do backend mudar, ajuste a base da API em `frontend/src/api/api.ts`.

## O que nao subir para o GitHub

Os arquivos abaixo ficam fora do repositorio pelo `.gitignore`:

- `node_modules/`
- `dist/`
- `bin/`
- `obj/`
- `*.db`
- arquivos de log
- configuracoes locais do editor
- pastas internas `.agents/` e `.codex/`

## Status

Projeto criado para estudo de backend, frontend e integracao com banco de dados.

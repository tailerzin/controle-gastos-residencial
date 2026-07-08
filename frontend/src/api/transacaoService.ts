import api from "./api";

export interface CreateTransacaoDto {
    descricao: string;
    valor: number;
    tipo: number;
    pessoaId: number;
}

export async function cadastrarTransacao(dados: CreateTransacaoDto) {
    // Cadastra uma receita ou despesa vinculada a uma pessoa.
    const response = await api.post("/Transacoes", dados);

    return response.data;
}

import api from "./api";
import type {
    ConsultaTotaisDto,
    CreatePessoaDto,
    PessoaResponseDto
} from "../types/pessoa";

export async function obterTotais(): Promise<ConsultaTotaisDto> {
    // Consulta o endpoint que retorna receitas, despesas e saldos.
    const response = await api.get<ConsultaTotaisDto>("/Pessoas/totais");
    return response.data;
}

export async function cadastrarPessoa(
    dados: CreatePessoaDto
): Promise<PessoaResponseDto> {
    // Envia uma nova pessoa para cadastro no backend.
    const response = await api.post<PessoaResponseDto>(
        "/Pessoas",
        dados
    );

    return response.data;
}

export async function excluirPessoa(id: number): Promise<void> {
    // Remove a pessoa pelo id informado.
    await api.delete(`/Pessoas/${id}`);
}

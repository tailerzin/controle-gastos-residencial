export interface CreatePessoaDto {
    nome: string;
    idade: number;
}

export interface PessoaResponseDto {
    id: number;
    nome: string;
    idade: number;
}

export interface PessoaResumoDto {
    id: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface ConsultaTotaisDto {
    pessoas: PessoaResumoDto[];
    totalReceitas: number;
    totalDespesas: number;
    saldoGeral: number;
}
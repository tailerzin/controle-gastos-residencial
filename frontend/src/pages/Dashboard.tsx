import { useEffect, useState } from "react";
import type { ConsultaTotaisDto } from "../types/pessoa";
import { obterTotais, excluirPessoa } from "../api/pessoaService";
import PessoaForm from "../components/PessoaForm";
import TransacaoForm from "../components/TransacaoForm";

export default function Dashboard() {
    const [dados, setDados] = useState<ConsultaTotaisDto | null>(null);

    async function carregarDados() {
        try {
            // Busca o resumo financeiro atualizado no backend.
            const resultado = await obterTotais();

            setDados(resultado);
        } catch (erro) {
            console.error("Erro ao buscar totais:", erro);
        }
    }

    async function handleExcluir(id: number) {
        const confirmar = window.confirm(
            "Deseja realmente excluir esta pessoa?"
        );

        if (!confirmar) return;

        try {
            // Depois de excluir, recarrega os totais para atualizar a tela.
            await excluirPessoa(id);
            await carregarDados();
        } catch (erro) {
            console.error(erro);
            alert("Erro ao excluir pessoa.");
        }
    }

    useEffect(() => {
        // Carrega os dados uma vez quando a pagina abre.
        carregarDados();
    }, []);

    if (!dados) {
        return <p className="loading">Carregando...</p>;
    }

    return (
        <main className="dashboard">
            <header className="dashboard-header">
                <p>Finanças residenciais</p>
                <h1>Controle de Gastos</h1>
            </header>

            <section className="forms-grid" aria-label="Cadastros">
                <PessoaForm onPessoaCadastrada={carregarDados} />
                <TransacaoForm onTransacaoCadastrada={carregarDados} />
            </section>

            <section className="summary-section">
                <h2>Resumo Geral</h2>

                <div className="summary-grid">
                    <div className="summary-item income">
                        <span>Receitas</span>
                        <strong>R$ {dados.totalReceitas}</strong>
                    </div>

                    <div className="summary-item expense">
                        <span>Despesas</span>
                        <strong>R$ {dados.totalDespesas}</strong>
                    </div>

                    <div className="summary-item balance">
                        <span>Saldo</span>
                        <strong>R$ {dados.saldoGeral}</strong>
                    </div>
                </div>
            </section>

            <section className="people-section">
                <h2>Pessoas</h2>

                <div className="people-list">
                    {dados.pessoas.map((pessoa) => (
                        <article className="person-card" key={pessoa.id}>
                            <div>
                                <h3>{pessoa.nome}</h3>
                                <p>ID {pessoa.id}</p>
                            </div>

                            <dl>
                                <div>
                                    <dt>Receita</dt>
                                    <dd>R$ {pessoa.totalReceitas}</dd>
                                </div>

                                <div>
                                    <dt>Despesa</dt>
                                    <dd>R$ {pessoa.totalDespesas}</dd>
                                </div>

                                <div>
                                    <dt>Saldo</dt>
                                    <dd>R$ {pessoa.saldo}</dd>
                                </div>
                            </dl>

                            <button
                                className="delete-button"
                                onClick={() => handleExcluir(pessoa.id)}
                            >
                                Excluir
                            </button>
                        </article>
                    ))}
                </div>
            </section>
        </main>
    );
}

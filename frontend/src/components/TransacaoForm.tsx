import { useState } from "react";
import { cadastrarTransacao } from "../api/transacaoService";

interface TransacaoFormProps {
  onTransacaoCadastrada: () => void;
}

export default function TransacaoForm({
  onTransacaoCadastrada,
}: TransacaoFormProps) {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState(0);
  const [pessoaId, setPessoaId] = useState("");
  const [erro, setErro] = useState("");

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    const valorNumero = Number(valor);
    const pessoaIdNumero = Number(pessoaId);

    if (pessoaId === "" || Number.isNaN(pessoaIdNumero) || pessoaIdNumero <= 0) {
      setErro("Informe um ID de pessoa válido. O ID deve ser maior que zero.");
      return;
    }

    if (valor === "" || Number.isNaN(valorNumero) || valorNumero <= 0) {
      setErro("Informe um valor válido. O valor deve ser maior que zero.");
      return;
    }

    try {
      setErro("");

      // Envia a receita ou despesa para ser validada e salva no backend.
      await cadastrarTransacao({
        descricao,
        valor: valorNumero,
        tipo,
        pessoaId: pessoaIdNumero,
      });

      // Limpa o formulario e atualiza os totais da tela principal.
      setDescricao("");
      setValor("");
      setTipo(0);
      setPessoaId("");

      onTransacaoCadastrada();
    } catch (error) {
      console.error(error);
      setErro("Erro ao cadastrar transação.");
    }
  }

  return (
    <form className="form-card" onSubmit={handleSubmit} noValidate>
      <h2>Nova Transação</h2>

      <label className="field-group">
        <span>ID da pessoa</span>
        <input
          type="number"
          min="1"
          step="1"
          placeholder="Ex: 1"
          value={pessoaId}
          aria-invalid={Boolean(erro)}
          onChange={(e) => setPessoaId(e.target.value)}
        />
        <small>Use o ID exibido no cartão da pessoa.</small>
      </label>

      <label className="field-group">
        <span>Descrição</span>
        <input
          type="text"
          placeholder="Ex: Mercado"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        />
      </label>

      <label className="field-group">
        <span>Valor da receita ou despesa</span>
        <input
          type="number"
          min="0.01"
          step="0.01"
          placeholder="Ex: 120.50"
          value={valor}
          aria-invalid={Boolean(erro)}
          onChange={(e) => setValor(e.target.value)}
        />
        <small>Digite apenas valores positivos.</small>
      </label>

      <label className="field-group">
        <span>Tipo</span>
        <select
          value={tipo}
          onChange={(e) => setTipo(Number(e.target.value))}
        >
          <option value={0}>Receita</option>
          <option value={1}>Despesa</option>
        </select>
      </label>

      {erro && <p className="form-error">{erro}</p>}

      <button type="submit">Salvar</button>
    </form>
  );
}

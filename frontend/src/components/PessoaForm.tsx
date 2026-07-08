import { useState } from "react";
import { cadastrarPessoa } from "../api/pessoaService";

interface PessoaFormProps {
  onPessoaCadastrada: () => void;
}

export default function PessoaForm({ onPessoaCadastrada }: PessoaFormProps) {
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [erro, setErro] = useState("");

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    const idadeNumero = Number(idade);

    if (idade === "" || Number.isNaN(idadeNumero) || idadeNumero < 0) {
      setErro("Informe uma idade válida. A idade não pode ser negativa.");
      return;
    }

    try {
      setErro("");

      // Envia os dados digitados para criar uma nova pessoa na API.
      await cadastrarPessoa({
        nome,
        idade: idadeNumero,
      });

      // Limpa o formulario e avisa a tela principal para recarregar o resumo.
      setNome("");
      setIdade("");

      onPessoaCadastrada();
    } catch (error) {
      console.error(error);
      setErro("Erro ao cadastrar pessoa.");
    }
  }

  return (
    <form className="form-card" onSubmit={handleSubmit} noValidate>
      <h2>Nova Pessoa</h2>

      <label className="field-group">
        <span>Nome da pessoa</span>
        <input
          type="text"
          placeholder="Ex: Maria"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
      </label>

      <label className="field-group">
        <span>Idade</span>
        <input
          type="number"
          min="0"
          placeholder="Ex: 25"
          value={idade}
          aria-invalid={Boolean(erro)}
          onChange={(e) => setIdade(e.target.value)}
        />
        <small>A idade deve ser zero ou maior.</small>
      </label>

      {erro && <p className="form-error">{erro}</p>}

      <button type="submit">
        Salvar
      </button>
    </form>
  );
}

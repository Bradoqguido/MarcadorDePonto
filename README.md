# MarcadorDePonto

## **Como funciona?**

Atualmente é possivel clicar em "Marcar ponto" para digitar a hora (exatamente como no exemplo mostrado).

Ao clicar em salvar a aplicação vai salvar um json com os dados preenchidos.

Sempre que a aplicação for aberta, ela irá verificar se existem registros do mesmo dia, se tiver ela irá salvar os novos pontos, junto aos existentes.

## **User Story**

- [ ]  A aplicação deve ter interface gráfica.
- [ ]  Deve possuir botões para sair da aplicação, salvar os dados, inserir dados.
- [ ]  A aplicação deve possibilitar salvar várias horas em um mesmo dia.
- [ ]  Deve ser possível salvar os dados preenchidos em um json, para leitura prévia.
- [ ]  Deve ser possível buscar no json o indíce que contem os dados de hoje.
- [ ]  Deve ser possível adicionar novos registros junto aos salvos hoje, a qualquer momento do dia.
- [ ]  Verificar se a hora digitada pelo usuário é compatível com o formato Hh:Mm.
- [ ]  A aplicação deve ter uma lista com os caracteres aceitos.
- [ ]  A aplicação deve exibir uma janela possibilitando o usuário inserir dados.
- [ ]  O usuário pode cancelar a ação de inserir uma hora.
- [ ]  Deve ser possível ler os dados preenchidos em um json.
- [ ]  Precisa ter uma função para somar as horas extras.
- [ ]  Exibir as horas extras do dia.
- [ ]  O usuário pode zerar as horas do dia.
- [ ]  Ao tentar salvar os registros do dia, deve ser possível cancelar a operação (caso não tenham registros).
- [ ]  Nos registros de hora, só podem ser aceitos os caracteres que compõem alguma hora, Ex: 14:23/00:34/23:00.
- [ ]  Só pode registrar novos horários se forem maiores que o anterior.
- [ ]  Não podem existir horas negativas.
- [ ]  O usuário não pode fazer mais do que 1:11h de hora extra por dia, exibir mensagem de erro.
- [ ]  O usuário não pode fazer mais do que 6h seguidas de trabalho.
- [ ]  O usuário não pode fazer mais do que 90 minutos/ 1:30h de almoço.
- [ ]  O usuário não pode fazer menos que 60 minutos/ 1h de almoço.
- [ ]  Não pode mostrar minutos negativos no relatório, apenas o horário no geral seja negativo ou não.

## Sequência de funções implementadas

Classe Controller:

- private void ReadDadosDoJson();
- public void ShowInputBox(string pTituloText, string pDescricaoText);
- private void ShowInfoMessageBox(string pTituloText, string pDescricaoText);
- private void ShowWarnMessageBox(string pText, string pTitle);
- public salvarDadosEmJson();
- public void ExibirRelatorioDoDia();
- private StringBuilder MontaHorasExtrasRelatorio(StringBuilder pStbConteudoMsg)
- private StringBuilder MontaAlmocoDoRelatorio(StringBuilder pStbConteudoMsg)
- private string MontarStrComHorariosEntradaEsaida(List<string> pArr);
- public void SelecionarAlmoco();
- private void ValidacoesDeEntrada(string pStrAux)

Classe TimeController:

- public TimeController(List<Ponto> pLstRegistrosPonto);
- public Ponto GetRegistroDePontoAtual();
- private void FindRegistroPontoDeHoje();
- private void VerificaSeExisteADataDeHoje();
- private int FindIndexRegistroPonto();
- public bool ValidarNovoRegistroDeHorario(string pHorario)
- private bool VerificarCaracteresAceitos(string pHorario);
- private bool VerificarFormatoDoHorario(string pHorario);
- public bool InserirRegistro(string pHorario);
- private int SubtrairHoras(string pHoraInicio, string pHoraFim);
- private void SomarHorasDoDia();
- private void AjusteFinoDeHorario(int pIntSomaMinutosExtras);
- private int EncontraHorarioAlmoco();
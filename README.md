# MarcadorDePonto

## **Como funciona?**

Atualmente � possivel clicar em "Marcar ponto" para digitar a hora (exatamente como no exemplo mostrado).

Ao clicar em salvar a aplica��o vai salvar um json com os dados preenchidos.

Sempre que a aplica��o for aberta, ela ir� verificar se existem registros do mesmo dia, se tiver ela ir� salvar os novos pontos, junto aos existentes.

## **User Story**

- [ ]  A aplica��o deve ter interface gr�fica.
- [ ]  Deve possuir bot�es para sair da aplica��o, salvar os dados, inserir dados.
- [ ]  A aplica��o deve possibilitar salvar v�rias horas em um mesmo dia.
- [ ]  Deve ser poss�vel salvar os dados preenchidos em um json, para leitura pr�via.
- [ ]  Deve ser poss�vel buscar no json o ind�ce que contem os dados de hoje.
- [ ]  Deve ser poss�vel adicionar novos registros junto aos salvos hoje, a qualquer momento do dia.
- [ ]  Verificar se a hora digitada pelo usu�rio � compat�vel com o formato Hh:Mm.
- [ ]  A aplica��o deve ter uma lista com os caracteres aceitos.
- [ ]  A aplica��o deve exibir uma janela possibilitando o usu�rio inserir dados.
- [ ]  O usu�rio pode cancelar a a��o de inserir uma hora.
- [ ]  Deve ser poss�vel ler os dados preenchidos em um json.
- [ ]  Precisa ter uma fun��o para somar as horas extras.
- [ ]  Exibir as horas extras do dia.
- [ ]  O usu�rio pode zerar as horas do dia.
- [ ]  Ao tentar salvar os registros do dia, deve ser poss�vel cancelar a opera��o (caso n�o tenham registros).
- [ ]  Nos registros de hora, s� podem ser aceitos os caracteres que comp�em alguma hora, Ex: 14:23/00:34/23:00.
- [ ]  S� pode registrar novos hor�rios se forem maiores que o anterior.
- [ ]  N�o podem existir horas negativas.
- [ ]  O usu�rio n�o pode fazer mais do que 1:11h de hora extra por dia, exibir mensagem de erro.
- [ ]  O usu�rio n�o pode fazer mais do que 6h seguidas de trabalho.
- [ ]  O usu�rio n�o pode fazer mais do que 90 minutos/ 1:30h de almo�o.
- [ ]  O usu�rio n�o pode fazer menos que 60 minutos/ 1h de almo�o.
- [ ]  N�o pode mostrar minutos negativos no relat�rio, apenas o hor�rio no geral seja negativo ou n�o.

## Sequ�ncia de fun��es implementadas

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
# MarcadorDePonto

## **Como funciona?**

Atualmente � possivel clicar em "Marcar ponto" para digitar a hora (exatamente como no exemplo mostrado).

Ao clicar em salvar a aplica��o vai salvar um json com os dados preenchidos.

Sempre que a aplica��o for aberta, ela ir� verificar se existem registros do mesmo dia, se tiver ela ir� salvar os novos pontos, junto aos existentes.

## **User Story**

- [x]  A aplica��o deve ter interface gr�fica.
- [x]  Deve possuir bot�es para sair da aplica��o, salvar os dados, inserir dados.
- [x]  A aplica��o deve possibilitar salvar v�rias horas em um mesmo dia.
- [x]  Deve ser poss�vel salvar os dados preenchidos em um json, para leitura pr�via.
- [x]  Deve ser poss�vel buscar no json o ind�ce que contem os dados de hoje.
- [x]  Deve ser poss�vel adicionar novos registros junto aos salvos hoje, a qualquer momento do dia.
- [x]  Verificar se a hora digitada pelo usu�rio � compat�vel com o formato Hh:Mm.
- [x]  A aplica��o deve ter uma lista com os caracteres aceitos.
- [x]  A aplica��o deve exibir uma janela possibilitando o usu�rio inserir dados.
- [x]  O usu�rio pode cancelar a a��o de inserir uma hora.
- [x]  Deve ser poss�vel ler os dados preenchidos em um json.
- [x]  Precisa ter uma fun��o para somar as horas extras.
- [x]  Exibir as horas extras do dia.
- [x]  O usu�rio pode zerar as horas do dia.
- [x]  Ao tentar salvar os registros do dia, deve ser poss�vel cancelar a opera��o (caso n�o tenham registros).
- [x]  Nos registros de hora, s� podem ser aceitos os caracteres que comp�em alguma hora, Ex: 14:23/00:34/23:00.
- [x]  S� pode registrar novos hor�rios se forem maiores que o anterior.
- [x]  N�o podem existir horas negativas.
- [x]  O usu�rio n�o pode fazer mais do que 1:11h de hora extra por dia, exibir mensagem de erro.
- [x]  O usu�rio n�o pode fazer mais do que 6h seguidas de trabalho.
- [x]  O usu�rio n�o pode fazer mais do que 90 minutos/ 1:30h de almo�o.
- [x]  O usu�rio n�o pode fazer menos que 60 minutos/ 1h de almo�o.
- [x]  N�o pode mostrar minutos negativos no relat�rio, apenas o hor�rio no geral seja negativo ou n�o.
- [x]  A aplica��o deve ter atalhos r�pidos.
- [x]  A aplica��o precisa ter �cones.
- [x]  Criar um relat�rio mensal.
- [x]  O usu�rio pode apagar todos os registros inseridos.
- [x]  Mostrar o relat�rio di�rio sempre que o usu�rio inserir um valor.
- [x]  Mostrar pontos marcados (lista de horas) sempre que o usu�rio inserir um registro.
- [x]  Mostrar mensagem com o saldo total de horas.
- [ ]  Mostrar que horas o usu�rio poderia sair.

## Sequ�ncia de fun��es implementadas

Classe Controller:

- private void ReadDadosDoJson();
- public bool InserirHorario(string pStrInput);
- public bool ValidacoesDeEntrada(string pStrAux);
- public void SalvarDadosEmJson();
- public string ExibirRelatorioDoDia();
- public StringBuilder MontaHorasExtrasRelatorio(StringBuilder pStbConteudoMsg, Ponto pPonto);
- private StringBuilder MontaAlmocoDoRelatorio(StringBuilder pStbConteudoMsg, Ponto pPonto);
- public string MontarStrComHorariosEntradaEsaida(List<string> pArr);
- public bool InvocarZerarRegistrosDia(string pStrInput);
- public bool ApagarTodosOsRegistros();
- public bool SelecionarAlmoco(string pStrInicioAlmoco, string pStrFimAlmoco);
- public List<string> GetHorarios();
- public string ExibirRelatorioMensal();
- public string GetSaldoTotal();

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
- public bool ZerarRegistrosDia();
# MarcadorDePonto

## **Como funciona?**

Atualmente é possivel clicar em "Marcar ponto" para digitar a hora (exatamente como no exemplo mostrado).

Ao clicar em salvar a aplicação vai salvar um json com os dados preenchidos.

Sempre que a aplicação for aberta, ela irá verificar se existem registros do mesmo dia, se tiver ela irá salvar os novos pontos, junto aos existentes.

## **User Story**

- [x]  A aplicação deve ter interface gráfica.
- [x]  Deve possuir botões para sair da aplicação, salvar os dados, inserir dados.
- [x]  A aplicação deve possibilitar salvar várias horas em um mesmo dia.
- [x]  Deve ser possível salvar os dados preenchidos em um json, para leitura prévia.
- [x]  Deve ser possível buscar no json o indíce que contem os dados de hoje.
- [x]  Deve ser possível adicionar novos registros junto aos salvos hoje, a qualquer momento do dia.
- [x]  Verificar se a hora digitada pelo usuário é compatível com o formato Hh:Mm.
- [x]  A aplicação deve ter uma lista com os caracteres aceitos.
- [x]  A aplicação deve exibir uma janela possibilitando o usuário inserir dados.
- [x]  O usuário pode cancelar a ação de inserir uma hora.
- [x]  Deve ser possível ler os dados preenchidos em um json.
- [x]  Precisa ter uma função para somar as horas extras.
- [x]  Exibir as horas extras do dia.
- [x]  O usuário pode zerar as horas do dia.
- [x]  Ao tentar salvar os registros do dia, deve ser possível cancelar a operação (caso não tenham registros).
- [x]  Nos registros de hora, só podem ser aceitos os caracteres que compõem alguma hora, Ex: 14:23/00:34/23:00.
- [x]  Só pode registrar novos horários se forem maiores que o anterior.
- [x]  Não podem existir horas negativas.
- [x]  O usuário não pode fazer mais do que 1:11h de hora extra por dia, exibir mensagem de erro.
- [x]  O usuário não pode fazer mais do que 6h seguidas de trabalho.
- [x]  O usuário não pode fazer mais do que 90 minutos/ 1:30h de almoço.
- [x]  O usuário não pode fazer menos que 60 minutos/ 1h de almoço.
- [x]  Não pode mostrar minutos negativos no relatório, apenas o horário no geral seja negativo ou não.
- [x]  A aplicação deve ter atalhos rápidos.
- [x]  A aplicação precisa ter ícones.
- [x]  Criar um relatório mensal.
- [x]  O usuário pode apagar todos os registros inseridos.
- [x]  Mostrar o relatório diário sempre que o usuário inserir um valor.
- [x]  Mostrar pontos marcados (lista de horas) sempre que o usuário inserir um registro.
- [x]  Mostrar mensagem com o saldo total de horas.
- [x]  Mostrar que horas o usuário poderia sair.

## Sequência de funções implementadas

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
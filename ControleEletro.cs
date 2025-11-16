using System;
namespace  ListaDeOpercaoDeEletroDomesticos
{
    class ControleEletro
    {
       
        static bool BuscarPeloNome(List<eletro> listaEletro, string nomeBusca)
        {
            foreach (eletro e in listaEletro)
            {
                if (e.nome.ToUpper().Contains(nomeBusca.ToUpper()))
                {
                    Console.WriteLine("**** Buscar Eletrodomesticos ****");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potencia (kW): {e.potencia}");
                    Console.WriteLine($"Tempo medio de uso diario (horas): {e.tempomedio}");
                   
                    return true;
                }
            }
            return false;
        }
        static void addeletro(List<eletro> listaEletro)
        {
            eletro NovoEletro = new eletro();
            Console.Write("Nome do eletrodomestico:");
            NovoEletro.nome = Console.ReadLine();
            Console.Write("Potencia (em kW):");
            NovoEletro.potencia = double.Parse(Console.ReadLine());
            Console.Write("Tempo medio de uso diario (em horas):");
            NovoEletro.tempomedio = double.Parse(Console.ReadLine());
            listaEletro.Add(NovoEletro);
            Console.WriteLine("--------");
        }        
        static void carregarDados(List<eletro> listaEletro, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(',');
                    eletro e = new eletro
                    {
                        nome = dados[0],
                        potencia = double.Parse(dados[1]),
                        tempomedio = double.Parse(dados[2])
                    };
                    listaEletro.Add(e);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
        }
        static void salvarDados(List<eletro> listaEletro, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (eletro e in listaEletro)
                {
                    writer.WriteLine($"{e.nome},{e.potencia},{e.tempomedio}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }
        static int menu()
        {
            Console.WriteLine("Menu de opcoes:");
            Console.WriteLine("1 - Adicionar eletrodomestico");
            Console.WriteLine("2 - Buscar eletrodomestico pelo nome");
            Console.WriteLine("3 - Mostrar Eletrosdomesticos que utrapassam (x valor)");
            Console.WriteLine("4 - Calculo de consumo diario e mensal do eletrodomestico");
            Console.WriteLine("5 - Mostrar todos os eletrodomesticos");
            Console.WriteLine("0 - Sair e salvar dados");

            Console.Write("Escolha uma opcao: ");
            int opcao = int.Parse(Console.ReadLine());
            return opcao;
        }

        static void mostrarEletrosQueUltrapassam(List<eletro> listaEletro, double valorReferencia)
        {
            Console.WriteLine($"Eletrodomesticos que ultrapassam {valorReferencia} kW:");
            foreach (eletro e in listaEletro)
            {
                double consumoDiario = e.potencia * e.tempomedio;
                if (consumoDiario > valorReferencia)
                {
                    Console.WriteLine($"Nome: {e.nome}, Consumo Diario: {consumoDiario} kW");
                }
            }
        }
        static void CalculoDeConsumoDarioeMensal(List<eletro> listaeletro, double ValoresFornecidos)
        {
            foreach (eletro e in listaeletro)
            {
                Console.WriteLine($"Valor gasto em dinheiro do eletrodomestico {e.nome}");
                double ConsumoDiario = e.potencia * e.tempomedio * ValoresFornecidos;
                double consumoMensal = ConsumoDiario * 30;
                Console.WriteLine($"Consumo diario: {ConsumoDiario} R$");
                Console.WriteLine($"Consumo mensal: {consumoMensal} R$");
            }
        }
        static void MostrarDados(List<eletro> listaEletro)
        {
            int posicao = 1;
            foreach (eletro e in listaEletro)
            {
                Console.WriteLine($"*** Eletrodomestico {posicao} ***");
                Console.WriteLine($"{e.nome} - {e.potencia} kW - {e.tempomedio} horas diarias");
                posicao++;
            }
        }
        
        static void Main()
        {
            List<eletro> ListaEletro = new List<eletro>();
            carregarDados(ListaEletro, "eletrodomesticos.txt"); 
            int opcao = 0;

            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        addeletro(ListaEletro);
                        break;
                    case 2:
                        Console.Write("Nome do eletrodomestico para buscar: ");
                        string nomeBusca = Console.ReadLine();
                        bool encontrado = BuscarPeloNome(ListaEletro, nomeBusca);
                        if (!encontrado)
                            Console.WriteLine("Eletrodoméstico não encontrado.");
                        break;
                    case 3:
                        Console.Write("Valor de referencia (em kW): ");
                        double valorReferencia = double.Parse(Console.ReadLine());
                        mostrarEletrosQueUltrapassam(ListaEletro, valorReferencia);
                        break;
                    case 4:
                        Console.WriteLine("Qual o valor cobrado por KW/H?");
                        double ValoresFornecidos = double.Parse(Console.ReadLine());
                        CalculoDeConsumoDarioeMensal(ListaEletro, ValoresFornecidos);
                        break;
                    case 5:
                        MostrarDados(ListaEletro);
                        break;
                    
                    case 0:
                        salvarDados(ListaEletro, "eletrodomesticos.txt");
                        break;

                } 
            } while (opcao != 0);
        }
    }
}
using System;
namespace CadastroBandas
{
    class Program
    {
        static void addBanda(List<Banda> listaBandas)
        {
            Banda novaBanda = new Banda();
            Console.Write("Nome:");
            novaBanda.nome = Console.ReadLine();
            Console.Write("Genero:");
            novaBanda.genero = Console.ReadLine();
            Console.Write("Integrantes:");
            novaBanda.integrantes = int.Parse(Console.ReadLine());
            Console.Write("Ranking:");
            novaBanda.ranking = int.Parse(Console.ReadLine());
            listaBandas.Add(novaBanda);
            Console.WriteLine("--------");

        }

        static void mostrarBandas(List<Banda> listaBandas)
        {
            int posicao = 1;
            foreach (Banda b in listaBandas)
            {
                Console.WriteLine($"*** Banda {posicao}***");
                Console.WriteLine($"{b.nome} - {b.genero} - {b.integrantes} - {b.ranking}");
                posicao++;
            }

        }

        static bool buscarBanda(List<Banda> listBandas, string nomeBusca)
        {
            foreach (Banda b in listBandas)
            {
                if (b.nome.ToUpper().Contains(nomeBusca.ToUpper()))
                {
                    Console.WriteLine("**** Buscar Bandas ****");
                    Console.WriteLine($"Nome: {b.nome}");
                    Console.WriteLine($"Gênero: {b.genero}");
                    Console.WriteLine($"Integrantes: {b.integrantes}");
                    Console.WriteLine($"Ranking: {b.ranking}");
                    return true;
                }
            }
            return false;
        }

        static int buscarIndiceBanda(List<Banda> listBandas, string nomeBusca)
        {
            for (int i = 0; i < listBandas.Count; i++)
            {
                if (listBandas[i].nome.ToUpper().Equals(nomeBusca.ToUpper()))
                {
                    return i;
                }
            }
            return -1;
        }

        static bool atualizarBanda(List<Banda> listaBandas, string nomeBanda)
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda);
            if (i == -1)
                return false;
            Console.WriteLine("**** Dados da Banda ****");
            Console.WriteLine($"{listaBandas[i].nome} - {listaBandas[i].genero} - {listaBandas[i].integrantes} - {listaBandas[i].ranking}");
            Console.WriteLine("Nome: ");
            listaBandas[i].nome = Console.ReadLine();
            Console.WriteLine("Genero: ");
            listaBandas[i].genero = Console.ReadLine();
            Console.WriteLine("Integrantes: ");
            listaBandas[i].integrantes = int.Parse(Console.ReadLine());
            Console.WriteLine("Ranking: ");
            listaBandas[i].ranking = int.Parse(Console.ReadLine());
            return true;

        }
        
        static bool removerBanda(List<Banda> listaBandas, string nomeBanda)
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda);
            if (i == -1)
                return false;
            Console.WriteLine($"Tem certeza que deseja remover a banda {nomeBanda}? [1 - Sim] [2- Não]");
            int resposta = int.Parse(Console.ReadLine());
            if (resposta == 1) ;
                listaBandas.RemoveAt(i);
            return true;


        }
        static int menu()
        {
            int opcao;
            Console.WriteLine("*** Sistema de Cadastro de Bandas Rolling Stones***");
            Console.WriteLine("1- Adicionar Banda");
            Console.WriteLine("2- Mostrar Bandas");
            Console.WriteLine("3- Buscar Bandas");
            Console.WriteLine("4- Atualizar Banda");
            Console.WriteLine("5- Excluir Banda");
            Console.WriteLine("0- Sair do Sistema");
            opcao = int.Parse(Console.ReadLine());
            return opcao;
        }
         static void salvarDados(List<Banda> listaBandas, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Banda b in listaBandas)
                {
                    writer.WriteLine($"{b.nome},{b.genero},{b.integrantes},{b.ranking}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }

        static void carregarDados(List<Banda> listaBandas, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(',');
                    Banda novaBanda = new Banda();
                    novaBanda.nome = campos[0];
                    novaBanda.genero = campos[1];
                    novaBanda.integrantes = int.Parse(campos[2]);
                    novaBanda.ranking = int.Parse(campos[3]);
                    listaBandas.Add(novaBanda);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
                Console.WriteLine("Arquivo não encontrado :(");

        }
        
        static void Main()
        {
            List<Banda> listaBandas = new List<Banda>();
            int opcao = 0;
            carregarDados(listaBandas, "bandas.txt");
            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        addBanda(listaBandas);
                        break;
                    case 2:
                        mostrarBandas(listaBandas);
                        break;
                    case 3:
                        string nomeBanda = Console.ReadLine();
                        bool encontrado = buscarBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                        Console.WriteLine("Banda não encontrada. ");
                        break;
                    case 4: Console.Write("Nome da banda para atualizar os dados. ");
                        nomeBanda = Console.ReadLine();
                        encontrado = atualizarBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                        Console.WriteLine("Banda não encontrada. ");
                        break;
                    case 5: Console.Write("Nome da banda para excluir: ");
                        nomeBanda = Console.ReadLine();
                        encontrado = removerBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                        Console.WriteLine("Banda não encontrada. ");
                        break;
                  
                    case 0:
                        salvarDados(listaBandas, "bandas.txt");
                        Console.WriteLine("Até mais ;)");
                        break;
                }
                
                Console.ReadKey();
                Console.Clear(); 

            } while (opcao != 0);

        }

    }

}


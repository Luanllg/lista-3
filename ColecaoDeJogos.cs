using System;
using System.IO;
using System.Collections.Generic;
namespace CatalogoEControleDeJogos
{
    class ColecaoDeJogos
    {
        static void adicionarUmEmprestimo(List<emprestimo> listaemprestimos, List<jogos> listajogos
        )
        {
            emprestimo novoemprestimo = new emprestimo();
            Console.WriteLine("Data do emprestimo:");   
            novoemprestimo.data=Console.ReadLine();
            Console.WriteLine("Nome da pessoa:");
            novoemprestimo.NomePessoa=Console.ReadLine();
            Console.WriteLine("Titulo do jogo emprestado:");
            string titulodojogo=Console.ReadLine();
        }
        static void AdicionarJogo(List<jogos> listadejogos)
        {
            jogos novojogo = new jogos();
            Console.WriteLine("Titulo:");
            novojogo.titulo=Console.ReadLine();
            Console.WriteLine("Console:");
            novojogo.console=Console.ReadLine();
            Console.WriteLine("Ano:");
            novojogo.ano=int.Parse(Console.ReadLine());
            Console.WriteLine("Ranking:");
            novojogo.ranking=int.Parse(Console.ReadLine());
            listadejogos.Add(novojogo);


        }
                static void carregarDados(List<jogos> listajogos, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(',');
                    jogos j = new jogos
                    {
                        titulo = dados[0],
                         console=dados[1],
                        ano = int.Parse(dados[2]),
                        ranking= int.Parse(dados[3])
                    };
                    listajogos.Add(j);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
        }
        static void salvarDados(List<jogos> listajogos, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (jogos j in listajogos)
                {
                    writer.WriteLine($"{j.titulo},{j.console},{j.ano},{j.ranking}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }
        static void carregarDados(List<emprestimo> listaEmprestimos, List<jogos> listaJogos, string nomeArquivo)
{
    if (File.Exists(nomeArquivo))
    {
        string[] linhas = File.ReadAllLines(nomeArquivo);
        foreach (string linha in linhas)
        {
            string[] dados = linha.Split(',');

            string tituloJogo = dados[3];
            jogos jogoEncontrado = listaJogos.Find(x => x.titulo == tituloJogo);

            emprestimo e = new emprestimo
            {
                data = dados[0],
                NomePessoa = dados[1],
                jogoemprestado = jogoEncontrado
            
            };

            listaEmprestimos.Add(e);
        }

        Console.WriteLine("Dados de empréstimos carregados com sucesso!");
    }
}

        static void salvarDados(List<emprestimo> listaemprestimos, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (emprestimo e in listaemprestimos)
                {
                    writer.WriteLine($"{e.data},{e.NomePessoa},{e.jogoemprestado.titulo}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");
        }
        static int menu()
        {
          Console.WriteLine ("Menu de Opções:");
          Console.WriteLine ("1. adcionar um jogo e um emprestimo");
          Console.WriteLine ("2. procurar um jogo pelo titulo");
          Console.WriteLine ("3. listar jogos de um console");
          Console.WriteLine ("4. realizar emprestimo");
          Console.WriteLine ("5. devolver jogo");
          Console.WriteLine ("6. mostrar emprestimos");
          Console.WriteLine ("0. sair");
          int opcao = int.Parse(Console.ReadLine());
          return opcao;
        } 
        static bool ProcurarJogoPorTitulo(List<jogos> listajogos, string titulo)
        {
            foreach (jogos j in listajogos)
            {
                if (j.titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Jogo encontrado: {j.titulo}, Console: {j.console}, Ano: {j.ano}, Ranking: {j.ranking}");
                    return true;
                }
            }
            Console.WriteLine("Jogo não encontrado.");
            return false;
        }
        static void RealizarEmprestimo(List<emprestimo> listaemprestimos, List<jogos> listajogos)
        {
            Console.WriteLine("Insira o titulo do jogo para emprestar:");
            string titulodojogo = Console.ReadLine();
            jogos jogoParaEmprestar = null;

            foreach (jogos j in listajogos)
            {
                if (j.titulo.Equals(titulodojogo, StringComparison.OrdinalIgnoreCase))
                {
                    jogoParaEmprestar = j;
                    break;
                }
            }

            if (jogoParaEmprestar != null)
            {
                emprestimo novoemprestimo = new emprestimo();
                Console.WriteLine("Data do emprestimo:");
                novoemprestimo.data = Console.ReadLine();
                Console.WriteLine("Nome da pessoa:");
                novoemprestimo.NomePessoa = Console.ReadLine();
                novoemprestimo.jogoemprestado = jogoParaEmprestar;

                listaemprestimos.Add(novoemprestimo);
                Console.WriteLine("Empréstimo realizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Jogo não encontrado na coleção.");
            }
        }
        static void Devolverjogo(List<emprestimo> listaemprestimos, List<jogos> listajogos)
        {
            Console.WriteLine("Insira o titulo do jogo para devolver:");
            string titulodojogo = Console.ReadLine();
            emprestimo emprestimoParaDevolver = null;

            foreach (emprestimo e in listaemprestimos)
            {
                if (e.jogoemprestado.titulo.Equals(titulodojogo, StringComparison.OrdinalIgnoreCase))
                {
                    emprestimoParaDevolver = e;
                    break;
                }
            }

            if (emprestimoParaDevolver != null)
            {
                listaemprestimos.Remove(emprestimoParaDevolver);
                Console.WriteLine("Jogo devolvido com sucesso!");
            }
            else
            {
                Console.WriteLine("Empréstimo não encontrado para o jogo especificado.");
            }
        }
        static void MostraEmprestimos(List<emprestimo> listaemprestimos)
        {
            Console.WriteLine("Lista de Empréstimos:");
            foreach (emprestimo e in listaemprestimos)
            {
                Console.WriteLine($"Jogo: {e.jogoemprestado.titulo}, Emprestado para: {e.NomePessoa}, Data: {e.data}");
            }
        }

        
        static void Main()
        {
            List<jogos> listaDeJogos = new List<jogos>();
            List<emprestimo> listaDeEmprestimos = new List<emprestimo>();
            int opcao=0;
            carregarDados(listaDeJogos, "jogo.txt");
            carregarDados(listaDeEmprestimos, listaDeJogos, "emprestimos.txt");

            do
            {
                opcao=menu();
                switch (opcao)
                {
                    case 1: 
                       AdicionarJogo(listaDeJogos);
                     adicionarUmEmprestimo(listaDeEmprestimos,listaDeJogos);
                        break;

                    case 2:
                    Console.WriteLine("Insira o titulo:");
                    string NomeJogo = Console.ReadLine();
                    ProcurarJogoPorTitulo(listaDeJogos,NomeJogo);

                        break;
                    
                   case 4:
                    RealizarEmprestimo(listaDeEmprestimos,listaDeJogos);
                        break;
                    case 5: 
                     Devolverjogo(listaDeEmprestimos,listaDeJogos);
                        break;
                    case 6:
                    MostraEmprestimos(listaDeEmprestimos);
                        break;  
                    case 0:
                    salvarDados(listaDeJogos, "jogo.txt");
                    salvarDados(listaDeEmprestimos, "emprestimos.txt");
                        Console.WriteLine("Saindo...");
                        break;  
                }
            } while (opcao != 0);
        }
    }
}
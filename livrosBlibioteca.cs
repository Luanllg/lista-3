
using System;
namespace Biblioteca
{
    class livrosBlibioteca
    {
        static void CadastroLivros(List<RegistroLivros> NovoLivros)
        {
            RegistroLivros NovoLivro = new RegistroLivros();
            Console.WriteLine("Titulo:");
            NovoLivro.titulo = Console.ReadLine();
            Console.WriteLine("Autor");
            NovoLivro.autor = Console.ReadLine();
            Console.WriteLine("ano:");
            NovoLivro.ano = int.Parse(Console.ReadLine());
            Console.WriteLine("Prateleira:");
            NovoLivro.prateleira = int.Parse(Console.ReadLine());
            NovoLivros.Add(NovoLivro);
            Console.WriteLine("Registro concluido!");
        }// Cadastrar os livros
        static void salvarDados(List<RegistroLivros> listaLivros, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (RegistroLivros livro in listaLivros)
                {
                    writer.WriteLine($"{livro.titulo},{livro.autor},{livro.ano},{livro.prateleira}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }

        static bool buscarLivro(List<RegistroLivros> listaLivros, string tituloBusca)
        {
            foreach (RegistroLivros livro in listaLivros)
            {
                if (livro.titulo.ToUpper().Contains(tituloBusca.ToUpper()))
                {
                    Console.WriteLine("**** Buscar Livros ****");
                    Console.WriteLine($"Titulo: {livro.titulo}");
                    Console.WriteLine($"Autor: {livro.autor}");
                    Console.WriteLine($"Ano: {livro.ano}");  
                    Console.WriteLine($"Prateleira: {livro.prateleira}");

                    return true;
                }
            }
            return false;
        }
        static void mostrarLivros(List<RegistroLivros> listaLivros)
        {
            int posicao = 1;
            foreach (RegistroLivros livro in listaLivros)
            {
                Console.WriteLine($"*** Livro {posicao}***");
                Console.WriteLine($"{livro.titulo} - {livro.autor} - {livro.ano} - {livro.prateleira}");
                posicao++;
            }
        }
        static void mostrarBandas(List<RegistroLivros> listaLivros)
        {   
            int posicao = 1;
            foreach (RegistroLivros livro in listaLivros)
            {
                Console.WriteLine($"*** Livro {posicao}***");
                Console.WriteLine($"{livro.titulo} - {livro.autor} - {livro.ano} - {livro.prateleira}");
                posicao++;
            }

        }


        static int menu()
        {
            Console.WriteLine("1 para Cadastar um Livro:");
            Console.WriteLine("2 Buscar livro:");
            Console.WriteLine("3 Mostrar todos os livros:");
            Console.WriteLine("4 Mostrar todas as bandas:");
            Console.WriteLine("0 para sair:");
            int opcao = int.Parse(Console.ReadLine());
            return opcao;
        }//menu de registros
        //leia um ano e apresentte todos livrs mais novos que ele
        static void mostrarLivrosMaisNovos(List<RegistroLivros> listaLivros, int anoReferencia)
        {
            foreach (RegistroLivros livro in listaLivros)
            {
                if (livro.ano > anoReferencia)
                {
                    Console.WriteLine($"{livro.titulo} - {livro.autor} - {livro.ano} - {livro.prateleira}");
                }
            }
        }
        static void Main()
        {
            Console.WriteLine("Seja bem vindo a biblioteca");
            carregardados(ListaDeLivros, "Livros.txt");
            List<RegistroLivros> ListaDeLivros = new List<RegistroLivros>();
         

            int opcao = 0;
            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        CadastroLivros(ListaDeLivros);
                        break;
                    case 2:
                        Console.WriteLine("Digite o titulo do livro que deseja buscar:");
                        string tituloBusca = Console.ReadLine();
                        buscarLivro(ListaDeLivros, tituloBusca);
                        break;
                    case 3:
                        mostrarLivros(ListaDeLivros);
                        break;
                    case 4:
                        mostrarBandas(ListaDeLivros);
                        break;
                        case 5:
                        Console.WriteLine("Digite um ano de referencia:");
                        int anoReferencia = int.Parse(Console.ReadLine());
                        mostrarLivrosMaisNovos(ListaDeLivros, anoReferencia);
                        break;
                    case 0:
                    salvarDados(ListaDeLivros, "Livros.txt");
                        Console.WriteLine("Até mais!");
                        break;

                }
            } while (opcao != 0);
        }
    }
}

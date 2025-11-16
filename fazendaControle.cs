using System;
using System.Collections.Generic;
using System.IO;

namespace Fazenda
{
    class FazendaControle
    {
        // A) Adicionar fazenda
        static void adicionarfazenda(List<fazendinha> lista)
        {
            fazendinha novo = new fazendinha();

            Console.Write("Código: ");
            novo.codigo = int.Parse(Console.ReadLine());

            Console.Write("Litros de leite por semana: ");
            novo.litrosdeleiteporsemana = double.Parse(Console.ReadLine());

            Console.Write("Quantidade de alimentos (kg/semana): ");
            novo.quantidadedealimentos = double.Parse(Console.ReadLine());

            Console.Write("Mês de nascimento: ");
            int mes = int.Parse(Console.ReadLine());

            Console.Write("Ano de nascimento: ");
            int ano = int.Parse(Console.ReadLine());

            novo.denascimento = new denascimento { mes = mes, ano = ano };
            novo.abate = false;

            lista.Add(novo);
            Console.WriteLine("Registro adicionado!\n");
        }

        // A) Carregar do arquivo
        static void carregarDados(List<fazendinha> lista, string arquivo)
        {
            if (!File.Exists(arquivo)) return;

            string[] linhas = File.ReadAllLines(arquivo);

            foreach (string linha in linhas)
            {
                string[] d = linha.Split(',');

                    fazendinha f = new fazendinha();

                f.codigo = int.Parse(d[0]);
                f.litrosdeleiteporsemana = double.Parse(d[1]);
                f.quantidadedealimentos = double.Parse(d[2]);
                f.denascimento = new denascimento
                {
                    mes = int.Parse(d[3]),
                    ano = int.Parse(d[4])
                };

                lista.Add(f);
            }
            Console.WriteLine("Dados carregados!\n");
        }

        // B) Atualizar campo abate
        static void AtualizarAbate(List<fazendinha> lista)
        {
            int anoAtual = DateTime.Now.Year;

            foreach (var f in lista)
            {
                int idade = anoAtual - f.denascimento.ano;

                f.abate = (idade > 5) || (f.litrosdeleiteporsemana < 40);
            }
        }

        // F) Salvar arquivo
        static void salvarDados(List<fazendinha> lista, string arquivo)
        {
            using (StreamWriter w = new StreamWriter(arquivo))
            {
                foreach (var f in lista)
                {
                    w.WriteLine($"{f.codigo},{f.litrosdeleiteporsemana},{f.quantidadedealimentos},{f.denascimento.mes},{f.denascimento.ano}");
                }
            }
            Console.WriteLine("Dados salvos!\n");
        }

        // C) Soma dos litros de leite
        static double totalLeite(List<fazendinha> lista)
        {
            double total = 0;

            foreach (var f in lista)
                total += f.litrosdeleiteporsemana;

            return total;
        }

        // D) Soma dos alimentos
        static double totalAlimento(List<fazendinha> lista)
        {
            double total = 0;

            foreach (var f in lista)
                total += f.quantidadedealimentos;

            return total;
        }

        // E) Animais para abate
        static void AnimaisParaAbate(List<fazendinha> lista)
        {
            Console.WriteLine("\nAnimais que irão para o abate:");
            foreach (var f in lista)
            {
                if (f.abate)
                {
                    Console.WriteLine(
                        $"Código: {f.codigo} | Leite/semana: {f.litrosdeleiteporsemana} | Alimento: {f.quantidadedealimentos}"
                    );
                }
            }
            Console.WriteLine();
        }

        static int menu()
        {
            Console.WriteLine("\n--- MENU DA FAZENDA ---");
            Console.WriteLine("1 - Adicionar registro");
            Console.WriteLine("2 - Total de leite produzido/semana");
            Console.WriteLine("3 - Total de alimento consumido/semana");
            Console.WriteLine("4 - Atualizar campo ABATE");
            Console.WriteLine("5 - Listar animais para abate");
            Console.WriteLine("0 - Salvar e sair");
            Console.Write("Opção: ");
            return int.Parse(Console.ReadLine());
        }

        static void Main()
        {
            List<fazendinha> lista = new List<fazendinha>();

            carregarDados(lista, "fazenda.txt");
            AtualizarAbate(lista);

            int op;
            do
            {
                op = menu();

                switch (op)
                {
                    case 1:
                        adicionarfazenda(lista);
                        AtualizarAbate(lista);
                        break;

                    case 2:
                        Console.WriteLine("Total de leite/semana: " + totalLeite(lista));
                        break;

                    case 3:
                        Console.WriteLine("Total de alimento/semana: " + totalAlimento(lista));
                        break;

                    case 4:
                        AtualizarAbate(lista);
                        Console.WriteLine("Campo ABATE atualizado!\n");
                        break;

                    case 5:
                        AnimaisParaAbate(lista);
                        break;

                    case 0:
                        salvarDados(lista, "fazenda.txt");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

            } while (op != 0);
        }
    }
}

using System;

namespace Cadastroeletro
{


    public class Exer3
    

    {

        static void cadastronovo(List<Eletro> lista)
        {
            Eletro novoeletro = new Eletro();
            Console.Write("Entre com nome:");
            novoeletro.nome = (Console.ReadLine());
            Console.Write("Entre com potencia :");
            novoeletro.potencia = double.Parse(Console.ReadLine());
            Console.Write("Entre com tempo de uso diario:");
            novoeletro.tempousodiario = double.Parse(Console.ReadLine());
            lista.Add(novoeletro);
            Console.Write("Cadastro realizado com sucesso.");
        }
        static void mostrareletro(List<Eletro> listaEletro)
        {
            int posicao = 1;
            for (int i = 0; i < listaEletro.Count; i++)
            {
                Console.WriteLine($"*** Eletrodoméstico {posicao}***");
                Console.WriteLine($"{listaEletro[i].nome} - {listaEletro[i].potencia} - {listaEletro[i].tempousodiario}");
                posicao++;
            }

        }
        static void Main()
        {
            List<Eletro> listaeletro = new List<Eletro>();
            Console.WriteLine("Quantos eletrodomesticos deseja cadastrar?");
            int quantidade = int.Parse(Console.ReadLine());
            for (int i = 0; i < quantidade; i++)
            {
                cadastronovo(listaeletro);

            }
            mostrareletro(listaeletro);
        }
    }
}

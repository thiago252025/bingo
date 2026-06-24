using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace bingo
{
    internal class Program
    {
        static void CadastroJogadores(Random valor, int total_jogadores, Jogo jogo1, StreamWriter arquivojogos)
        {
            arquivojogos.WriteLine("Cadastro de jogador");
            int total_cartelas, idade;
            string nome;
            char sexo;
            for (int i = 0; i < total_jogadores; i++)
            {
                Console.WriteLine("========================");
                Console.WriteLine($"|| Nome do {i + 1}º Jogador ||");
                Console.WriteLine("========================");
                Console.Write("Resposta:");
                nome = Console.ReadLine();
                do
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"|| Sexo do {i + 1}º Jogador (M/F) ||");
                    Console.WriteLine("==============================");
                    Console.Write("Resposta:");
                    sexo = char.Parse(Console.ReadLine());
                } while (sexo != 'M' && sexo != 'F' && sexo != 'f' && sexo != 'm');
                Console.WriteLine("=========================");
                Console.WriteLine($"|| Idade do {i + 1}º Jogador ||");
                Console.WriteLine("=========================");
                Console.Write("Resposta:");
                idade = int.Parse(Console.ReadLine());

                do
                {
                    Console.WriteLine("=========================================================");
                    Console.WriteLine($"|| Quantidade de cartelas do jogador {i + 1}º (Min:1, Max:4) ||");
                    Console.WriteLine("=========================================================");
                    Console.Write("Resposta:");
                    total_cartelas = int.Parse(Console.ReadLine());
                }
                while (total_cartelas > 4 || total_cartelas < 1);
                arquivojogos.WriteLine($"Foi adicionado um jogador. Nome:{nome}, Sexo:{sexo}, Idade:{idade}, Qnt Cartelas:{total_cartelas}\n");
                jogo1.AdicionarJogador(nome, sexo, idade, total_cartelas, valor, arquivojogos);

                Console.Clear();
            }

        }
        static void Instrucoes(StreamWriter arquivojogos)
        {
            string[,] exemplo = { { "bingo", "C0", "C1", "C2" }, { "L0", "1", "2", "3" }, { "L1", "4", "5", "6" }, { "L2", "7", "8", "9" } };
            Console.WriteLine("======================================");
            Console.WriteLine("||            Instruções            ||");
            Console.WriteLine("======================================\n");
            Console.WriteLine("Vai ser apresentada uma cartela a cada jogada");
            Console.WriteLine("A cartela a seguir é somente um exemplo\n");

            for (int i = 0; i < exemplo.GetLength(0); i++)
            {
                for (int j = 0; j < exemplo.GetLength(1); j++)
                {
                    Console.Write(exemplo[i, j] + "\t");
                    arquivojogos.Write(exemplo[i, j] + "\t");
                }
                arquivojogos.WriteLine("\n");
                Console.WriteLine("\n");
            }
            Console.WriteLine("Números após C e L representa as colunas e linhas\n\"C0\" = Coluna 0\t \"L1\" = Linha 1\n");
            Console.WriteLine("Quando for pedido para informar Coluna e Linha, informe somente o número que representa a coluna e a linha.");
            Console.WriteLine("Obrigado e Bom Jogo!!!\n");
            Console.WriteLine("Pressione enter para prosseguir...");
            Console.ReadLine();

            arquivojogos.WriteLine("======================================");
            arquivojogos.WriteLine("||            Instruções            ||\n");
            arquivojogos.WriteLine("======================================");
            arquivojogos.WriteLine("Vai ser apresentada uma cartela a cada jogada");
            arquivojogos.WriteLine("A cartela a seguir é somente um exemplo\n");

            arquivojogos.WriteLine("Números após C e L representa as colunas e linhas\n\"C0\" = Coluna 0\t \"L1\" = Linha 1\n");
            arquivojogos.WriteLine("Quando for pedido para informar Coluna e Linha, informe somente o número que representa a coluna e a linha.");
            arquivojogos.WriteLine("Obrigado e Bom Jogo!!!\n");
            arquivojogos.WriteLine("Pressione enter para prosseguir...\n");
        }
        static void Main(string[] args)
        {
            try
            {
                StreamWriter arquivojogos = new StreamWriter("jogos.txt", false, Encoding.UTF8);
                Random r = new Random();
                int total_jogadores;
                arquivojogos.WriteLine("Código Iniciado\n");
                Instrucoes(arquivojogos);
                Console.Clear();
                Console.WriteLine("Antes de começar o jogo, precisamos de algumas informações...");
                arquivojogos.WriteLine("Antes de começar o jogo, precisamos de algumas informações...");
                Thread.Sleep(2000);
                Console.Clear();
                do
                {
                    Console.WriteLine("===========================================================");
                    Console.WriteLine("|| Quantos Jogadores irá ter neste jogo? (Min: 2 Max: 5) ||");
                    Console.WriteLine("===========================================================");
                    Console.Write("Resposta:");
                    total_jogadores = int.Parse(Console.ReadLine());

                    arquivojogos.WriteLine("Quantos Jogadores irá ter neste jogo? (Min: 2 Max: 5)");
                    arquivojogos.WriteLine($"Resposta: {total_jogadores}");
                }
                while (total_jogadores > 5 || total_jogadores < 2);
                Console.Clear();
                Jogo jogo1 = new Jogo(total_jogadores);
                CadastroJogadores(r, total_jogadores, jogo1, arquivojogos);
                jogo1.IniciarJogo(r, total_jogadores, arquivojogos);
                arquivojogos.Flush();
                arquivojogos.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}
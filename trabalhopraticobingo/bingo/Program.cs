using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace bingo
{
    class Cartela
    {
        public int[,] matrizCart = new int[5, 5];
        public bool[,] matrizBool = new bool[5, 5];

        public Cartela()
        {
            matrizCart[2, 2] = -1;
            matrizBool[2, 2] = true;

        }
        public void PreencherCartela(Random r , Jogador[] jogadores)
        {
            bool valorIne, matrizIgual;
            do
            {
                for (int i = 0; i < matrizCart.GetLength(0); i++)
                {
                    for (int j = 0; j < matrizCart.GetLength(1); j++)
                    {
                        valorIne = false;
                        if (j == 2 && i == 2)
                        {
                            continue;
                        }
                        else if (j == 0)
                        {
                            int valor = r.Next(1, 16);
                            valorIne = VerificaCartela(valor, j);
                            if (valorIne == false)
                            {
                                matrizCart[i, j] = valor;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else if (j == 1)
                        {
                            int valor = r.Next(16, 31);
                            valorIne = VerificaCartela(valor, j);
                            if (valorIne == false)
                            {
                                matrizCart[i, j] = valor;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else if (j == 2)
                        {
                            int valor = r.Next(31, 46);
                            valorIne = VerificaCartela(valor, j);
                            if (valorIne == false)
                            {
                                matrizCart[i, j] = valor;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else if (j == 3)
                        {
                            int valor = r.Next(46, 61);
                            valorIne = VerificaCartela(valor, j);
                            if (valorIne == false)
                            {
                                matrizCart[i, j] = valor;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        else if (j == 4)
                        {
                            int valor = r.Next(61, 76);
                            valorIne = VerificaCartela(valor, j);
                            if (valorIne == false)
                            {
                                matrizCart[i, j] = valor;
                            }
                            else
                            {
                                j--;
                            }
                        }

                    }
                    
                }
                matrizIgual = CartelaIgual(jogadores);
            } while (matrizIgual);
        }
        // verifica se existe número repetido na cartela, na hora da criação 
        private bool VerificaCartela(int valor, int col)
        {
            bool valorIne = false;
            for (int i = 0; i < matrizCart.GetLength(0) && valorIne == false; i++)
            {
                if (matrizCart[i, col] == valor)
                {
                    valorIne = true;
                }
            }
            return valorIne;
        }
        // usuario marca a cartela
        public void MarcarNumero(int col, int lin)
        {
            if (matrizBool[lin, col] == false)
            {
                matrizBool[lin, col] = true;
            }
        }
        //verifica bingo em linha
        public bool VerificarLinha(int linha)
        {
            bool linhaTrue = true;
            for (int j = 0; j < matrizBool.GetLength(1) && linhaTrue == true; j++)
            {
                if (matrizBool[linha, j] == false)
                {
                    linhaTrue = false;
                }
            }
            return linhaTrue;
        }
        //verifica bingo em coluna
        public bool VerificarColuna(int coluna)
        {
            bool colunaTrue = true;
            for (int i = 0; i < matrizBool.GetLength(0) && colunaTrue == true; i++)
            {
                if (matrizBool[i, coluna] == false)
                {
                    colunaTrue = false;
                }
            }
            return colunaTrue;
        }
        //Mostrar a cartela no console
        public void ObterCartela()
        {
            for (int i = 0; i < matrizCart.GetLength(0); i++)
            {
                for (int j = 0; j < matrizCart.GetLength(1); j++)
                {
                    Console.Write(matrizCart[i, j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();

        }
        //verifica se tem cartela com valores iguais com a que está sendo feita
        public bool CartelaIgual(Jogador[] jogadores)
        {
            bool cartelaIgual = false;
            int[,] matrizOutros;
            for(int i = 0; i < jogadores.Length; i++)
            {
                for(int j = 0;j < jogadores[i].cartelasJog.Length; j++)
                {
                    
                }
            }
            return cartelaIgual;
        }
    }
    class Jogador
    {
        public string nome;
        public char sexo;
        public int idade, total_Cartelas;
        public int qntjogadores = 0;

        public Cartela[] cartelasJog;
        public Jogador(string nome, char sexo, int idade, int total_Cartelas, Random valor, Jogador[] jogadores
            )
        {
            
            this.nome = nome;
            this.sexo = sexo;
            this.idade = idade;
            this.total_Cartelas = total_Cartelas;
            cartelasJog = new Cartela[total_Cartelas];
            for (int i = 0; i < cartelasJog.Length; i++)
            {
                AdicionarCartela(valor, jogadores);
            }
            qntjogadores++;
        }
        public void AdicionarCartela(Random valor, Jogador[] jogadores)
        {
            for (int i = 0; i < cartelasJog.Length; i++)
            {
                cartelasJog[i] = new Cartela();
                cartelasJog[i].PreencherCartela(valor, jogadores);
            }
        }
    }
    class Jogo
    {
        public Jogador[] jogadores;
        public int[] numerosSorteados = new int[75];
        public int numSort = 0;
        int qntjogadores = 0;

        public Jogo(int totalJogadores)
        {
            jogadores = new Jogador[totalJogadores];
            qntjogadores++;
        }
        public void AdicionarJogador(string nome, char sexo, int idade, int total_cartelas, Random valor)
        {
            jogadores[qntjogadores] = new Jogador(nome, sexo, idade, total_cartelas, valor, jogadores);
        }
        public int SortearNumero(Random valor)
        {
            bool verificaNumSort;
            int numeroSort;
            do
            {
                verificaNumSort = true;
                numeroSort = valor.Next(1, 76);
                for (int i = 0; i < numerosSorteados.Length && verificaNumSort == true; i++)
                {
                    if (numerosSorteados[i] == numeroSort)
                    {
                        verificaNumSort = false;
                    }
                }
            }
            while (verificaNumSort == false);

            numerosSorteados[numSort] = numeroSort;
            numSort++;
            return numeroSort;
        }
        public void MostrarCartelas()
        {
            for(int i = 0;i < jogadores.Length; i++) 
            {
                Console.WriteLine($"Jogador {i + 1}");
                for(int j = 0; j < jogadores[i].total_Cartelas; j++)
                {
                    jogadores[j].cartelasJog[j].ObterCartela();
                }

            }
        }

    }
    internal class Program
    {
        static void CadastroJogadores(Random valor)
        {
            int total_jogadores, total_cartelas, idade;
            string nome;
            char sexo;
            Console.WriteLine("Antes de começar o jogo, precisamos de algumas informações...");
            Thread.Sleep(5000);
            Console.Clear();
            do
            {
                Console.WriteLine("Quantos Jogadores irá ter neste jogo? (Min: 2, Max: 5)");
                total_jogadores = int.Parse(Console.ReadLine());
            }
            while (total_jogadores > 5 || total_jogadores < 2);
            Console.Clear();
            Jogo jogo1 = new Jogo(total_jogadores);

            for (int i = 0; i < total_jogadores; i++)
            {
                Console.WriteLine($"Nome do {i + 1}º Jogador");
                nome = Console.ReadLine();
                do
                {
                    Console.WriteLine($"Sexo do {i + 1}º Jogador (M/F)");
                    sexo = char.Parse(Console.ReadLine());
                } while (sexo != 'M' && sexo != 'F' && sexo != 'f' && sexo != 'm');
                Console.WriteLine($"Idade do {i + 1}º Jogador");
                idade = int.Parse(Console.ReadLine());

                do
                {
                    Console.WriteLine($"Quantidade de cartelas do jogador {i + 1}º (Min:1, Max:4)");
                    total_cartelas = int.Parse(Console.ReadLine());
                }
                while (total_cartelas > 4 || total_cartelas < 1);
                jogo1.AdicionarJogador(nome, sexo, idade, total_cartelas, valor);
                

                Console.Clear();
            }
            jogo1.MostrarCartelas();
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            {
                Random r = new Random();
                CadastroJogadores(r);
                
            }
        }
    }
}

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
    class Cartela
    {
        public int[,] matrizCart = new int[5, 5];
        public bool[,] matrizBool = new bool[5, 5];

        public Cartela()
        {
            matrizCart[2, 2] = -1;
            matrizBool[2, 2] = true;
        }
        public void PreencherCartela(Random r, Jogador[] jogadores)
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
        public bool MarcarCartela(int lin, int col)
        {
            if (matrizBool[lin, col] == false)
            {
                matrizBool[lin, col] = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DesmarcarCartela(int lin, int col)
        {
            if (matrizBool[lin, col] == true)
            {
                matrizBool[lin, col] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        //verifica bingo em linha
        public bool VerificarLinha()
        {
            bool linhaTrue = false, linhainteiraTrue = false;
            for (int i = 0; i < matrizBool.GetLength(0) && linhaTrue == false; i++)
            {
                for (int j = 0; j < matrizBool.GetLength(1) && linhaTrue == false; j++)
                {
                    if (matrizBool[i, j] == true)
                    {
                        linhaTrue = true;
                    }
                    else
                    {
                        linhaTrue = false;
                    }
                }
                if (linhainteiraTrue) 
                {
                    return true;
                }
            }
            return linhaTrue;
        }
        //verifica bingo em coluna
        public bool VerificarColuna()
        {
            bool colunaTrue = true;
            for (int i = 0; i < matrizBool.GetLength(0) && colunaTrue == true; i++)
            {
                for (int j = 0; j < matrizBool.GetLength(0) && colunaTrue == true; j++)
                {
                    if (matrizBool[i, j] == false)
                    {
                        colunaTrue = false;
                    }
                }
            }
            return colunaTrue;
        }
        //Mostrar a cartela no console
        public void ObterCartela()
        {
            if (CartelaEmJogo())
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
        }
        //verifica se tem cartela com valores iguais com a que está sendo feita
        public bool CartelaIgual(Jogador[] jogadores)
        {
            bool cartelaIgual = false;
            //for(int i = 0; i < jogadores.Length; i++)
            //{
            //    for(int j = 0;j < jogadores[i].cartelasJog.Length; j++)
            //    {

            //    }
            //}
            return cartelaIgual;
        }
        public Cartela CartelasOutros(Jogador[] jogadores, int jogador, int cartela)
        {
            return jogadores[jogador].cartelasJog[cartela];
        }
        public void CartelaEliminada(int jogador, int cartela)
        {
            matrizCart[0, 0] = 0;
        }
        public bool CartelaEmJogo()
        {
            if (matrizCart[0,0] != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Jogador
    {
        public string nome;
        public char sexo;
        public int idade, total_Cartelas;

        public Cartela[] cartelasJog;
        public Jogador(string nome, char sexo, int idade, int total_Cartelas, Random valor, Jogador[] jogadores)
        {

            this.nome = nome;
            this.sexo = sexo;
            this.idade = idade;
            this.total_Cartelas = total_Cartelas;
            cartelasJog = new Cartela[total_Cartelas];

            AdicionarCartela(valor, jogadores);

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
        public Jogador[] ranking; 
        public Jogo(int totalJogadores)
        {
            jogadores = new Jogador[totalJogadores];
            ranking = new Jogador[totalJogadores];

        }
        public void AdicionarJogador(string nome, char sexo, int idade, int total_cartelas, Random valor)
        {
            jogadores[qntjogadores] = new Jogador(nome, sexo, idade, total_cartelas, valor, jogadores);
            qntjogadores++;
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
        public void MostrarCartela(int i)
        {
            for (int j = 0; j < jogadores.Length; j++)
            {
                jogadores[i].cartelasJog[j].ObterCartela();
            }
        }
        public void IniciarJogo(Random valor, int total_jogadores)
        {
            
            //variaveis MarDes vão conter informações para marcar ou desmarcar 
            int resposta, MarDesLinha, MarDesColuna, jogadorestotais = total_jogadores;
            char lc;
            
            Console.Clear();
            do
            {
                int numeroSorteado = SortearNumero(valor);
                for (int i = 0; i < total_jogadores; i++)
                {
                    for (int j = 0; j < jogadores[i].cartelasJog.Length; j++)
                    {
                        jogadores[i].cartelasJog[j].CartelaEmJogo();
                        Console.WriteLine($"O número sorteado foi {numeroSorteado}");
                        Console.WriteLine($"Cartela do Jogador {i + 1}");
                        jogadores[i].cartelasJog[j].ObterCartela();
                        Console.WriteLine("Deseja fazer algo ?");
                        Console.WriteLine("[1] Marcar");
                        Console.WriteLine("[2] Desmarcar");
                        Console.WriteLine("[3] Gritar Bingo");
                        if (j == jogadores[i].cartelasJog.Length - 1)
                        {
                            Console.WriteLine("[4] Próximo Jogador");
                        }
                        else
                        {
                            Console.WriteLine("[4] Próxima Cartela");
                        }
                        do
                        {
                            Console.Write("Digite o número para selecionar a opção: ");
                            resposta = int.Parse(Console.ReadLine());
                            switch (resposta)
                            {
                                case 1:
                                    Console.WriteLine("Qual posição deseja marcar (Linha e Coluna) ?");
                                    Console.Write("Linha: ");
                                    MarDesLinha = int.Parse(Console.ReadLine());
                                    Console.Write("\nColuna: ");
                                    MarDesColuna = int.Parse(Console.ReadLine());
                                    if(jogadores[i].cartelasJog[j].MarcarCartela(MarDesLinha, MarDesColuna))
                                    {
                                        Console.WriteLine("Posição marcada com sucesso !!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Posição já está marcada");
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("Qual posição deseja desmarcar ?");
                                    Console.Write("Linha: ");
                                    MarDesLinha = int.Parse(Console.ReadLine());
                                    Console.Write("\nColuna: ");
                                    MarDesColuna = int.Parse(Console.ReadLine());
                                    if (jogadores[i].cartelasJog[j].DesmarcarCartela(MarDesLinha, MarDesColuna))
                                    {
                                        Console.WriteLine("Posição desmarcada com sucesso !!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Posição não estava marcada para a ação desmarcar ser executada");
                                    }
                                    break;
                                case 3:
                                    Console.Clear();
                                    Console.Write("Linha ou Coluna está completa ? (Use L/C): ");
                                    lc = char.Parse(Console.ReadLine());
                                    Console.Clear();
                                    if (GritarBingo(lc, i, j))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Parabéns sua cartela estava correta !!!");
                                        jogadorestotais--;
                                        ranking[VerificaRanking()] = jogadores[i];
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sua cartela foi retirada do jogo, pois o BINGO foi gritado de forma errada...");
                                    }
                                    break;
                                case 4:
                                    break;
                                default:
                                    Console.WriteLine("Opção Inexistente");
                                    break;
                            }
                        } while (resposta != 4);
                        Console.Clear();
                    }
                }
            } while (jogadorestotais != 1);
        }
        public int VerificaRanking()
        {
            int posranking = 0;
            for(int i = 0; i < ranking.Length; i++)
            {
                if (ranking[i] == null)
                {
                    posranking = i;
                    return posranking;
                }
            }
            return posranking;
        }

        public bool GritarBingo(char lc, int jogador, int cartela)
        {
            bool resultado;
            if(lc == 'l' || lc == 'L')
            {
                resultado = jogadores[jogador].cartelasJog[cartela].VerificarLinha(); 
            }
            else
            {
                resultado = jogadores[jogador].cartelasJog[cartela].VerificarColuna();
            }
            return resultado;
        }

    }


    internal class Program
    {
        static void CadastroJogadores(Random valor, int total_jogadores, Jogo jogo1)
        {
            int total_cartelas, idade;
            string nome;
            char sexo;


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
        }
        static void Main(string[] args)
        {
            {
                Random r = new Random();
                int total_jogadores;
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
                CadastroJogadores(r, total_jogadores, jogo1);
                jogo1.IniciarJogo(r, total_jogadores);
            }
        }
    }
}
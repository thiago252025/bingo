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
        string[] indicaColunas = { " ", "C0", "C1", "C2", "C3", "C4" };
        string[] indicaLinhas = {"L0", "L1", "L2", "L3", "L4" };
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
            bool linhaTrue = false;
            for (int i = 0; i < matrizBool.GetLength(0); i++)
            {
                linhaTrue = true;
                for (int j = 0; j < matrizBool.GetLength(1) && linhaTrue == true; j++)
                {
                    if (matrizBool[i, j] == false)
                    {
                        linhaTrue = false;
                    }
                }
                if (linhaTrue) 
                {
                    return true;
                }
            }
            return linhaTrue;
        }
        //verifica bingo em coluna
        public bool VerificarColuna()
        {
            bool colunaTrue = false;
            for (int i = 0; i < matrizBool.GetLength(0); i++)
            {
                colunaTrue = true;
                for (int j = 0; j < matrizBool.GetLength(0) && colunaTrue == true; j++)
                {
                    if (matrizBool[j, i] == false)
                    {
                        colunaTrue = false;
                    }
                }
                if (colunaTrue)
                {
                    return true;
                }
            }
            return colunaTrue;
        }
        //Mostrar a cartela no console
        public void ObterCartela()
        {
            if (CartelaEmJogo())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("bingo");
                for (int i = 0; i < indicaColunas.Length; i++)
                {
                    Console.Write(indicaColunas[i] + "\t");
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < matrizCart.GetLength(0); i++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;                    
                    Console.Write(indicaLinhas[i] + "\t");
                    Console.ResetColor();
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
        public void CartelaEliminada()
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
        // verifica quantas cartelas o jogador ainda tem
        public bool VerificaQntCartelasJog()
        {
            if(matrizCart[0, 0] != 0)
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
                        if (jogadores[i].cartelasJog[j].CartelaEmJogo())
                        {


                            Console.Write("Os números sorteados foram: ");
                            for (int l = 0; l < numSort; l++)
                            {
                                Console.Write(numerosSorteados[l] + " | ");
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine($"O número sorteado nesta rodada foi {numeroSorteado}\n\n");
                            Console.WriteLine($"Jogador {i+1}\nCartela {j + 1}\n");
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
                                        Console.Write("Coluna: ");
                                        MarDesColuna = int.Parse(Console.ReadLine());
                                        if (jogadores[i].cartelasJog[j].MarcarCartela(MarDesLinha, MarDesColuna))
                                        {
                                            Console.WriteLine("Posição marcada com sucesso !!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Posição já está marcada");
                                        }
                                        Thread.Sleep(1500);
                                        resposta = 4;
                                        break;
                                    case 2:
                                        Console.WriteLine("Qual posição deseja desmarcar ?");
                                        Console.Write("Linha: ");
                                        MarDesLinha = int.Parse(Console.ReadLine());
                                        Console.Write("Coluna: ");
                                        MarDesColuna = int.Parse(Console.ReadLine());
                                        if (jogadores[i].cartelasJog[j].DesmarcarCartela(MarDesLinha, MarDesColuna))
                                        {
                                            Console.WriteLine("Posição desmarcada com sucesso !!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Posição não estava marcada para a ação desmarcar ser executada");
                                        }
                                        Thread.Sleep(1500);
                                        resposta = 4;
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
                                            ranking[VerificaRankingGanhador()] = jogadores[i];
                                        }
                                        else
                                        {
                                            Console.WriteLine("Sua cartela foi retirada do jogo, pois o BINGO foi gritado de forma errada...");
                                            jogadores[i].cartelasJog[j].CartelaEliminada();
                                            int qntCartelasJog = 0;
                                            for (int k = 0; k < jogadores[i].cartelasJog.Length; k++)
                                            {
                                                if (jogadores[i].cartelasJog[k].VerificaQntCartelasJog())
                                                {
                                                    qntCartelasJog++;
                                                }
                                            }
                                            if (qntCartelasJog == 0)
                                            {
                                                jogadorestotais--;
                                                jogadores[i].total_Cartelas--;
                                                ranking[VerificaRankingPerdedor()] = jogadores[i];
                                            }
                                            if (jogadorestotais == 1)
                                            {
                                                ranking[VerificaRankingGanhador()] = jogadores[i - 1];
                                            }

                                        }
                                        Thread.Sleep(2000);
                                        resposta = 4;
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
                }
            } while (jogadorestotais != 1);
            MostrarRanking();
        }
        public int VerificaRankingGanhador()
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
        public int VerificaRankingPerdedor()
        {
            int posranking = ranking.Length-1;
            for (int i = ranking.Length-1; i >= 0; i--)
            {
                if (ranking[i] == null)
                {
                    posranking = i;
                    return posranking;
                }
            }
            return posranking;
        }
        public void MostrarRanking()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("||    O RANKING FICOU ASSIM    ||");
            Console.WriteLine("=================================");
            for (int i = 0; i < ranking.Length; i++) 
            {
                Console.WriteLine($"{i+1}º LUGAR: {ranking[i].nome} | {ranking[i].idade} | {ranking[i].sexo}");
            }
            Console.ReadLine();
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
                jogo1.AdicionarJogador(nome, sexo, idade, total_cartelas, valor);


                Console.Clear();
            }
            
        }
        static void Instrucoes()
        {
            string[,] exemplo = { { "bingo", "C0", "C1", "C2" }, { "L0", "1", "2", "3" }, { "L1", "4", "5", "6" }, { "L2", "7", "8", "9" } };
            Console.WriteLine("======================================");
            Console.WriteLine("||            Instruções            ||");
            Console.WriteLine("======================================\n");
            Console.WriteLine("Vai ser apresentada uma cartela a cada jogada");
            Console.WriteLine("A cartela a seguir é somente um exemplo\n");
            for(int i = 0; i < exemplo.GetLength(0); i++) 
            {
                for(int j = 0; j < exemplo.GetLength(1); j++)
                {
                    Console.Write(exemplo[i,j] + "\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("Números após C e L representa as colunas e linhas\n\"C0\" = Coluna 0\t \"L1\" = Linha 1\n");
            Console.WriteLine("Quando for pedido para informar Coluna e Linha, informe somente o número que representa a coluna e a linha.");
            Console.WriteLine("Obrigado e Bom Jogo!!!\n");
            Console.WriteLine("Pressione enter para prosseguir...");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            {
                Random r = new Random();
                int total_jogadores;
                Instrucoes();
                Console.Clear();
                Console.WriteLine("Antes de começar o jogo, precisamos de algumas informações...");
                Thread.Sleep(5000);
                Console.Clear();
                do
                {
                    Console.WriteLine("============================================================");
                    Console.WriteLine("|| Quantos Jogadores irá ter neste jogo? (Min: 2, Max: 5) ||");
                    Console.WriteLine("============================================================");
                    Console.Write("Resposta:");
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
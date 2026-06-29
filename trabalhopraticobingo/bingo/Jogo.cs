using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bingo
{
    internal class Jogo
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
        public void AdicionarJogador(string nome, char sexo, int idade, int total_cartelas, Random valor, StreamWriter arquivojogos)
        {
            jogadores[qntjogadores] = new Jogador(nome, sexo, idade, total_cartelas, valor, jogadores, arquivojogos);
            qntjogadores++;
        }
        public int SortearNumero(Random valor, StreamWriter arquivojogos)
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
            for (int i = 0; i < jogadores.Length; i++)
            {
                for (int j = 0; j < jogadores[i].cartelasJog.Length; j++)
                {
                    jogadores[i].cartelasJog[j].MarcarCartelaJogo(numeroSort);
                }
            }

            numSort++;
            return numeroSort;
        }
        // Metodo seleção de resposta
        public int MenuResposta(int resposta, int jogador, int cartela, ref int jogadorestotais, StreamWriter arquivojogos)
        {
            int linha, coluna;
            char lc;

            arquivojogos.WriteLine($"Menu de Resposta\n");
            if (resposta == 1)
            {
                Console.WriteLine("Qual posição deseja marcar (Linha e Coluna) ?");
                Console.Write("Linha: ");
                linha = int.Parse(Console.ReadLine());
                Console.Write("Coluna: ");
                coluna = int.Parse(Console.ReadLine());
                arquivojogos.WriteLine($"Jogador quer marcar a posição Linha: {linha} Coluna: {coluna}");
                if (jogadores[jogador].cartelasJog[cartela].MarcarCartelaJogador(linha, coluna))
                {
                    Console.WriteLine("Posição marcada com sucesso !!");
                    arquivojogos.WriteLine("Posição marcada com sucesso !!");
                }
                else
                {
                    Console.WriteLine("Posição já está marcada");
                    arquivojogos.WriteLine("Posição já está marcada");
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
            else if (resposta == 2)
            {
                Console.WriteLine("Qual posição deseja desmarcar ?");
                Console.Write("Linha: ");
                linha = int.Parse(Console.ReadLine());
                Console.Write("Coluna: ");
                coluna = int.Parse(Console.ReadLine());
                arquivojogos.WriteLine($"Jogador quer desmarcar a posição Linha: {linha} Coluna: {coluna}\n");
                if (coluna == 2 && linha == 2)
                {
                    Console.WriteLine("Não é possível desmarcar essa posição !!");
                    arquivojogos.WriteLine("Não é possível desmarcar essa posição !!\n");
                    Thread.Sleep(1000);
                }
                else
                {
                    if (jogadores[jogador].cartelasJog[cartela].DesmarcarCartela(linha, coluna))
                    {
                        Console.WriteLine("Posição desmarcada com sucesso !!");
                        arquivojogos.WriteLine("Posição desmarcada com sucesso !!\n");
                    }
                    else
                    {
                        Console.WriteLine("Posição não estava marcada para a ação desmarcar ser executada");
                        arquivojogos.WriteLine("Posição não estava marcada para a ação desmarcar ser executada\n");
                    }
                    Thread.Sleep(1000);
                }
                Console.Clear();
            }
            else if (resposta == 3)
            {

                Console.Clear();
                Console.Write("Linha ou Coluna está completa ? (Use L/C): ");
                lc = char.Parse(Console.ReadLine());
                arquivojogos.WriteLine($"Jogador{jogador} com a cartela {cartela} gritou bingo\n");
                Console.Clear();
                if (GritarBingo(lc, jogador, cartela))
                {
                    Console.Clear();
                    Console.WriteLine("Parabéns sua cartela estava correta !!!");
                    jogadorestotais--;

                    ranking[VerificaRankingGanhador()] = jogadores[jogador];
                    jogadores[jogador].noJogo = false;
                    if (jogadorestotais == 1)
                    {


                        for (int k = 0; k < jogadores.Length; k++)
                        {
                            if (jogadores[k].noJogo)
                            {
                                ranking[VerificaRankingPerdedor()] = jogadores[k];
                            }
                        }
                    }
                    arquivojogos.WriteLine($"Jogador{jogador} com a cartela {cartela} gritou bingo certo, foi para o ranking\n");
                    resposta = 4;
                }
                else
                {
                    arquivojogos.WriteLine($"Jogador{jogador} com a cartela {cartela} gritou bingo errado\n");
                    Console.WriteLine("Sua cartela foi retirada do jogo, pois o BINGO foi gritado de forma errada...");
                    jogadores[jogador].cartelasJog[cartela].CartelaEliminada();
                    int qntCartelasJog = 0;
                    for (int k = 0; k < jogadores[jogador].cartelasJog.Length; k++)
                    {
                        if (jogadores[jogador].cartelasJog[k].CartelaEmJogo())
                        {
                            qntCartelasJog++;
                        }
                    }
                    if (qntCartelasJog == 0)
                    {
                        jogadorestotais--;
                        jogadores[jogador].noJogo = false;
                        ranking[VerificaRankingPerdedor()] = jogadores[jogador];
                        arquivojogos.WriteLine("Jogador eliminado do jogo e foi adicionado no ranking\n");
                    }
                    if (jogadorestotais == 1)
                    {
                        for (int k = 0; k < jogadores.Length; k++)
                        {
                            if (jogadores[k].noJogo)
                            {
                                ranking[VerificaRankingGanhador()] = jogadores[k];
                            }
                        }

                    }

                    resposta = 4;
                }
                Thread.Sleep(1500);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Opção Inexistente");
                Thread.Sleep(1500);
                Console.Clear();

            }

            return resposta;
        }
        // Metodo de seleção de opções
        public int MenuOpcoes(int jogador, int cartela, ref int jogadorestotais, StreamWriter arquivojogos)
        {
            arquivojogos.WriteLine("Menu de Opções\n");
            arquivojogos.WriteLine($"Perguntado o que o jogador {jogador} quer fazer\n");
            int resposta;

            Console.WriteLine("Deseja fazer algo ?");
            Console.WriteLine("[1] Marcar");
            Console.WriteLine("[2] Desmarcar");
            Console.WriteLine("[3] Gritar Bingo");

            Console.WriteLine("[4] Próxima Cartela/Jogador");

            Console.Write("Digite o número para selecionar a opção: ");
            resposta = int.Parse(Console.ReadLine());
            if (resposta == 4)
            {
                arquivojogos.WriteLine("Jogador pulou para a próxima cartela/jogador\n");
                Console.Clear();
                return resposta;
            }


            return MenuResposta(resposta, jogador, cartela, ref jogadorestotais, arquivojogos);
        }
        //Metodo para mostrar o menu principal
        public int MenuPrincipal(int jogador, int cartela, ref int jogadorestotais, int numeroSorteado, StreamWriter arquivojogos)
        {
            Console.Write("Os números sorteados foram: ");
            for (int l = 0; l < numSort; l++)
            {
                Console.Write(numerosSorteados[l] + " | ");
            }
            arquivojogos.WriteLine();
            Console.WriteLine("\n");
            Console.WriteLine($"O número sorteado nesta rodada foi {numeroSorteado}\n\n");
            Console.WriteLine($"Jogador {jogador + 1} | {jogadores[jogador].nome}\nCartela {cartela + 1}\n");
            jogadores[jogador].cartelasJog[cartela].ObterCartela();
            arquivojogos.WriteLine("Menu principal\n");
            arquivojogos.WriteLine($"Informado o número que foi sorteado na rodada: {numeroSorteado}, vez do jogador {jogador}\n");
            return MenuOpcoes(jogador, cartela, ref jogadorestotais, arquivojogos);
        }
        public void IniciarJogo(Random valor, int total_jogadores, StreamWriter arquivojogos)
        {
            arquivojogos.WriteLine("Jogo Iniciado");
            int jogadorestotais = total_jogadores, numeroSorteado = 0;
            int resposta = 0;
            Console.Clear();
            do
            {

                if (numSort < 75)
                {
                    numeroSorteado = SortearNumero(valor, arquivojogos);
                }
                for (int i = 0; i < total_jogadores; i++)
                {
                    if (jogadorestotais == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < jogadores[i].cartelasJog.Length && jogadores[i].noJogo; j++)
                    {
                        if (jogadores[i].cartelasJog[j].CartelaEmJogo())
                        {
                            do
                            {
                                resposta = MenuPrincipal(i, j, ref jogadorestotais, numeroSorteado, arquivojogos);

                            } while (resposta != 4 && jogadorestotais != 1);
                        }
                    }
                }
            } while (jogadorestotais != 1);

            arquivojogos.Write("Os números sorteados foram: ");
            for (int i = 0; i < numerosSorteados.Length; i++)
            {
                if (numerosSorteados[i] != 0)
                {
                    arquivojogos.Write(numerosSorteados[i] + " | ");
                }
            }

            arquivojogos.WriteLine();
            arquivojogos.WriteLine();
            MostrarRanking(arquivojogos);
        }
        public int VerificaRankingGanhador()
        {
            int posranking = 0;
            for (int i = 0; i < ranking.Length; i++)
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
            int posranking = ranking.Length - 1;
            for (int i = ranking.Length - 1; i >= 0; i--)
            {
                if (ranking[i] == null)
                {
                    posranking = i;
                    return posranking;
                }
            }
            return posranking;
        }
        public void MostrarRanking(StreamWriter arquivojogos)
        {
            arquivojogos.WriteLine("JOGO FINALIZADO");
            Console.WriteLine("=================================");
            Console.WriteLine("||    O RANKING FICOU ASSIM    ||");
            Console.WriteLine("=================================");

            arquivojogos.WriteLine("=================================");
            arquivojogos.WriteLine("||    O RANKING FICOU ASSIM    ||");
            arquivojogos.WriteLine("=================================");
            for (int i = 0; i < ranking.Length; i++)
            {
                Console.WriteLine($"{i + 1}º LUGAR: {ranking[i].nome} | {ranking[i].idade} | {ranking[i].sexo}");
                arquivojogos.WriteLine($"{i + 1}º LUGAR: {ranking[i].nome} | {ranking[i].idade} | {ranking[i].sexo}\n");
            }
        }
        public bool GritarBingo(char lc, int jogador, int cartela)
        {
            bool resultado;
            if (lc == 'l' || lc == 'L')
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
}

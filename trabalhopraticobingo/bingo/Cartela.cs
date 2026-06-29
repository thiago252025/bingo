using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bingo
{
    internal class Cartela
    {
        public int[,] matrizCart = new int[5, 5];
        public bool[,] matrizBool = new bool[5, 5];
        public bool[,] cartelaMarcada = new bool[5, 5];
        string[] indicaColunas = { " ", "C0", "C1", "C2", "C3", "C4" };
        string[] indicaLinhas = { "L0", "L1", "L2", "L3", "L4" };
        public Cartela()
        {
            matrizCart[2, 2] = -1;
            matrizBool[2, 2] = true;
            cartelaMarcada[2, 2] = true;
        }
        public void PreencherCartela(Random r, Jogador[] jogadores, StreamWriter arquivojogos)
        {
            arquivojogos.WriteLine("Preenchendo cartela");
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
                arquivojogos.WriteLine("Verificando se a cartela é igual\n");
            } while (matrizIgual);
            arquivojogos.WriteLine("Cartela Pronta para ser usada\n");
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
        //ao sortear o número, o número é marcado na cartela
        public void MarcarCartelaJogo(int valor)
        {
            if (matrizCart[0, 0] != 0)
            {
                for (int i = 0; i < matrizCart.GetLength(0); i++)
                {
                    for (int j = 0; j < matrizCart.GetLength(1); j++)
                    {
                        if (matrizCart[i, j] == valor)
                        {
                            matrizBool[i, j] = true;
                        }
                    }
                }
            }
        }
        // usuario marca a cartela
        public bool MarcarCartelaJogador(int lin, int col)
        {
            if (cartelaMarcada[lin, col] == false)
            {
                cartelaMarcada[lin, col] = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DesmarcarCartela(int lin, int col)
        {
            if (cartelaMarcada[lin, col] == true)
            {
                cartelaMarcada[lin, col] = false;
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
                    if (cartelaMarcada[i, j] == false || matrizBool[i, j] == false)
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
                for (int j = 0; j < matrizBool.GetLength(1) && colunaTrue == true; j++)
                {
                    if (cartelaMarcada[j, i] == false || matrizBool[j, i] == false)
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
                        if (cartelaMarcada[i, j] == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(matrizCart[i, j] + "\t");
                        Console.ResetColor();
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
            bool cartelaIgual;
            int cartelasIguais = 0;
            if (jogadores.Length > 0)
            {
                for (int i = 0; i < jogadores.Length; i++)
                {
                    if (jogadores[i] != null)
                    {
                        for (int j = 0; j < jogadores[i].cartelasJog.Length; j++)
                        {
                            cartelaIgual = true;
                            for (int k = 0; k < matrizCart.GetLength(0) && cartelaIgual; k++)
                            {
                                for (int l = 0; l < matrizCart.GetLength(1) && cartelaIgual; l++)
                                {
                                    if (matrizCart[k, l] != jogadores[i].cartelasJog[j].matrizCart[k, l])
                                    {
                                        cartelaIgual = false;
                                    }

                                }
                            }
                            if (cartelaIgual)
                            {
                                cartelasIguais++;
                            }
                        }
                    }
                }
            }
            return cartelasIguais > 0;
        }
        // elimina a cartela do jogador
        public void CartelaEliminada()
        {
            matrizCart[0, 0] = 0;
        }
        // verifica se a cartela ta em  jogo
        public bool CartelaEmJogo()
        {
            if (matrizCart[0, 0] != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void PreencherCartela(Random r)
        {
            bool valorIne;
            bool linhaTrue;
            for (int i = 0; i < matrizCart.GetLength(0); i++)
            {
                for (int j = 0; j < matrizCart.GetLength(1); j++)
                {
                    valorIne = false;
                    if (j == 2 && i == 2)
                    {

                    }
                    else if (j == 0)
                    {
                        int valor = r.Next(1, 16);
                        valorIne = VerificaCartela(valor, j);
                        if (valorIne == false)
                        {
                            matrizCart[i, j] = valor;
                            MarcarNumero(valor, j);
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
                            MarcarNumero(valor, j);
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
                            MarcarNumero(valor, j);
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
                            MarcarNumero(valor, j);
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
                            MarcarNumero(valor, j);
                        }
                        else
                        {
                            j--;
                        }
                    }

                }
                linhaTrue = VerificarLinha(i);
            }
        }
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
        public void MarcarNumero(int valor, int col)
        {
            for (int i = 0; i < matrizBool.GetLength(0); i++)
            {             
                    if (matrizCart[i, col] == valor)
                    {
                        matrizBool[i, col] = true;  
                    }
            }
        }
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
    }
    class Jogador
    {
        string nome;
        char sexo;
        int idade, total_Cartelas;
        Random valor = new Random();

        public Cartela[] cartelasJog;
        public Jogador(string nome, char sexo, int idade, int total_Cartelas)
        {
            this.nome = nome;
            this.sexo = sexo;
            this.idade = idade;
            this.total_Cartelas = total_Cartelas;
            cartelasJog = new Cartela[total_Cartelas];
        }
        public void AdicionarCartela(Cartela cart)
        {
            for (int i = 0; i < cartelasJog.Length; i++)
            {
                cart.PreencherCartela(valor);
                cartelasJog[i] = cart;
            }
        }
    }
    class Jogo
    {
        Jogador[] jogadores;
        public int[] numerosSort = new int[75];
        public int numSort = 0;
        bool verificaNumSort = true;
        public Jogo(int totalJogadores)
        {
            this.jogadores = new Jogador[totalJogadores];
        }
        public int[] SortearNumero(Random valor)
        {
            int numeroSort = valor.Next(1, 76);
            for (int i = 0; i < numeroSort && verificaNumSort == true; i++)
            {
                for (int j = 0; j < numeroSort && verificaNumSort == true; j++)
                {
                    if (numerosSort[j] == numeroSort)
                    {
                        verificaNumSort = false;
                    }
                }
                if (verificaNumSort)
                {
                    numerosSort[i] = numeroSort;
                    numSort++;

                }
            }
            return numerosSort;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}


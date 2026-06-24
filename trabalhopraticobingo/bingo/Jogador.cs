using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bingo
{
    internal class Jogador
    {
        public string nome;
        public char sexo;
        public int idade, total_Cartelas;
        public bool noJogo;

        public Cartela[] cartelasJog;
        public Jogador(string nome, char sexo, int idade, int total_Cartelas, Random valor, Jogador[] jogadores, StreamWriter arquivojogos)
        {

            this.nome = nome;
            this.sexo = sexo;
            this.idade = idade;
            this.total_Cartelas = total_Cartelas;
            noJogo = true;
            cartelasJog = new Cartela[total_Cartelas];

            AdicionarCartela(valor, jogadores, arquivojogos);

        }
        public void AdicionarCartela(Random valor, Jogador[] jogadores, StreamWriter arquivojogos)
        {
            for (int i = 0; i < cartelasJog.Length; i++)
            {
                cartelasJog[i] = new Cartela();
                cartelasJog[i].PreencherCartela(valor, jogadores, arquivojogos);
                arquivojogos.WriteLine($"Cartela {i} do jogador {nome} foi preenchida\n");

                for (int j = 0; j < cartelasJog[i].matrizCart.GetLength(0); j++)
                {
                    for (int k = 0; k < cartelasJog[i].matrizCart.GetLength(1); k++)
                    {
                        arquivojogos.Write(cartelasJog[i].matrizCart[j, k] + "\t");
                    }
                    arquivojogos.WriteLine();
                }
            }
        }
    }
}

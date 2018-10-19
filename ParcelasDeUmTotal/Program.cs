using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelasDeUmTotal
{
    class Program
    {
        static int size;
        static int posicao;
        static void Main(string[] args)
        {
            //O programa inicia aqui.
            Primeiro();
    
        }
        
        //-------------------------------------------------------------
        public static void Primeiro()
        {
            lerArquivo();

            /*set = new double[,] 
            {              
                {326.63365, 7900    },
                {599.99295, 7901    },
                {167.30112, 7945    }
            };*/

            //se nao quiser que aparece os valore e so comentar
            //ImprimirSet(set, "INICIANDO O SET.");

            //gbs ESTE É O VALOR QUE SERÁ BUSCADO
            soma = 232.29;

            //id | nº lancamento | valor
            double[,] teste = {
                {176    ,8407  ,750},
                {27     ,7959  ,1150.01},
                {31     ,7959  ,1189},
                {138    ,8273  ,1208.86},
                {175    ,8407  ,1303.56},
                {172    ,8363  ,1850},
                {170    ,8357  ,1885.17},
                {29     ,7959  ,2333.31},
                {128    ,8024  ,2456.68},
                {17     ,7886  ,2574},
                {22     ,7958  ,2674.02},
                {26     ,7959  ,2674.02},
                {150    ,8347  ,2927.45},
                {127    ,8007  ,3000},
                {135    ,8188  ,3160.86},
                {154    ,8355  ,4000},
                {11     ,7882  ,4467.58},
                {18     ,7886  ,4860.02},
                {12     ,7883  ,5000},
                {30     ,8007  ,5000},
                {153    ,8353  ,5000},
                {134    ,8188  ,5839.14},
                {147    ,8347  ,6072.55},
                {155    ,8357  ,6308.92},
                {21     ,7887  ,6325.98},
                {132    ,8024  ,6543.32},
                {171    ,8363  ,6917.71},
                {151    ,8348  ,7000},
                {152    ,8349  ,8000},
                {15     ,7884  ,8139.8},
                {144    ,8273  ,8291.14},
                {136    ,8189  ,9000},
                {145    ,8311  ,9000},
                {146    ,8346  ,9000},
                {137    ,8271  ,9776},
                {133    ,8187  ,15000}
            };
            //{ 232.29, 325.98,426,434.2,532.42,546.44,565.98,653.66,664.15,674.02,750,1150.01,1189,1208.86,1303.56,1850,1885.17,2333.31,2456.68,2574,2674.02,2674.02,
            //              2927.45,3000,3160.86,4000,4467.58,4860.02,5000,5000,5000,5839.14,6072.55,6308.92,6325.98,6543.32,6917.71,7000,8000,8139.8,8291.14,9000,9000,9000,9776,15000,};


            soma = teste[posicao, 2];

            Console.WriteLine("");
            Console.WriteLine("PROCURANDO VALORES QUE COMPOEM A SOMA = {0}" + "; proc " + teste[posicao, 0] + "; adiantamento " + teste[posicao, 1], soma);

            EncontrarSubsetSoma();

        }

        //static double[,] set = new double[400, 2];
        static double[,] set;
        static int[] subSetIndexes;
        static double soma;
        static int numerosOfSubsetSums;

        //------------------------------------------------------------

        private static void EncontrarSubsetSoma()
        {
            numerosOfSubsetSums = 0;
            int numerosOfElements = size;
            EncontrarPowerSet(numerosOfElements);
        }
        //-------------------------------------------------------------

        private static void EncontrarPowerSet(int n)
        {
            // Super set - todos os conjuntos com tamanho: 0, 1, ..., n - 1
            for (int k = 0; k <= n - 1; k++)
            {
                subSetIndexes = new int[k];
                CombinacaoNaoRepeticao(k, 0, n - 1);
            }

            if (numerosOfSubsetSums == 0)
            {
                Console.WriteLine("Não existem subconjuntos com soma desejada.");
                Console.ReadKey();
            }
        }
        //-------------------------------------------------------------

        private static void CombinacaoNaoRepeticao(int k, int iInicio, int iFim)
        {
            if (k == 0)
            {
                EmprimirSubSet();
                return;
            }

            for (int i = iInicio; i <= iFim; i++)
            {
                subSetIndexes[k - 1] = i;
                ++iInicio;
                CombinacaoNaoRepeticao(k - 1, iInicio, iFim);
            }
        }

        private static void EmprimirSubSet()
        {

            double correnteSubsetSoma = 0;

            // Acumular a soma do subconjunto atual
            for (int i = 0; i < subSetIndexes.GetLength(0); i++)
            {
                correnteSubsetSoma += set[subSetIndexes[i], 0];
            }

            // Se desejado sum: Imprimir elementos do subconjunto atual
            if (correnteSubsetSoma == soma)
            {
                ++numerosOfSubsetSums;

                Console.Write("($");
                for (int i = 0; i < subSetIndexes.Length; i++)
                {
                    Console.Write(set[subSetIndexes[i], 0] + " " + set[subSetIndexes[i], 1]);
                    //Console.Write( set[subSetIndexes[i], 1]);

                    if (i < subSetIndexes.Length - 1)
                    {
                        Console.Write(" ,$");

                    }

                }

                Console.WriteLine(")");
                remove();
                Console.ReadKey();

                posicao++;

                Primeiro();
                if (posicao == 45)
                {
                    System.Environment.Exit(0);
                }
            }
        }

        private static void remove()
        {
            Stream saida = File.Open("c:/gbs.txt", FileMode.Truncate);
            StreamWriter escritor = new StreamWriter(saida);

            for (int i = 0; i < size; ++i)
            {
                bool find = false;
                for (int j = 0; j < subSetIndexes.Length; ++j)
                    if (i == subSetIndexes[j])
                    {
                        find = true;
                        break;
                    }
                if (!find)
                    escritor.WriteLine(set[i, 0] + "@" + set[i, 1]);
            }

            escritor.Close();
            saida.Close();

        }

        //-------------------------------------------------------------

        private static void ImprimirSet(double[,] arr, string label = "")
        {
            Console.WriteLine(label);

            Console.Write("{");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write(arr[i, 0]);

                if (i < arr.Length - 1)
                {
                    Console.Write(" ,");
                }
            }
            Console.WriteLine("}");

        }

        private static void lerArquivo()
        {
            Stream entrada = File.Open("c:/arquivo.txt", FileMode.Open);

            List<string> list = new List<string>();
            int i = 0;

            StreamReader leitor = new StreamReader(entrada);
            //int size = leitor.;
            set = new double[500, 2];
            string linha = leitor.ReadLine();
            size = 0;
            while (linha != null)
            {
                //list.Add(leitor.ReadLine());
                linha = leitor.ReadLine();

                string primeiro = linha;
                if (linha != null)
                {
                    string[] valores = primeiro.Split('@');
                    set[i, 0] = Convert.ToDouble(valores[0]);
                    set[i, 1] = Convert.ToDouble(valores[1]);
                    i++;

                    size++;
                }

            }

            leitor.Close();
            entrada.Close();

        }

    }
}

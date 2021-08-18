using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace NeurounThree
{
    class RecognitionLayer
    {
        List<Neuron> layerNeuron;
        List<bool> R;
        bool Rflag = false;
        public List<bool> check;
        public int countNeuron => layerNeuron.Count;

        public RecognitionLayer()
        {
            layerNeuron = new List<Neuron>();
            R = new List<bool>();
            check = new List<bool>();

            layerNeuron.Add(new Neuron());
            R.Add(false);
            check.Add(false);
        }
        public void Print()
        {
            Console.WriteLine($"Кол-во нейронов : {countNeuron}");
            for (int i = 0; i < countNeuron; i++)
            {
                
                Console.WriteLine($"\nНомер нейрона: {i}");
                Console.Write("\nBk :\n\n№|");
                for (int j = 0; j < Topology.size; j++)
                {
                    Console.Write($"\t{j}  ");
                }
                Console.WriteLine();
                for (int j = 0; j < Topology.size; j++)
                {
                    Console.Write("---------");
                }
                Console.WriteLine();
                layerNeuron[i].PrintB();

                Console.Write("\nTk :\n\n№|");
                for (int j = 0; j < Topology.size; j++)
                {
                    Console.Write($"\t{j}  ");
                }
                Console.WriteLine();
                for (int j = 0; j < Topology.size; j++)
                {
                    Console.Write("---------");
                }
                Console.WriteLine();
                layerNeuron[i].PrintT();

            }

            for (int n = 0; n < countNeuron; n++)
            {
                Bitmap bitmap = new Bitmap(Topology.col, Topology.size/ Topology.col);
                
                for (int i = 0; i < Topology.size / Topology.col; i++)
                {
                    for (int j = 0; j < Topology.col; j++)
                    {
                        Color color = layerNeuron[n].Tk[i * Topology.col + j] == true ? Color.Black : Color.White;
                        bitmap.SetPixel(j,i,color);
                    }
                }
                bitmap.Save($"Images/ImageNeuron{n}.png",ImageFormat.Png);
       
            }
            Console.Read();
            
        }
        public void ClearR()
        {
            for (int i = 0; i < R.Count; i++)
            {
                R[i] = false;
            }
        }
        public int CalcActivationNeurons(bool[] C)
        {
            double max = double.MinValue;
            int numberNeuronActivation = -1;
            double currentS;
            for (int i = 0; i < layerNeuron.Count; i++)
            {
                if (check[i]==true)
                {
                    continue;
                }
                currentS = ReturnS(i, C);
                
                if (currentS>=Topology.threshold && currentS>max)
                {
                    max = currentS;
                    numberNeuronActivation = i;
                }
                
            }
            if (numberNeuronActivation!=-1)
            {
                R[numberNeuronActivation] = true;
                Rflag = true;
                return numberNeuronActivation;
            }
            Rflag = false;
            return -1;
            
        }
        public void AddNewNeuron(bool[] C)
        {
            double[] Bk = new double[Topology.size];
            double sum = 0.0;
            for (int i = 0; i < Topology.size; i++)
            {
                sum += Convert.ToInt32(C[i]);
            }
            for (int i = 0; i < Topology.size; i++)
            {
                Bk[i] = Topology.L * Convert.ToInt32(C[i])/(Topology.L-1+sum);
            }
            layerNeuron.Add(new Neuron(Bk,C));
            R.Add(false);
            check.Add(false);
        }
        public void Study(bool[] C,int number)
        {
            double sum = 0.0;
            for (int i = 0; i < Topology.size; i++)
            {
                sum += Convert.ToInt32(C[i]);
            }
            for (int i = 0; i < Topology.size; i++)
            {
                layerNeuron[number].Bk[i] = Topology.L * Convert.ToInt32(C[i]) / (Topology.L - 1 + sum);
                layerNeuron[number].Tk[i] = C[i];
            }
        }
        public double ReturnS(int i,bool[] C)
        {
            double S=0.0;
            for (int j = 0; j < Topology.size; j++)
            {
                S += layerNeuron[i].Bk[j] * Convert.ToInt32(C[j]);
            }
            return S;
        }
        public bool ReturnR()
        {
            return Rflag;
        }
        public bool SetR(int i,bool flag)
        {
            return R[i]=flag;
        }
        public bool[] ReturnP(int k)
        {
            bool[] P = new bool[Topology.size];

            for (int i = 0; i < Topology.size; i++)
            {
                P[i] = R[k] && layerNeuron[k].Tk[i];
            }
            return P;
        }
        
    }
}

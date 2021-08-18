using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeurounThree
{
    class Neuron
    {
        public double[] Bk;
        public bool[] Tk;
        public Neuron()
        {
            Bk = new double[Topology.size];
            Tk = new bool[Topology.size];
            
            for (int i = 0; i < Topology.size; i++)
            {
                Bk[i] = (Topology.L / (Topology.L-1.0+Topology.size))/2.0;
                Tk[i] = true;
            }
        }
        public void PrintB()
        {
            for (int i = 0; i < Topology.size; i++)
            {
                Console.Write($"\t{Bk[i]:f3}");
            }
            Console.WriteLine("");
        }
        public void PrintT()
        {
            for (int i = 0; i < Topology.size; i++)
            {
                Console.Write($"\t{Tk[i]} ");
            }
            Console.WriteLine("");
        }
        public Neuron(double[] Bk,bool[] Tk)
        {
            this.Bk = Bk;
            this.Tk = Tk;
        }

    }
}
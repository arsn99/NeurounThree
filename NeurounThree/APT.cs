using System;
using System.Collections.Generic;

namespace NeurounThree
{
    class APT
    {
        
        public APT(List<bool[]> images)
        {
            RecognitionLayer recognitionLayer = new RecognitionLayer();
            int numberNeuron = 0;
            bool[] C;
            for (int i = 0; i < images.Count; i++)
            {
                numberNeuron = 0;
                bool trigger = true;
                for (int j = 0; j < recognitionLayer.check.Count; j++)
                {
                    recognitionLayer.check[j] = false;
                }
                recognitionLayer.ClearR();
                while (true)
                {
                    if (trigger)
                    {
                        C = new bool[Topology.size];
                        Array.Copy(images[i], C, Topology.size);
                    }
                    else
                    {
                        C = Comparison(images[i], recognitionLayer.ReturnP(numberNeuron), Receiver1(images[i], recognitionLayer.ReturnR()));
                        if (!CheckP(images[i], C))
                        {
                            Array.Copy(images[i], C, Topology.size);
                            recognitionLayer.SetR(numberNeuron, false);
                            recognitionLayer.check[numberNeuron] = true;
                            numberNeuron = -1;
                        }
                        else
                            break;
                    }
                    
                    numberNeuron = recognitionLayer.CalcActivationNeurons(C);
                    trigger = false;
                    if (numberNeuron==-1)
                    {
                        break;
                    }
                }
                if (numberNeuron == -1)
                {
                    recognitionLayer.AddNewNeuron(C);
                    numberNeuron = recognitionLayer.countNeuron - 1;
                }
                recognitionLayer.Study(C, numberNeuron);

            }
            recognitionLayer.Print();


        }

        bool CheckP(bool[] X, bool[]C)
        {
            int count = 0;
            for (int i = 0; i < Topology.size; i++)
            {
                if (X[i]==C[i])
                {
                    count++;
                }
            }
            double p = (double)count / Topology.size;
            if (p<Topology.p)
            {
                return false;
            }
            return true;
        }
        public bool[] Comparison(bool[] X, bool[] P, bool G)
        {
            bool[] bitArrayOut = new bool[Topology.size];
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] && P[i] || X[i] && G || P[i] && G)
                    bitArrayOut[i] = true;
                else
                    bitArrayOut[i] = false;
            }
            return bitArrayOut;
        }
        public bool Receiver1(bool[] X, bool R)
        {
            if (!R)
            {
                return Receiver2(X);
            }
            return false;
        }
        public bool Receiver2(bool[] X)
        {
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}

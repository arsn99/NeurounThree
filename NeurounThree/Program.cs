using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace NeurounThree
{
    class Program
    {
        public static bool[]GetImage(Bitmap bitmap)
        {
            bool[] imageBool = new bool[Topology.size];
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    imageBool[i * bitmap.Width + j] = bitmap.GetPixel(j, i).ToArgb() == Color.Black.ToArgb() ? true : false;
                }
            }
            return imageBool;
        }
        static void Main(string[] args)
        {
            List<bool[]> images = new List<bool[]>();
            ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry item in resourceSet.OfType<DictionaryEntry>().OrderBy(i =>i.Key))
            {
                images.Add(GetImage((Bitmap)item.Value));
            }
            APT pT = new APT(images);
        }
    }
}

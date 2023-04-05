using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor
{
    public static class Helper
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            var rng = new Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

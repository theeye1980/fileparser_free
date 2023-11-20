using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.DedicClasses
{
    public static class array_searcher
    {
        public static int[] ArrInArrSearcher(string[] searchArray, string[] lookArray, int startIndex = 0)
        {
            List<int> finds = new List<int>();
            foreach (string needle in searchArray)
            {
                foreach (string look in lookArray)
                {
                    if (look.Contains(needle))
                    {

                        int index = Array.IndexOf(lookArray, look, startIndex);

                        if (index == -1)
                        {
                            //MessageBox.Show("Вот!");
                        }
                        else
                        {
                            finds.Add(index);
                            startIndex = index + 1;
                        }
                    }

                }
            }
            int[] output = finds.ToArray();
            return output;
        }
        public static int StrInArrSearcher(string[] searchArray, string needle, int startIndex = 0)
        {
            int index = Array.IndexOf(searchArray, needle, startIndex);

            if (index == -1)
            {
                startIndex = index;
            }
            else
            {

                startIndex = index + 1;
                if (startIndex >= searchArray.Length) startIndex = -1;
                //StrInArrSearcher(searchArray, needle, startIndex);
            }

            return startIndex;
        }
    }
}

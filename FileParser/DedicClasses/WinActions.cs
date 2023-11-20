using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser.DedicClasses
{
    public static class WinActions
    {

        public static void OpenRes(string path)
        {
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n,/select,{path}" });
        }
    }
}

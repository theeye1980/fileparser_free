using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParser.DedicClasses
{
    public static class FileInfoGetter
    {

        public static string GetFileChangeDate(string filepath)
        { // возвращает дату последнего изменения файла в виде строки

            DateTime dt = File.GetLastWriteTime(filepath);
            string result = dt.ToString();
            return result;
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace FileParser.DedicClasses
{
    public class Converters
    {
        public string[] numbers2 = {
            "_1",
            "_2",
            "_3",
            "_4",
            "_5",
            "_6",
            "_7",
            "_8",
            "_9"
        };
        public string[] numbers3 = {
            "_10",
            "_11",
            "_12",
            "_13",
            "_14",
            "_15",
            "_16",
            "_17",
            "_18",
            "_19",
            "_20",
            "_21",
            "_22",
            "_23",
            "_24",
            "_25",
        };
        public string[] suffix = {
            "_3d",
            "_ies_ldt",
            "_cb",
            "_mcl",
            "_ibl",
            "_drawing",
            "_drawing",
            "_scheme",
            "_ai",
            "_manual",
            "_interior",
            "_video",
            ".png",
            ".rar",
            ".pdf",
            ".jpg",
            ".svg",
            ".mp4"

        };
        public static string GetFileNameFromArt(string article) // Метод преобразования артикула в допустимое имя файла в файловой системе
        {
            article = article.Replace("/", "~");
            article = article.Replace("\\", "~");
            article = article.Replace(":", "~");
            article = article.Replace("*", "~");
            article = article.Replace("?", "~");
            article = article.Replace("\"", "~");
            article = article.Replace("<", "~");
            article = article.Replace(">", "~");
            article = article.Replace("|", "~");

            return article;
        }
        public static string[] GetArtsFromFileName(string article) // Метод преобразования файл в потенциальные имена
        {
            List<string> arts = new List<string>();

            arts.Add(article.Replace("~", "/"));
            arts.Add(article.Replace("~", "\\"));
            arts.Add(article.Replace("~", ":"));
            arts.Add(article.Replace("~", "*"));
            arts.Add(article.Replace("~", "?"));
            arts.Add(article.Replace("~", "\""));
            arts.Add(article.Replace("~", "<"));
            arts.Add(article.Replace("~", ">"));
            arts.Add(article.Replace("~", "|"));

            string[] out_res = arts.ToArray();
            return out_res;
        }
        public bool contain_suffix(string Filer)
        {
            bool outer = false;
            //вырезаем номер последних 2 знаков
            foreach (string part in this.numbers2)
            {

                if (Filer.Contains(part))
                {

                    outer = true;

                }

            }
            return outer;
        }

        public string Clean_suffix(string input)
        {
            string f_no_suff_ext = input;
            //если находится суффикс, то с этого момента режем
            foreach (string part in suffix)
            {

                if (f_no_suff_ext.Contains(part))
                {
                    //находим позицию вхождения подстроки
                    int ind = f_no_suff_ext.IndexOf(part);
                    //и записываем в f_no_suff_ext левую часть f
                    f_no_suff_ext = f_no_suff_ext.Substring(0, ind);
                }

            }
            //вырезаем номер последних 3 знаков
            foreach (string part in numbers3)
            {

                if (f_no_suff_ext.Contains(part))
                {
                    //находим позицию вхождения подстроки
                    int ind = f_no_suff_ext.IndexOf(part);
                    if (input.Length - ind == 3)
                    {
                        //и записываем в f_no_suff_ext левую часть f
                        f_no_suff_ext = f_no_suff_ext.Substring(0, ind);
                    }


                }

            }
            //вырезаем номер последних 2 знаков
            foreach (string part in numbers2)
            {

                if (f_no_suff_ext.Contains(part))
                {
                    //находим позицию вхождения подстроки
                    int ind = f_no_suff_ext.IndexOf(part);
                    if (f_no_suff_ext.Length - ind == 2)
                    {
                        //и записываем в f_no_suff_ext левую часть f
                        f_no_suff_ext = f_no_suff_ext.Substring(0, ind);
                    }


                }

            }

            return f_no_suff_ext;
        }

        public string urlencodeToUTF(string input) {
            string output = "";



            return output;
        }
        public static void jpgToPng(string folderPath) {

            // Create a new folder for the PNG files
            string pngFolderPath = Path.Combine(folderPath, "folder_png");
            Directory.CreateDirectory(pngFolderPath);

            // Get all the JPEG files in the folder
            string[] jpegFiles = Directory.GetFiles(folderPath, "*.jpg");

            foreach (string jpegFilePath in jpegFiles)
            {
                // Load the JPEG image
                using (Image image = Image.FromFile(jpegFilePath))
                {
                    // Calculate the new size while maintaining the aspect ratio
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = 300;
                        height = (int)(image.Height * (300.0 / image.Width));
                    }
                    else
                    {
                        height = 300;
                        width = (int)(image.Width * (300.0 / image.Height));
                    }

                    // Create a new bitmap with a white background and the new size
                    using (Bitmap bitmap = new Bitmap(300, 300))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(Color.White);

                            // Calculate the coordinates to center the image
                            int x = (300 - width) / 2;
                            int y = (300 - height) / 2;

                            // Draw the JPEG image onto the bitmap
                            graphics.DrawImage(image, x, y, width, height);
                        }

                        // Save the bitmap as a PNG file with the same name in the new folder
                        string pngFilePath = Path.Combine(pngFolderPath, Path.GetFileNameWithoutExtension(jpegFilePath) + ".png");
                        bitmap.Save(pngFilePath, ImageFormat.Png);
                    }
                }
            }



        }
        public static void jpgTo224(string jpegFilePath)
        {

            // Create a new folder for the PNG files
            string miniphotobank_path = Properties.Settings.Default.miniphotobank_path;
           
            
                // Load the JPEG image
                using (Image image = Image.FromFile(jpegFilePath))
                {
                    // Calculate the new size while maintaining the aspect ratio
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = 224;
                        height = (int)(image.Height * (224.0 / image.Width));
                    }
                    else
                    {
                        height = 224;
                        width = (int)(image.Width * (224.0 / image.Height));
                    }

                    // Create a new bitmap with a white background and the new size
                    using (Bitmap bitmap = new Bitmap(224, 224))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(Color.White);

                            // Calculate the coordinates to center the image
                            int x = (224 - width) / 2;
                            int y = (224 - height) / 2;

                            // Draw the JPEG image onto the bitmap
                            graphics.DrawImage(image, x, y, width, height);
                        }

                        // Save the bitmap as a PNG file with the same name in the new folder
                        string pngFilePath = Path.Combine(miniphotobank_path, Path.GetFileNameWithoutExtension(jpegFilePath) + ".jpg");
                        bitmap.Save(pngFilePath, ImageFormat.Jpeg);
                    }
                }
            



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveM_Car_Packer
{
    public class Car
    {
        public string route { get; set; }
        public string[] files { get; set; }

        private Dictionary<string, bool> extensions = new Dictionary<string, bool>()
        {
            {".yft", false},
            {".ytd", false},
            {".meta", true },
        }; // true goes to data and false goes to stream

        public string DirectoryOutput;

        public void ParseData()
        {
            string dirname = Path.GetFileName(route);
            string diroutput = DirectoryOutput + @"\data\" + dirname;
            string streamfile = DirectoryOutput + @"\stream\";
            Directory.CreateDirectory(diroutput);
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (extension != ".lua")
                {
                    if (extensions.ContainsKey(extension))
                    {
                        ColorConsole.WriteLine("Packing file ", file.Green());
                        if (extensions[extension])
                        {
                            File.Move(file, diroutput + @"\" + Path.GetFileName(file));
                        } else
                        {
                            File.Move(file, streamfile + @"\" + Path.GetFileName(file));
                        }
                    } else
                    {
                        ColorConsole.WriteLine("Unknown file extension ", extension.Red());
                    }
                    
                }
            }
        }
    }
}

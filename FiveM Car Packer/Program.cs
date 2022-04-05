global using ColoredConsole;
global using System.IO;
using System;
using System.Threading;

namespace FiveM_Car_Packer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Car packer by guillerp :)");
            Thread.Sleep(1000);
            Console.WriteLine("Introduce your car folder route:");
            string data = Console.ReadLine();
            ColorConsole.WriteLine("Introduce the resource name: ", "Ex.(FiveMCars)".Green());
            string dirname = Console.ReadLine();
            ColorConsole.WriteLine("Confirm that you did a security copy of your cars! ".Yellow(), "Write 'yes' to confirm!".Green());
            string confirm = Console.ReadLine();
            if (confirm != "yes") return;
            if (Directory.Exists(data))
            {
                string[] directories = {data + @"\" + dirname, data + @"\" + dirname + @"\" + "stream", data + @"\" + dirname + @"\" + "data" };
                string[] files = {data + "/" + dirname + "/fxmanifest.lua"};
                foreach (string directory in directories)
                {
                    if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
                }
                foreach (string file in files)
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, "fx_version 'cerulean'\ngame 'gta5'\nauthor 'FiveM Car Packer by guillerp' \ndescription 'An authomatic car pack generated'\nfiles { 'data/**/*.meta' }  ");
                }
                
                ColorConsole.WriteLine("Packing cars inside ", data.Yellow());
                ColorConsole.WriteLine("Getting cars...");
                foreach(string subdirectory in Directory.GetDirectories(data))
                {
                    if (Path.GetFileName(subdirectory) != dirname)
                    {
                        Car car = new Car
                        {
                            route = subdirectory,
                            files = Directory.GetFiles(subdirectory),
                            DirectoryOutput = directories[0]
                        };
                        car.ParseData();
                    }
                }
            } else
            {
                ColorConsole.WriteLine("The directoy does not exist, try again with a valid directory".Red());
            }
        }
    }
}

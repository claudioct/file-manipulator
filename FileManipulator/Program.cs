using FileManipulator.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManipulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rootFolder = args[0];

            DirectoryInfo rootFolderInfo = new DirectoryInfo(rootFolder);
            if (rootFolderInfo.Exists == false)
            {
                Console.WriteLine("directory does not exist, the app will now close, sorry ...");
                Console.Read();
            }

            var files = rootFolderInfo.GetFiles("*.token", SearchOption.TopDirectoryOnly);
            if (files.Count() == 0)
            {
                Console.WriteLine("No files found are eligible for processing...");
                Console.Read();
            }

            foreach (var file in files)
            {
                string filePath = file.FullName;
                File.SetAttributes(file.FullName, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
                string fileContent = File.ReadAllText(filePath);
                if (fileContent.Contains("foo"))
                {
                    fileContent = fileContent.Replace("foo", "bar");
                }
                
                System.IO.File.WriteAllText(filePath.Replace(".token", ""), fileContent);
                file.Delete();
                Debug.WriteLine($"{file.FullName} processed");
            }
        }
    }
}

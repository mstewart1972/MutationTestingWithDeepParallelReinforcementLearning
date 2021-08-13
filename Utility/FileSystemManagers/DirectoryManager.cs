using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.PathProviders;

namespace Utility.FileSystemManagers
{
    public class DirectoryManager:IDirectoryManager
    {
        private const string REPORT_TEMPLATE_FOLDER = "template";

        public void CopyFile(string sourceFile, string destFile, bool force = false)
        {
            File.Copy(sourceFile, destFile,  force);
        }

        public void CleanDirectory(String directoryPath)
        {


                //delete all generated folder
                string[] generatedDirectories = Directory.GetDirectories(directoryPath);
                string[] generatedFiles = null;
                foreach (var item in generatedDirectories)
                {
                    //not delete template folder
                    if (!item.Contains(REPORT_TEMPLATE_FOLDER))
                    {
                        generatedFiles = Directory.GetFiles(item);
                        if (generatedFiles != null)
                        {
                            foreach (var eachFile in generatedFiles)
                            {
                                File.Delete(eachFile);
                            }
                        }
                    }

                }


        }

        public void DeleteAndRecreateDirectory(String directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }

            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("The directory process failed: {0}", e.ToString());
                Thread.Sleep(2000);
                // MAS 20210216 - added sleep to prevent sporadic failures
            }
            finally
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        // MAS 20210216
        public void DeleteDirectory(String directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Thread.Sleep(2000);
                    Directory.Delete(directoryPath, true);
                }
            }
            catch (Exception e)
            {
                if (Directory.Exists(directoryPath))
                {
                    Thread.Sleep(2000);
                    Directory.Delete(directoryPath, true);
                }
                //Console.WriteLine("The delete directory process failed: {0}", e.ToString());
            }
            //finally
            //{
            //    if (Directory.Exists(directoryPath))
            //    {
            //        Directory.Delete(directoryPath, true);
            //    }
            //}
        }

        public void CreateDirectory(String directoryPath)
        {
            try
            {
                Thread.Sleep(2000);
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                Thread.Sleep(2000);
                Directory.CreateDirectory(directoryPath);
                //Console.WriteLine("The create directory process failed: {0}", e.ToString());
            }
            //finally
            //{
            //    Directory.CreateDirectory(directoryPath);
            //}
        }

        public void CopyDirectory(String directoryToBackup, String destinationDirectory)
        {
            foreach (var file in Directory.GetFiles(directoryToBackup))
            {
                File.Copy(file, Path.Combine(destinationDirectory, Path.GetFileName(file)), true);

            }
        }
    }
}

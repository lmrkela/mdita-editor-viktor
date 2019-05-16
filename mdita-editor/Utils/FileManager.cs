using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mDitaEditor.Utils
{
    static class FileManager
    {
        public static void CreateFile(string path)
        {
            for (int i = 0; ; ++i)
            {
                try
                {
                    File.Create(path);
                    return;
                }
                catch (Exception ex)
                {
                    if (i > 100)
                    {
                        throw ex;
                    }
                }
            }
        }
        public static void DeleteFile(string path)
        {
            for (int i = 0; ; ++i)
            {
                try
                {
                    File.Delete(path);
                    return;
                }
                catch (Exception ex)
                {
                    if (i > 100)
                    {
                        throw ex;
                    }
                }
            }
        }
        public static void CreateDir(string path)
        {
            for (int i = 0; ; ++i)
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return;
                }
                catch (Exception ex)
                {
                    if (i > 100)
                    {
                        throw ex;
                    }
                }
            }
        }
        public static void DeleteDir(string path, bool recursive = false)
        {
            for (int i = 0; ; ++i)
            {
                try
                {
                    Directory.Delete(path, recursive);
                    return;
                }
                catch (Exception ex)
                {
                    if (i > 100)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EtnaSoft.Dal.Services.Converter
{
    public class FileConverter : IDisposable
    {
        public bool Save(string file, string input)
        {
            bool succes = false;
            try
            {
                byte[] array = Encoding.UTF8.GetBytes(input);
                using (var fs = new FileStream(file + "\\Database.data", FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(array, 0, array.Length);
                    succes = true;
                }
            }
            catch
            {
                succes = false;
                throw;
            }

            return succes;
        }

        public string Load(string file)
        {
            string scripts = "";

            try
            {
                var byteArray = File.ReadAllBytes(file);
                scripts = Encoding.UTF8.GetString(byteArray);
            }
            catch
            {
                throw;
            }

            return scripts;
        }

        public void Dispose()
        {
        }
    }
}

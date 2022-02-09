using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using File = System.IO.File;
using FileStyleUriParser = System.FileStyleUriParser;


/// <summary>
/// Encrypting
/// </summary>

namespace EtnaSoft.Dal.Services.Authorization
{
    public static class EncryptDecrypt
    {
        public static void EncryptSettings()
        {
            var file = File.ReadAllText(Directory.GetCurrentDirectory() + "\\App.xaml");
            
        }
    }
}

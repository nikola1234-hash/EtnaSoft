using System;
using System.Security.Cryptography;
using System.Text;

namespace EtnaSoft.Bll.BulkSms.Helpers
{
    public sealed class Protector
    {
        public static string Protect(string value)
        {

            return Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null,
                DataProtectionScope.LocalMachine));
        }

        public static string Unprotect(string value)
        {
            return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(value), null,
                DataProtectionScope.LocalMachine));
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace dominio.mdm.Helper
{
    public static class Extensions
    {

        public static string SerializeJson(this object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static object DeSerializeJson(this string convertido)
        {
            return JsonConvert.DeserializeObject<object>(convertido);
        }

        public static T DeSerializeJson<T>(this string convertido)
        {
            return JsonConvert.DeserializeObject<T>(convertido);
        }


        public static string Compress(this string uncompressedString)
        {
            byte[] compressedBytes;

            using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString)))
            {
                using (var compressedStream = new MemoryStream())
                {
                    using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                    {
                        uncompressedStream.CopyTo(compressorStream);
                    }

                    compressedBytes = compressedStream.ToArray();
                }
            }

            return Convert.ToBase64String(compressedBytes);
        }

        public static string Decompress(this string compressedString)
        {
            byte[] decompressedBytes;

            var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(decompressedStream);

                    decompressedBytes = decompressedStream.ToArray();
                }
            }

            return Encoding.UTF8.GetString(decompressedBytes);
        }

        public static bool ValidarEmail(this string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
            {
                try
                {
                    string hostname = email.Split('@').ToArray()[1].ToString();

                    IPAddress[] ipaddress = Dns.GetHostAddresses(hostname);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
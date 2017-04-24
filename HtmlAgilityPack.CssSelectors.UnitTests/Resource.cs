using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.UnitTests
{
    internal class Resource
    {
        private static Dictionary<string, byte[]> s_Cache = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

        public static string GetString(string name)
        {
            return Encoding.UTF8.GetString(GetBytes(name));
        }

        public static byte[] GetBytes(string name)
        {

            if (s_Cache.TryGetValue(name, out byte[] data))
                return data;

            var stream = new FileStream(name, FileMode.Open);

            if (stream == null)
                throw new InvalidOperationException("Stream não encontrado: " + name);

            var ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            data = ms.ToArray();
            s_Cache.Add(name, data);
            return data;
        }
    }
}

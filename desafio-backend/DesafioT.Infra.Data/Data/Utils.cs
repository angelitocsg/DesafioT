using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioT.Infra.Data.Data
{
    public class Utils
    {
        public static string ReadFile(string file)
        {
            string ret = string.Empty;

            using (StreamReader sr = new StreamReader(file, Encoding.Default))
            {
                ret = sr.ReadToEnd();
            }

            return ret;
        }
    }
}

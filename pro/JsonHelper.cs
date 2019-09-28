using Newtonsoft.Json;
using pro.models;
using System;
using System.IO;
using System.Text;

namespace pro
{
    public static class JsonHelper
    {
        public static void Export(Data data)
        {
            string path = @"d:\pro\" + "pro.json";
            path = string.Join("", path.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
         
            try
            {

                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    string json = JsonConvert.SerializeObject(data);
                    Byte[] info = new UTF8Encoding(true).GetBytes(json);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static Data Import(string path)
        {
            Data data;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<Data>(json);
            }
            return data;
        }
    }
}

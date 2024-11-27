using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;

namespace WMSMiniWebService.Components
{
    public class Log
    {
        public void WriteToLog(string message)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/");
            Console.WriteLine(message);

            try
            {
                if (!File.Exists(filepath + "\\logfile.txt"))
                {


                    using (FileStream fs = File.Create(filepath + "\\logfile.txt"))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Log File Created  ");
                        fs.Write(info, 0, info.Length);
                    }
                    using (StreamReader sr = File.OpenText(filepath + "\\logfile.txt"))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }

                }

                // Compose a string that consists of three lines.

                string lines = DateTime.Now.ToString() + " - " + message;
                // Write the string to a file.


                FileInfo f = new FileInfo(filepath + "\\logfile.txt");
                long s1 = f.Length;


                if (s1 > 5000)
                {
                    try
                    {
                        File.WriteAllText(filepath + "\\logfile.txt", String.Empty);
                    }
                    catch { }
                }
                TextWriter tsw = new StreamWriter(filepath + "\\logfile.txt", true);


                tsw.WriteLine(lines);
                tsw.Close();



            }
            catch
            {


            }

        }
    }

}

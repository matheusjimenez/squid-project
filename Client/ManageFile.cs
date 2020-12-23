using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Client
{
    public class AcessLog
    {
        public string Number { get; set; }
        public string RandomNumber { get; set; }
        public string Ipv6 { get; set; }
        public string Protocol { get; set; }
        public string Port { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Test { get; set; }
        public string Type { get; set; }

    }

    public class IData
    {
        public List<AcessLog> AcessLogs { get; set; }
        public TimeSpan ElapsedTime { get; set; }

    }
    class ManageFile
    {
        public IData ReadFile()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Burned\Downloads\access.log");
            List<AcessLog> data = new List<AcessLog>();
            foreach (string line in lines)
            {
                string[] subs = line.Split(" ");

                var test = new AcessLog()
                {
                    Number = subs[0].ToString(),
                    RandomNumber = subs[1].ToString(),
                    Ipv6 = subs[2].ToString(),
                    Protocol = subs[3].ToString(),
                    Port = subs[4].ToString(),
                    Method = subs[5].ToString(),
                    Url = subs[6].ToString(),
                    Test = subs[8].ToString(),
                    Type = subs[9].ToString()
                };

                data.Add(test);
            }
            stopwatch.Stop();
            Console.WriteLine($"Tempo passado: {stopwatch.Elapsed}");
            System.Console.WriteLine("File contains {0}", lines.Length);

            return new IData() { AcessLogs = data, ElapsedTime = stopwatch.Elapsed };
        }
    }
}

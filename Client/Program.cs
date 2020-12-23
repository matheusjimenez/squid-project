using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client
{

    class Program
    {
        static void Main(string[] args)
        {
            ManageFile file = new ManageFile();
            HandleDataToSend dataToSend = new HandleDataToSend();

            var data = file.ReadFile();
            List<AcessLog> logList = data.AcessLogs;
            foreach (AcessLog log in logList)
            {
                dataToSend.PrepareData(JsonSerializer.Serialize(log));
            }

        }
    }
}

using Server.Models;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            TCPServer tcp = new TCPServer();

            tcp.EstabelecerConexao();
            dbConnection.HandleConnection();
        }
    }
}

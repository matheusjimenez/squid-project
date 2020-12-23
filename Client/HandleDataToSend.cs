using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class HandleDataToSend
    {
        public static void SendFile(string IPHostRemoto, int PortaHostRemoto, string nomeCaminhoArquivo, string nomeAbreviadoArquivo)
        {
            var allText = System.IO.File.ReadAllLines(@"C:\Users\Burned\Downloads\access.log");

            for (var i = 0; i < allText.Length; i++)
            {
                try
                {
                    if (!string.IsNullOrEmpty(IPHostRemoto))
                    {
                        byte[] fileNameByte = Encoding.ASCII.GetBytes(nomeAbreviadoArquivo);
                        //byte[] fileData = File.ReadAllBytes(nomeCaminhoArquivo);
                        byte[] fileData = Encoding.ASCII.GetBytes(nomeCaminhoArquivo);
                        byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                        byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                        //
                        fileNameLen.CopyTo(clientData, 0);
                        fileNameByte.CopyTo(clientData, 4);
                        fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                        //
                        TcpClient clientSocket = new TcpClient(IPHostRemoto, PortaHostRemoto);
                        NetworkStream networkStream = clientSocket.GetStream();
                        //
                        networkStream.Write(clientData, 0, clientData.GetLength(0));
                        networkStream.Close();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }


       public void PrepareData(string jsonData)
        {
            string enderecoIP = "127.0.0.1";
            int porta = 1555;
            string nomeArquivo = jsonData;
            string nomeAbreviadoArquivo = "access.log";
            
            try
            {
                Task.Factory.StartNew(() => SendFile(enderecoIP, porta, nomeArquivo, nomeAbreviadoArquivo));
                Console.WriteLine("Arquivo Enviado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro : " + ex.Message);
            }
            Console.ReadLine();
        }

    }
}

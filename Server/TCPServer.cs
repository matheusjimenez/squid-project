using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Server.Models;
using System.Text.Json;

namespace Server
{
    class TCPServer
    {
        public void EstabelecerConexao()
        {
            int porta = int.Parse("1555");
            string endIP = "127.0.0.1";
            try
            {
                Task.Factory.StartNew(() => TratamentoArquivoRecebido(porta, endIP));
                Console.WriteLine("Escutando na porta...: " + porta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro : " + ex.Message);
            }
        }

        public void TratamentoArquivoRecebido(int porta, string enderecoIp)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(enderecoIp);
                TcpListener tcpListener = new TcpListener(ip, porta);
                tcpListener.Start();
                var textLineCounter = 0;
                while (true)
                {
                    Socket manipularSocket = tcpListener.AcceptSocket();
                    if (manipularSocket.Connected)
                    {
                        string nomeArquivo = string.Empty;
                        NetworkStream networkStream = new NetworkStream(manipularSocket);
                        //int thisRead = 0;
                        int blockSize = 102400;
                        Byte[] dataByte = new Byte[blockSize];
                        List<AcessLog> log = new List<AcessLog>();
                        lock (this)
                        {
                            //string caminhoPastaDestino = @"c:\dados\";
                            manipularSocket.Receive(dataByte);
                            int tamanhoNomeArquivo = BitConverter.ToInt32(dataByte, 0);
                            nomeArquivo = Encoding.ASCII.GetString(dataByte, 4, tamanhoNomeArquivo);
                            //

                            Console.WriteLine(Encoding.ASCII.GetString(dataByte, 4 + tamanhoNomeArquivo, (1024 - (4 + tamanhoNomeArquivo))));
                            textLineCounter++;
                            Console.WriteLine("text line: " + textLineCounter);
                            log.Add(JsonSerializer.Deserialize<AcessLog>(Encoding.ASCII.GetString(dataByte, 4 + tamanhoNomeArquivo, 1024 - (4 + tamanhoNomeArquivo))));
                            
                            if(log.Count == 36299)
                            {
                                DatabaseConnection dc = new DatabaseConnection();
                                dc.WriteOnDataBase(log);
                            }
                        }
                        manipularSocket = null;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

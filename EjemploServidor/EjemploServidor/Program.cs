using System;
using System.Net.Sockets;


namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFromClient = "";
            TcpListener serverSocket = new TcpListener(8888);
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server iniciado");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Conexion exitosa");

            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                    
                    try 
                    {
                        dataFromClient = "";
                        byte[] bytesFrom = new byte[1000000];
                        networkStream.ReadTimeout = 50;

                        //Leer mensaje
                        networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);

                        //Codificar string a ascii
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                        networkStream.Flush();
                        Console.WriteLine(dataFromClient);
                    }
                    catch (Exception ex)
                    {
                        //Esto lo use para debuguear
                        //Console.WriteLine(" >> Esperando mensaje");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> Error de inicio");
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }
    }
}

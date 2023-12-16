using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FightServer
{
    public static class Server
    {
        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverport = 12345;

        public static int ServerPort { 
            get { return serverport; }  
            set { serverport = value; } 
        }
        public static IPAddress ServerIP
        {
            get { return serverIP; }
            set { serverIP = value; }
        }

        static void Main()
        {
            Console.WriteLine("Do you want to enter IP and Port? Y/N");
            bool enterIp = GetUserConfirmation(); // получаем подтверждение на изменение IP с портом
            if (enterIp) GetIpAndPort(); // вводим Ip и порт
            TcpListener server = new(ServerIP, ServerPort);
            try
            {
                // Инициализируем сервер
                server.Start();
                Console.WriteLine("Server is online");
                // Принимаем каждого пользователя в отдельном потоке
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
            }
            finally
            {
                server?.Stop();
            }


            Console.ReadLine();
        }

        public static bool GetUserConfirmation()
        {
            while (true)
            {
                string confirmation = Console.ReadLine()?.ToLower();
                if (!string.IsNullOrWhiteSpace(confirmation))
                {
                    if (confirmation == "y" || confirmation == "yes")
                    {
                        return true;
                    }
                    else if (confirmation == "n" || confirmation == "no")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Неизвестный символ! Повторите ввод:");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Введите непустое значение.");
                }
            }
        }

        public static void GetIpAndPort()
        {
            bool exitBool = false;

            Console.WriteLine("Enter IP and Port like this: '127.0.0.1:12345'");

            while (!exitBool)
            {
                string ipport = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(ipport)) // проверяем на пустоту
                {
                    string[] parts = ipport.Split(':'); // делим для получения пары IP + порт

                    // Парсим оба значения и если всё хорошо заносим в переменные
                    if (parts.Length == 2 
                        && int.TryParse(parts[1], out int port) 
                        && IPAddress.TryParse(parts[0], out IPAddress IPaddr))
                    {
                        ServerIP = IPaddr;
                        ServerPort = port;
                        Console.WriteLine($"IP: {IPaddr}");
                        Console.WriteLine($"Port: {port}");
                        exitBool = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Enter IP and Port in a correct format");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter something.");
                }
            }
        }
        static void HandleClient(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            
            try
            {
                // Получаем сетевые потоки для чтения и записи
                NetworkStream stream = tcpClient.GetStream();

                Console.WriteLine("Received connection from client");
                
                // Читаем данные от клиента
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string receivedData = Encoding.ASCII.GetString(data, 0, bytesRead);

                // Обработка данных (просто сравниваем два массива)
                string[] receivedArrays = receivedData.Split(';');
                double[] array1 = Array.ConvertAll(receivedArrays[0].Split('_'), Double.Parse);
                double[] array2 = Array.ConvertAll(receivedArrays[1].Split('_'), Double.Parse);

                Console.WriteLine("Statistics");
                Console.WriteLine($"Player: {string.Join(" ", array1)}");
                Console.WriteLine($"Enemy: {string.Join(" ", array2)}");

                double response = CallFightFromLib(array1, array2);

                byte[] responseData = BitConverter.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                // Закрываем соединение          
                tcpClient.Close();
                Console.WriteLine("The client has disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error within client: " + ex.Message);
            }
        }

        public static double CallFightFromLib(double[] first, double[] second)
        {
            try
            {
                double result = 0;

                Assembly assembly = Assembly.LoadFrom("FightingLib.dll");

                Object fighting = assembly.CreateInstance("Fighting");
                Type type = assembly.GetType("Fighting");
                MethodInfo method = type.GetMethod("Fight");

                double[][] args = new double[][] { first, second };

                result = (double)method.Invoke(fighting, args);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
                return 0;
            }

        }
    }
}


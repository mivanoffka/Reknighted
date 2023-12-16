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

        static void Main(string[] args)
        {
            Console.WriteLine("Хотите ввести IP и порт? Y/N");
            bool enterIp = GetUserConfirmation();
            if (enterIp) GetIpAndPort();
            try
            {
                TcpListener server = new TcpListener(ServerIP, ServerPort);
                server.Start();
                Console.WriteLine("Сервер запущен");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так {ex}");
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

            Console.WriteLine("Введите IP адрес и порт в таком виде: '127.0.0.1:12345'");

            while (!exitBool)
            {
                string ipport = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(ipport))
                {
                    string[] parts = ipport.Split(':');

                    if (parts.Length == 2 && int.TryParse(parts[1], out int port))
                    {
                        ServerIP = IPAddress.Parse(parts[0]);
                        ServerPort = port;
                        Console.WriteLine($"Введен IP: {parts[0]}");
                        Console.WriteLine($"Введен порт: {port}");
                        exitBool = true;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Введите IP адрес и порт в правильном формате.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Введите непустое значение IP адреса и порта.");
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

                // Читаем данные от клиента
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string receivedData = Encoding.ASCII.GetString(data, 0, bytesRead);

                // Обработка данных (просто сравниваем два массива)
                string[] receivedArrays = receivedData.Split(';');
                string array1 = receivedArrays[0];
                string array2 = receivedArrays[1];

                bool arraysMatch = array1.Equals(array2);

                // Отправляем результат клиенту
                string response = arraysMatch ? "true" : "false";
                byte[] responseData = Encoding.ASCII.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);

                // Закрываем соединение
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при обработке клиента: " + ex.Message);
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
                Console.WriteLine($"Что-то пошло не так: {ex}");
                return 0;
            }

        }
    }
}


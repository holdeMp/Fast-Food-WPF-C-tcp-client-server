using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ipz_fast_food_server
{
    class Program
    {       
        static int port = 8005;
        static void Main(string[] args)
        {// получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                // начинаем прослушивание
                listenSocket.Listen(10);
                DB user = new DB();
                Console.WriteLine("Server started. Waiting for connections...");
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    Console.WriteLine(DateTime.Now.ToShortTimeString()+":"+"Client connected.");
                    //// получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[6200]; // буфер для получаемых данных
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    }
                    while (handler.Available > 0);
                    string s = builder.ToString();
                    string[] subs = s.Split('-');
                    if (subs[0] == "login")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press login ");
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " login: " + subs[1]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " password: " + subs[2]);                       
                        if (user.Login(subs[1], subs[2]))
                        {
                            data = Encoding.Unicode.GetBytes("pass good");
                            handler.Send(data);
                        }
                        else
                        {
                            data = Encoding.Unicode.GetBytes("pass not good");
                            handler.Send(data);                    
                        }
                   
                    }
                    if (subs[0] == "registration")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Name: " + subs[1]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Surname: " + subs[2]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Login: " + subs[3]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Password: " + subs[4]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Email: " + subs[5]);
                        user.AddUser(subs[1], subs[2], subs[3], subs[4], subs[5]);
                    }
                    if (subs[0] == "addorders")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press add");

                        //string name,string surname,string login,string password,string email
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Login: " + subs[1]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Name: " + subs[2]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Description " + subs[3]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Price: " + subs[4]);
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Image: " + subs[5]);
                        user.AddOrders(subs[1], subs[2], subs[3], subs[4], subs[5]);
                    }
                    if (subs[0] == "userorders")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press his orders");
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " Login: " + subs[1]);                       
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.UserOrders(subs[1])));
                        handler.Send(data);
                    }
                    if (subs[0] == "beverages")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press beverages ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.Beverages()));
                        handler.Send(data);
                    }
                    if (subs[0] == "burgers")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press burgers ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.Burgers()));
                        handler.Send(data);
                    }
                    if (subs[0] == "deleteorder")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press deleteorder");
                        user.DropOrders(subs[1]);
                    }
                    if (subs[0] == "chicken")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press chicken ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.Chicken()));
                        handler.Send(data);
                    }
                    if (subs[0] == "confirm")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press confirm ");
                        string message = user.Confirm(subs[1]).ToString();
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                    }
                    if (subs[0] == "ordersconfirm")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press confirm his orders ");
                        user.ConfirmOrders(subs[1]);
                    }
                    if (subs[0] == "search")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press search ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.Search(subs[1])));
                        handler.Send(data);
                    }
                    if (subs[0] == "myorders")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press myorders ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.MyOrders(subs[1])));
                        handler.Send(data);;
                    }
                    if (subs[0] == "tablescheck")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press reserve table ");
                        data = Encoding.Unicode.GetBytes(user.TableCheck(subs[1]));
                        handler.Send(data);
                    }
                    if (subs[0] == "reservetable")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press reservate table ");
                        user.ReserveTable(subs[1], subs[2], subs[3], subs[4]);
                    }
                    if (subs[0] == "seatreserve")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press seatreserve ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.Tables()));
                        handler.Send(data);
                    }
                    if (subs[0] == "reservedtables")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press reservedtables ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.ReservedTables()));
                        handler.Send(data);
                    }
                    if (subs[0] == "myreservedtables")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press myreservedtables ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.MyReservedTables(subs[1])));
                        handler.Send(data);
                    }
                    if (subs[0] == "deletereservation")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press deletereservation");
                        user.DropReservation(subs[1]);
                    }
                    if (subs[0] == "logincheck")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press reserve registration (login check) ");
                        data = Encoding.Unicode.GetBytes(user.LoginCheck(subs[1]));
                        handler.Send(data);
                       
                    }
                    if (subs[0] == "happymeal")
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + " User press happymeal ");
                        data = Encoding.Unicode.GetBytes(user.SerializeTableToString(user.HappyMeal()));
                        handler.Send(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }      
    }
}


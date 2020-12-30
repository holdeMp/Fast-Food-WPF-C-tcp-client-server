using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Xml;

namespace IPZ_fast_food_
{
    class Client
    {
        static int port = 8005; // порт сервера
        static string address = "192.168.0.102"; // адрес сервера
        DataTable dataTable = new DataTable();
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public DataTable Menu(string query)
        {           
            try
            {
                socket.Connect(ipPoint);
                // подключаемся к удаленному хосту
                byte[] data = Encoding.Unicode.GetBytes(query);
                socket.Send(data);
                //// получаем ответ
                data = new byte[16384]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                string message = builder.ToString();
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(message);
                xdoc.Save("menu.xml");
                dataTable.ReadXml("menu.xml");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dataTable;
            }
        }    
        public DataTable Orders(string login,string query)
        {           
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket1.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(query + login);
                socket1.Send(data);
                //// получаем ответ
                data = new byte[16384]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket1.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket1.Available > 0);
                string message = builder.ToString();
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(message);
                xdoc.Save("unconfirmedorders.xml");
                dataTable.ReadXml("unconfirmedorders.xml");
                return dataTable;            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dataTable;
            }
        }
        public void DeleteOrders(string id)
        {
            try
            {
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("deleteorder-" + id );
                socket.Send(data);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string Confirm(string login)
        {
            string summary="";
            try
            {               
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("confirm-" + login);
                socket.Send(data);
                //// получаем ответ
                data = new byte[6144]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                summary = builder.ToString();
                return summary;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return summary;
            }
        }
        public void ConfirmOrder(string login)
        {
            try
            {                
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("ordersconfirm-" + login);
                socket.Send(data);
                MessageBox.Show("Order confirmed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddToOrders(string login, string name, string description, string price, string image)
        {
            try
            {
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("addorders-"+login + "-" + name+ "-"+description + "-"+price + "-"+image);
                socket.Send(data);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool Login(string message, string login, string password)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(message + "-"+ login + "-"+ password);
                socket.Send(data);
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    if (builder.ToString() == "pass good")
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        return true;
                    }
                    else 
                    {
                        MessageBox.Show("Incorrect password or login");
                    }
                }
                while (socket.Available > 0);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void Registration(string message,string name,string surname,string login,string password,string email)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
               
                    byte[] data = Encoding.Unicode.GetBytes(message+"-"+name+"-"+surname+"-"+login+"-"+password+"-"+email);
                    socket.Send(data);
               


                //Console.WriteLine("Send message");


                //// получаем ответ
                //data = new byte[256]; // буфер для ответа
                //StringBuilder builder = new StringBuilder();
                //int bytes = 0; // количество полученных байт

                //do
                //{
                //    bytes = socket.Receive(data, data.Length, 0);
                //    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                //}
                //while (socket.Available > 0);
                //Console.WriteLine("ответ сервера: " + builder.ToString());

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable SearchResult(string query)
        {           
            try
            {               
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("search-" + query);
                socket.Send(data);
                //// получаем ответ
                data = new byte[6144]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                string message = builder.ToString();
                //MessageBox.Show("ответ сервера: " + builder.ToString());
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(message);
                xdoc.Save("searchresult.xml");
                dataTable.ReadXml("searchresult.xml");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return dataTable;
                // закрываем сокет
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dataTable;
            }

        }
        public DataTable MyOrders(string login,string query)
        {          
            try
            {               
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes(query + login);
                socket.Send(data);
                data = new byte[65536]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                string message = builder.ToString();
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(message);
                xdoc.Save("confirmedorders.xml");
                dataTable.ReadXml("confirmedorders.xml");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dataTable;
            }
        }
        public void DeleteReservations(string ProductId)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);

                byte[] data = Encoding.Unicode.GetBytes("deletereservation-" + ProductId);
                socket.Send(data);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string TableCheck(string login) {
            string check = "true";
            try
            {                
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("tablescheck-" + login );
                //byte[] data1 = Encoding.Unicode.GetBytes("reservetable-" + login+"-"+Number+ "-" + AfterDate + "-" + BeforeDate );
                socket.Send(data);
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));                  
                }
                while (socket.Available > 0);
                if (builder.ToString() == "True")
                {
                    MessageBox.Show("You already reserved table");
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return check;
                }
                else
                {
                    check = builder.ToString();
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return check;
            }

        }
        public void ReserveTable(string login,string Number,string AfterDate,string BeforeDate)
        { 
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("reservetable-" + login+"-" + Number + "-" + AfterDate + "-" + BeforeDate + "-");
                socket.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);              
            }
        }
        public string LoginCheck(string login)
        {

            string check = "true";
            try
            {               
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("logincheck-" + login);
                //byte[] data1 = Encoding.Unicode.GetBytes("reservetable-" + login+"-"+Number+ "-" + AfterDate + "-" + BeforeDate );
                socket.Send(data);
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                if (builder.ToString() == "True")
                {
                    MessageBox.Show("Login already registrated");
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return check;
                }
                else
                {
                    check = builder.ToString();
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return check;
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return check;
            }

        }
        public void Connect()
        {
            try
            {                
                socket.Connect(ipPoint);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}

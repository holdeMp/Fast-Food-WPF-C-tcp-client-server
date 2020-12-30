using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace ipz_fast_food_server
{
    class DB
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string SerializeTableToString(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            else
            {
                using (var sw = new StringWriter())
                using (var tw = new XmlTextWriter(sw))
                {
                    // Must set name for serialization to succeed.
                    table.TableName = @"MyTable";

                    // --

                    tw.Formatting = Formatting.Indented;

                    tw.WriteStartDocument();
                    tw.WriteStartElement(@"data");

                    ((IXmlSerializable)table).WriteXml(tw);

                    tw.WriteEndElement();
                    tw.WriteEndDocument();

                    // --

                    tw.Flush();
                    tw.Close();
                    sw.Flush();
                   //Console.WriteLine(sw.ToString());
                    return sw.ToString();
                }
            }
        }
        public DataTable Beverages()
        {
            string sql = "SELECT * FROM Beverages";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable Burgers()
        {
            string sql = "SELECT * FROM Burgers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable Chicken()
        {
            string sql = "SELECT * FROM Chicken";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);               
                return dt;
            }
        }
        public DataTable HappyMeal()
        {
            string sql = "SELECT * FROM HappyMeal";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);               
                return dt;
            }
        }
        public DataTable UserOrders(string login)
        {
            string sql = String.Format("select * from Orders where Login='{0}'",login);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public void DropOrders(string id)
        {
            // название процедуры
            string sqlExpression = "sp_DeleteOrders";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                // добавляем параметр
                command.Parameters.Add(idParam);
                // параметр для ввода возраста     
                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

            }
        }
        public double Confirm(string login)
        {
            double summary = 0.0;
            string sqlExpression = String.Format("UPDATE Orders SET Price=REPLACE(Price,',','.') select sum(cast(Price as float)) as Summary from Orders where Login = '{0}'", login);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    

                    while (reader.Read()) // построчно считываем данные
                    {
                        summary = reader.GetDouble(0);

                        Console.WriteLine("{0} ", summary);
                        
                            reader.Close();
                            var result1 = command.ExecuteNonQuery();
                            return summary;
                        }
                    
                }
                reader.Close();
                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();
                return summary;
            }
        }
        public string TableCheck(string login)
        {
            string pass="false";
            // название процедуры
            string sqlExpression = String.Format("select *from TableChek('{0}')", login);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    

                    while (reader.Read()) // построчно считываем данные
                    {
                        pass = reader.GetString(0);

                        Console.WriteLine("{0} ", pass);
                      
                    }
                }
                reader.Close();
                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();
                return pass;
            }

            // если нам не надо возвращать id
            //var result = command.ExecuteNonQuery();

        }

        public DataTable ReservedTables()
        {
            string sql = String.Format("select * from ReservedTables ");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);               
                return dt;
            }
        }
        public DataTable MyReservedTables(string login)
        {
            string sql = String.Format("select * from ReservedTables where Login='{0}'",login);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);               
                return dt;
            }
        }

        public void AddOrders(string login, string name, string description, string price, string image)
        {
            // название процедуры
            string sqlExpression = "sp_InsertOrders";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                // добавляем параметр
                command.Parameters.Add(loginParam);
                // параметр для ввода возраста
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                command.Parameters.Add(nameParam);
                SqlParameter desciptionParam = new SqlParameter
                {
                    ParameterName = "@desciption",
                    Value = description
                };
                command.Parameters.Add(desciptionParam);
                SqlParameter priceParam = new SqlParameter
                {
                    ParameterName = "@price",
                    Value = price
                };
                command.Parameters.Add(priceParam);
                SqlParameter imageParam = new SqlParameter
                {
                    ParameterName = "@image",
                    Value = image
                };
                command.Parameters.Add(imageParam);

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }
        public void ConfirmOrders(string login)
        {
            // название процедуры
            string sqlExpression = "CopyDataToConfirmedOrders";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                // добавляем параметр
                command.Parameters.Add(loginParam);
                // параметр для ввода возраста
                

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }
        public void AddUser(string name, string surname,string login , string password,string email)
        {
            string sqlExpression = "sp_InsertUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                command.Parameters.Add(nameParam);
                SqlParameter surnameParam = new SqlParameter
                {
                    ParameterName = "@surname",
                    Value = surname
                };
                command.Parameters.Add(surnameParam);
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                command.Parameters.Add(loginParam);
                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password
                };
                command.Parameters.Add(passwordParam);
                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email",
                    Value = email
                };
                command.Parameters.Add(emailParam);

                var result = command.ExecuteScalar();
     
                Console.WriteLine("Succesful registration!", result);
            }
        }
        public bool Login(string login, string password)
        {
            // название процедуры
            string sqlExpression = String.Format("select * from GetPassword('{0}')",login); 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    Console.Write("{0} ", reader.GetName(0));
                    Console.Write("is: ", reader.GetName(0));

                    while (reader.Read()) // построчно считываем данные
                    {
                        string pass = reader.GetString(0);

                        Console.WriteLine("{0} ", pass);
                        if (pass == password)
                        {
                            Console.WriteLine("pass good");
                            reader.Close();
                            var result1 = command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                Console.WriteLine("Incorrect password!");
                reader.Close();
                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();
                return false;
            }
        }
        public DataTable Search(string query)
        {
            // название процедуры
            string sqlExpression = String.Format("select * from getvalues('{0}')", query);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable MyOrders(string login)
        {
            string sql = String.Format("select * from ConfirmedOrders where Login='{0}'", login);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable Tables()
        {
            string sql = String.Format("select * from Tables");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public void DropReservation(string login)
        {
            // название процедуры
            string sqlExpression = "sp_DeleteReservation";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                // добавляем параметр
                command.Parameters.Add(idParam);
                // параметр для ввода возраста



                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

            }
        }

        public void ReserveTable(string login, string number, string AfterDate, string BeforeDate)
        {
            // название процедуры
            string sqlExpression = "sp_ReserveTable";
            string connectionString = @"Data Source=DESKTOP-FM6GLLE\SQL;Initial Catalog=Fast_Food;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = login
                };
                // добавляем параметр
                command.Parameters.Add(loginParam);
                // параметр для ввода возраста
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@Number",
                    Value = number
                };
                command.Parameters.Add(nameParam);
                SqlParameter desciptionParam = new SqlParameter
                {
                    ParameterName = "@AfterDate",
                    Value = AfterDate
                };
                command.Parameters.Add(desciptionParam);
                SqlParameter priceParam = new SqlParameter
                {
                    ParameterName = "@BeforeDate",
                    Value = BeforeDate
                };
                command.Parameters.Add(priceParam);
               

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }
        public string LoginCheck(string login)
        {
            string pass = "false";
            // название процедуры
            string sqlExpression = String.Format("select *from LoginCheck('{0}')", login);
            string connectionString = @"Data Source=DESKTOP-FM6GLLE\SQL;Initial Catalog=Fast_Food;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandText = sqlExpression;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов


                    while (reader.Read()) // построчно считываем данные
                    {
                        pass = reader.GetString(0);

                        Console.WriteLine("{0} ", pass);

                    }
                }
                reader.Close();
                var result = command.ExecuteNonQuery();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();
                return pass;
            }

            // если нам не надо возвращать id
            //var result = command.ExecuteNonQuery();

        }


    }
}

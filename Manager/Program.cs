using System;
using System.Collections.Generic;
using Shared;
using System.Data.SQLite;
using System.Threading;

namespace Manager
{    
    public class DiginoteManager : MarshalByRefObject
    {
        //public event UserLoginHandler userLogin;
        public event FullSaleOrderHandler saleOrder;

        SQLiteConnection m_dbConnection;


        public DiginoteManager()
        {
            Console.WriteLine("Constructor DiginoteManager");
            m_dbConnection = new SQLiteConnection("Data Source=diginotes.db;Version=3;");
            m_dbConnection.Open();
        }

        //if user doesn't exist, return -1
        //if user exists, but password wrong, return -2
        //if user exists and password correct, return 0
        public int Login(string username, string password){

            string sql1 = "select * from user where username = @username";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.Parameters.Add(new SQLiteParameter("@username", username));
            SQLiteDataReader reader1 = command1.ExecuteReader();
            if (! reader1.HasRows){
                Console.WriteLine("Returned -1");
                return -1;
            }
            else
            {
                string sql2 = "select * from user where username = @username and password = @password";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                command2.Parameters.Add(new SQLiteParameter("@username", username));
                command2.Parameters.Add(new SQLiteParameter("@password", password));
                SQLiteDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows){
                    Console.WriteLine("Returned 0");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Returned -2");
                    return -2;
                }
                    
            }

            /*
            if(userLogin != null)
                userLogin(new UserLoggedArgs(username));
             */
        }

        //if register successfull, return 0
        //if username already exists, return -1
        //other error, return -2
        public int Register(string username, string nickname, string password)
        {
            string sql1 = "select * from user where username = @username";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.Parameters.Add(new SQLiteParameter("@username", username));
            SQLiteDataReader reader1 = command1.ExecuteReader();
            if (! reader1.HasRows) //if username doesn't exist
            {
                string sql2 = "insert into user(username, nickname, password) values ( @username, @nickname, @password )";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                command2.Parameters.Add(new SQLiteParameter("@username", username));
                command2.Parameters.Add(new SQLiteParameter("@nickname", nickname));
                command2.Parameters.Add(new SQLiteParameter("@password", password));
                if (command2.ExecuteNonQuery() > 0)
                    return 0;
                else
                    return -2;

            }
            
            return -1;
        }

        public double GetQuotation()
        {
            string sql = "select value from quotation order by date desc limit 1";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetDouble(0);
        }

        public double GetDiginotes(int clientId)
        {
            string sql = "select count(*) from diginote where owner = @clientId";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@clientId", clientId));
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine(reader.GetInt32(0));
            return reader.GetInt32(0);
        }

        //return the number of diginotes that weren't bought
        public int BuyDiginotes(int buyer, int quantity)
        {
            if (quantity <= 0)
                return 0;
            
            int bought_quantity = 0;
            
            //ver se ha esta quantidade a venda
            string sql = "select u.id, quantity from user u, sales_order s where u.sales_order = s.id and quantity > 0 and u.id <> @buyer order by date desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@buyer", buyer));
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int seller = reader.GetInt32(0);
                int quantitySale = reader.GetInt32(1); //quantidade de diginotes a venda para cada ordem de venda
       
                int soldQuantity = quantity > quantitySale ? quantitySale : quantity;

                if (saleOrder != null)
                {
                    Delegate[] invkList = saleOrder.GetInvocationList();

                    foreach (FullSaleOrderHandler handler in invkList)
                    {
                        new Thread(() =>
                        {
                            try
                            {
                                handler(new FullSaleOrderArgs(buyer, seller, soldQuantity));
                                Console.WriteLine("Invoking event handler");
                            }
                            catch (Exception)
                            {
                                saleOrder -= handler;
                                Console.WriteLine("Exception: Removed an event handler");
                            }
                        }).Start();
                    }
                    //saleOrder(new FullSaleOrderArgs(buyer, seller, soldQuantity));
                }
              
                string updateQuantitySeller = "update sales_order set quantity = quantity - @quantity_sold where id = (select sales_order from user where id = @seller)";
                SQLiteCommand command4 = new SQLiteCommand(updateQuantitySeller, m_dbConnection);
                command4.Parameters.Add(new SQLiteParameter("@quantity_sold", soldQuantity));
                command4.Parameters.Add(new SQLiteParameter("@seller", seller));
                command4.ExecuteNonQuery();


                string sql2 = "select serial_number from diginote where owner = @seller";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                command2.Parameters.Add(new SQLiteParameter("@seller", seller));
                SQLiteDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    Console.WriteLine("Editar diginote " + reader2.GetString(0));

                    //fazer update das diginotes (deixam de estar a venda)
                    string updateDiginotesSql = "update diginote set owner = @buyer where serial_number = @serial_number";
                    SQLiteCommand command3 = new SQLiteCommand(updateDiginotesSql, m_dbConnection);
                    command3.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    command3.Parameters.Add(new SQLiteParameter("@serial_number", reader2.GetString(0)));
                    command3.ExecuteNonQuery();
                    bought_quantity++;

                    if (bought_quantity == quantity)
                        return 0;
                }
            }

            //emitente da ordem tem de especificar valor maior ou igual a cotacao atual
            //alertar users da nova cotacao
            if (bought_quantity < quantity)
                return quantity - bought_quantity;

            return 0;
        }

        public int getClientId(string username)
        {
            string sql = "select id from user where username = @username";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@username", username));
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
    }
    /*
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password){
            this.Username = username;
            this.Password = password;
        }
    }
     * */
}

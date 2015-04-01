using System;
using System.Collections.Generic;
using Shared;
using System.Data.SQLite;

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

        public void BuyDiginotes(int buyer, int quantity)
        {
            int bought_quantity = 0;
            
            //ver se ha esta quantidade a venda
            string sql = "select s.id, s.owner, count(*) from diginote d, sales_order s where d.sales_order = s.id group by s.id order by date desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int salesOrderId = reader.GetInt32(0);
                int quantitySale = reader.GetInt32(2); //quantidade de diginotes a venda para cada ordem de venda

                if(bought_quantity + quantitySale > quantity)
                {
                    int neededQuantity = quantitySale - quantity;
                    
                    string sql2 = "select serial_number from diginote where sales_order = @sales_order_id";
                    SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                    command2.Parameters.Add(new SQLiteParameter("@sales_order_id", salesOrderId));
                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Console.WriteLine("Editar diginote " + reader2.GetString(0));

                        //fazer update das diginotes (deixam de estar a venda e mudam de owner)
                        string updateDiginotesSql = "update diginote set sales_order = NULL, owner = @buyer where serial_number = @serial_number";
                        SQLiteCommand command3 = new SQLiteCommand(updateDiginotesSql, m_dbConnection);
                        command3.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                        command3.Parameters.Add(new SQLiteParameter("@serial_number", reader2.GetString(0)));
                        command3.ExecuteNonQuery();
                        
                        bought_quantity++;

                        if (bought_quantity == quantity)
                            break;
                    }
                    
                    //alertar partial sale
                    break;
                }
                else
                {
                    int seller = reader.GetInt32(1);

                    if (saleOrder != null)
                        saleOrder(new FullSaleOrderArgs(buyer, seller, quantitySale));

                    Console.WriteLine("Quantidade a mais");

                    string sql2 = "select serial_number from diginote where sales_order = @sales_order_id";
                    SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                    command2.Parameters.Add(new SQLiteParameter("@sales_order_id", salesOrderId));
                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Console.WriteLine("Editar diginote " + reader2.GetString(0));

                        //fazer update das diginotes (deixam de estar a venda)
                        string updateDiginotesSql = "update diginote set sales_order = NULL, owner = @buyer where serial_number = @serial_number";
                        SQLiteCommand command3 = new SQLiteCommand(updateDiginotesSql, m_dbConnection);
                        command3.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                        command3.Parameters.Add(new SQLiteParameter("@serial_number", reader2.GetString(0)));
                        command3.ExecuteNonQuery();
                        bought_quantity++;
                    }


                    if (bought_quantity == quantity)
                        break;

                    //buscar seller

                    
                    //alertar vendedores
                }
            }

            if (bought_quantity < quantity)
            {
                //emitente da ordem tem de especificar valor maior ou igual a cotacao atual
                //alertar users da nova cotacao
            }
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

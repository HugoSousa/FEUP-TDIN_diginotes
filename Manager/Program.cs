using System;
using System.Collections.Generic;
using Shared;
using System.Data.SQLite;

namespace Manager
{    
    public class DiginoteManager : MarshalByRefObject
    {
        //public event UserLoginHandler userLogin;

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
    }

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
}

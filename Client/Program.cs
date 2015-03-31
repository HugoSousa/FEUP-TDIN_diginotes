using System;
using System.Runtime.Remoting;
using Manager;
using Shared;

namespace Client
{
    class Program : MarshalByRefObject
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("Client.exe.config", false);
            DiginoteManager dm = new DiginoteManager();
            /*
            ClientRepeater c = new ClientRepeater();
            Login login = new Login();

            c.userLoggedInEvent += login.UserLoggedIn;
            dm.userLogin += c.Repeater;
            
            Console.WriteLine("Press Enter to add a user.");
            Console.ReadLine();
            */
            dm.AddUser("teste", "teste");
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        class Login : MarshalByRefObject
        {
            public void UserLoggedIn(UserLoggedArgs param)
            {
                Console.WriteLine(param.Username + " logged in.");

            }
        }
    }
}

using System;

namespace Shared
{
    public delegate void UserLoginHandler(UserLoggedArgs pars);

    [Serializable]
    public class UserLoggedArgs : EventArgs
    {
        public string Username { get; set; }

        public UserLoggedArgs(string username)
        {
            Username = username;
        }
    }

    public class ClientRepeater : MarshalByRefObject
    {
        public event UserLoginHandler userLoggedInEvent;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(UserLoggedArgs param)
        {
            if (userLoggedInEvent != null)
                userLoggedInEvent(param);
        }
    }
}

using System;

namespace Shared
{
    //public delegate void UserLoginHandler(UserLoggedArgs pars);
    public delegate void FullSaleOrderHandler(FullSaleOrderArgs param);

    /*
    [Serializable]
    public class UserLoggedArgs : EventArgs
    {
        public string Username { get; set; }

        public UserLoggedArgs(string username)
        {
            Username = username;
        }
    }
    */
    [Serializable]
    public class FullSaleOrderArgs : EventArgs
    {
        public int Buyer { get; set; }
        public int Seller { get; set; }
        public int Quantity { get; set; }

        public FullSaleOrderArgs(int buyer, int seller, int quantity)
        {
            Buyer = buyer;
            Seller = seller;
            Quantity = quantity;
        }
    }

    public class FullSaleOrderRepeater : MarshalByRefObject
    {
        //public event UserLoginHandler userLoggedInEvent;
        public event FullSaleOrderHandler fullSaleOrder;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(FullSaleOrderArgs param)
        {
            if (fullSaleOrder != null)
                fullSaleOrder(param);
        }
    }
}

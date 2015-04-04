using System;

namespace Shared
{
    public delegate void SaleOrderHandler(SaleOrderArgs param);
    public delegate void PurchaseOrderHandler(PurchaseOrderArgs param);
    public delegate void ChangeQuotationHandler(ChangeQuotationArgs param);

    /* ARGS */

    [Serializable]
    public class SaleOrderArgs : EventArgs
    {
        public int Buyer { get; set; }
        public int Seller { get; set; }
        public int Quantity { get; set; }

        public SaleOrderArgs(int buyer, int seller, int quantity)
        {
            Buyer = buyer;
            Seller = seller;
            Quantity = quantity;
        }
    }

    [Serializable]
    public class PurchaseOrderArgs : EventArgs
    {
        public int Buyer { get; set; }
        public int Seller { get; set; }
        public int Quantity { get; set; }

        public PurchaseOrderArgs(int seller, int buyer, int quantity)
        {
            Buyer = buyer;
            Seller = seller;
            Quantity = quantity;
        }
    }

    [Serializable]
    public class ChangeQuotationArgs : EventArgs
    {
        public double NewQuotation { get; set; }
        public double OldQuotation { get; set; }

        public int Changer { get; set; }

        public ChangeQuotationArgs(double oldQuotation, double newQuotation, int changer)
        {
            this.OldQuotation = oldQuotation;
            this.NewQuotation = newQuotation;
            this.Changer = changer;
        }
    }


    /* REPEATERS */

    public class SaleOrderRepeater : MarshalByRefObject
    {
        public event SaleOrderHandler fullSaleOrder;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(SaleOrderArgs param)
        {
            if (fullSaleOrder != null)
                fullSaleOrder(param);
        }
    }

    public class PurchaseOrderRepeater : MarshalByRefObject
    {
        public event PurchaseOrderHandler fullPurchaseOrder;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(PurchaseOrderArgs param)
        {
            if (fullPurchaseOrder != null)
                fullPurchaseOrder(param);
        }
    }

    public class ChangeQuotationRepeater : MarshalByRefObject
    {
        public event ChangeQuotationHandler changeQuotation;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Repeater(ChangeQuotationArgs param)
        {
            if (changeQuotation != null)
                changeQuotation(param);
        }
    }
}

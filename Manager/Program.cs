﻿using System;
using System.Collections.Generic;
using Shared;
using System.Data.SQLite;
using System.Threading;

namespace Manager
{
    public class DiginoteManager : MarshalByRefObject
    {
        public event SaleOrderHandler saleOrder;
        public event PurchaseOrderHandler purchaseOrder;
        public event ChangeQuotationHandler changeQuotation;

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
        public int Login(string username, string password)
        {

            string sql1 = "select * from user where username = @username";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.Parameters.Add(new SQLiteParameter("@username", username));
            SQLiteDataReader reader1 = command1.ExecuteReader();
            if (!reader1.HasRows)
            {
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
                if (reader2.HasRows)
                {
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
            if (!reader1.HasRows) //if username doesn't exist
            {
                /*
                string sql2 = "insert into user(username, nickname, password) values ( @username, @nickname, @password )";
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                command2.Parameters.Add(new SQLiteParameter("@username", username));
                command2.Parameters.Add(new SQLiteParameter("@nickname", nickname));
                command2.Parameters.Add(new SQLiteParameter("@password", password));
                if (command2.ExecuteNonQuery() > 0){
                    string lastUserSql = "select last_insert_rowid() from user";
                    SQLiteCommand command3 = new SQLiteCommand(lastUserSql, m_dbConnection);
                    int lastId = (int)command3.ExecuteScalar();

                    return 0;
                }
                    
                else
                    return -2;
                 */

                //create sales_order
                string createSalesOrderSql = "insert into sales_order DEFAULT VALUES";
                SQLiteCommand command2 = new SQLiteCommand(createSalesOrderSql, m_dbConnection);
                if (command2.ExecuteNonQuery() > 0)
                {
                    string lastSalesOrderSql = "select last_insert_rowid() from sales_order";
                    SQLiteCommand command3 = new SQLiteCommand(lastSalesOrderSql, m_dbConnection);
                    long lastSalesOrder = (long)command3.ExecuteScalar();

                    //create purchase_order
                    string createPurchaseOrderSql = "insert into purchase_order DEFAULT VALUES";
                    SQLiteCommand command4 = new SQLiteCommand(createPurchaseOrderSql, m_dbConnection);
                    if (command4.ExecuteNonQuery() > 0)
                    {
                        string lastPurchaseOrderSql = "select last_insert_rowid() from purchase_order";
                        SQLiteCommand command5 = new SQLiteCommand(lastPurchaseOrderSql, m_dbConnection);
                        long lastPurchaseOrder = (long)command5.ExecuteScalar();

                        //create user
                        string createUserSql = "insert into user(username, password, nickname, purchase_order, sales_order) VALUES (@username, @password, @nickname, @purchase_order, @sales_order)";
                        SQLiteCommand command6 = new SQLiteCommand(createUserSql, m_dbConnection);
                        command6.Parameters.Add(new SQLiteParameter("@username", username));
                        command6.Parameters.Add(new SQLiteParameter("@nickname", nickname));
                        command6.Parameters.Add(new SQLiteParameter("@password", password));
                        command6.Parameters.Add(new SQLiteParameter("@purchase_order", lastPurchaseOrder));
                        command6.Parameters.Add(new SQLiteParameter("@sales_order", lastSalesOrder));
                        if (command6.ExecuteNonQuery() > 0)
                            return 0;
                        else
                            return -2;
                    }
                    else
                        return -2;
                }
                else
                    return -2;
            }
            return -1;
        }

        
        //return the number of diginotes that weren't bought
        public int BuyDiginotes(int buyer, int quantity)
        {
            if (quantity <= 0)
                return 0;

            int bought_quantity = 0;

            //ver se ha esta quantidade a venda
            string sql = "select u.id, quantity from user u, sales_order s where u.sales_order = s.id " +
            "and quantity > 0 and u.id <> @buyer and s.is_busy = 0 order by date desc";
            SQLiteCommand selectUsersWithSalesOrders = new SQLiteCommand(sql, m_dbConnection);
            selectUsersWithSalesOrders.Parameters.Add(new SQLiteParameter("@buyer", buyer));
            SQLiteDataReader reader = selectUsersWithSalesOrders.ExecuteReader();
            while (reader.Read())
            {
                int seller = reader.GetInt32(0);
                int quantitySale = reader.GetInt32(1); //quantidade de diginotes a venda para cada ordem de venda

                int soldQuantity = quantity > quantitySale ? quantitySale : quantity;

                if (saleOrder != null)
                {
                    Delegate[] invkList = saleOrder.GetInvocationList();

                    foreach (SaleOrderHandler handler in invkList)
                    {
                        new Thread(() =>
                        {
                            try
                            {
                                handler(new SaleOrderArgs(buyer, seller, soldQuantity));
                                Console.WriteLine("Invoking event handler");
                            }
                            catch (Exception)
                            {
                                saleOrder -= handler;
                                Console.WriteLine("Exception: Removed an event handler");
                            }
                        }).Start();
                    }
                }

                string updateQuantitySeller = "update sales_order set quantity = quantity - @quantity_sold where id = (select sales_order from user where id = @seller)";
                SQLiteCommand updateQuantitySalesOrder = new SQLiteCommand(updateQuantitySeller, m_dbConnection);
                updateQuantitySalesOrder.Parameters.Add(new SQLiteParameter("@quantity_sold", soldQuantity));
                updateQuantitySalesOrder.Parameters.Add(new SQLiteParameter("@seller", seller));
                updateQuantitySalesOrder.ExecuteNonQuery();


                string sql2 = "select serial_number from diginote where owner = @seller limit @quantity_sold";
                SQLiteCommand selectDiginotesFromSeller = new SQLiteCommand(sql2, m_dbConnection);
                selectDiginotesFromSeller.Parameters.Add(new SQLiteParameter("@quantity_sold", soldQuantity));
                selectDiginotesFromSeller.Parameters.Add(new SQLiteParameter("@seller", seller));
                SQLiteDataReader reader2 = selectDiginotesFromSeller.ExecuteReader();
                while (reader2.Read())
                {
                    Console.WriteLine("Editar diginote " + reader2.GetString(0));

                    //fazer update das diginotes (deixam de estar a venda)
                    string updateDiginotesSql = "update diginote set owner = @buyer where serial_number = @serial_number";
                    SQLiteCommand updateDiginoteOwner = new SQLiteCommand(updateDiginotesSql, m_dbConnection);
                    updateDiginoteOwner.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    updateDiginoteOwner.Parameters.Add(new SQLiteParameter("@serial_number", reader2.GetString(0)));
                    updateDiginoteOwner.ExecuteNonQuery();
                    bought_quantity++;

                    if (bought_quantity == quantity)
                        break;
                }
                if (bought_quantity == quantity)
                    break;
            }

            string updatePurchaseSql = "update purchase_order set quantity = @difference where id = (select purchase_order from user where id = @buyer)";
            SQLiteCommand updateQuantityPurchaseOrder = new SQLiteCommand(updatePurchaseSql, m_dbConnection);
            updateQuantityPurchaseOrder.Parameters.Add(new SQLiteParameter("@difference", quantity - bought_quantity));
            updateQuantityPurchaseOrder.Parameters.Add(new SQLiteParameter("@buyer", buyer));
            updateQuantityPurchaseOrder.ExecuteNonQuery();

            return quantity - bought_quantity;

        }


        public int SellDiginotes(int seller, int quantity)
        {
            if (quantity <= 0)
                return 0;

            int sold_quantity = 0;

            //ver se ha esta quantidade para comprar
            string sql = "select u.id, quantity from user u, purchase_order p where u.purchase_order = p.id " +
            "and quantity > 0 and u.id <> @seller and p.is_busy = 0 order by date desc";
            SQLiteCommand selectUsersWithPurchaseOrders = new SQLiteCommand(sql, m_dbConnection);
            selectUsersWithPurchaseOrders.Parameters.Add(new SQLiteParameter("@seller", seller));
            SQLiteDataReader reader = selectUsersWithPurchaseOrders.ExecuteReader();
            while (reader.Read())
            {
                int buyer = reader.GetInt32(0);
                int quantityPurchase = reader.GetInt32(1); //quantidade de diginotes para compra

                int purchasedQuantity = quantity > quantityPurchase ? quantityPurchase : quantity;

                if (purchaseOrder != null)
                {
                    Delegate[] invkList = purchaseOrder.GetInvocationList();

                    foreach (PurchaseOrderHandler handler in invkList)
                    {
                        new Thread(() =>
                        {
                            try
                            {
                                handler(new PurchaseOrderArgs(seller, buyer, purchasedQuantity));
                                Console.WriteLine("Invoking event handler");
                            }
                            catch (Exception)
                            {
                                purchaseOrder -= handler;
                                Console.WriteLine("Exception: Removed an event handler");
                            }
                        }).Start();
                    }
                }

                string updateQuantityBuyer = "update purchase_order set quantity = quantity - @quantity_purchased where id = (select sales_order from user where id = @buyer)";
                SQLiteCommand updateQuantityPurchaseOrder = new SQLiteCommand(updateQuantityBuyer, m_dbConnection);
                updateQuantityPurchaseOrder.Parameters.Add(new SQLiteParameter("@quantity_purchased", purchasedQuantity));
                updateQuantityPurchaseOrder.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                updateQuantityPurchaseOrder.ExecuteNonQuery();


                string sql2 = "select serial_number from diginote where owner = @seller limit @quantity_purchased";
                SQLiteCommand selectDiginotesFromSeller = new SQLiteCommand(sql2, m_dbConnection);
                selectDiginotesFromSeller.Parameters.Add(new SQLiteParameter("@quantity_purchased", purchasedQuantity));
                selectDiginotesFromSeller.Parameters.Add(new SQLiteParameter("@seller", seller));
                SQLiteDataReader reader2 = selectDiginotesFromSeller.ExecuteReader();
                while (reader2.Read())
                {
                    Console.WriteLine("Editar diginote " + reader2.GetString(0));

                    //fazer update das diginotes (deixam de estar a venda)
                    string updateDiginotesSql = "update diginote set owner = @buyer where serial_number = @serial_number";
                    SQLiteCommand updateDiginoteOwner = new SQLiteCommand(updateDiginotesSql, m_dbConnection);
                    updateDiginoteOwner.Parameters.Add(new SQLiteParameter("@buyer", buyer));
                    updateDiginoteOwner.Parameters.Add(new SQLiteParameter("@serial_number", reader2.GetString(0)));
                    updateDiginoteOwner.ExecuteNonQuery();
                    sold_quantity++;

                    if (sold_quantity == quantity)
                        break;
                }
                if (sold_quantity == quantity)
                    break;

            }

            string updatePurchaseSql = "update sales_order set quantity = @difference where id = (select sales_order from user where id = @seller)";
            SQLiteCommand updateQuantitySalesOrder = new SQLiteCommand(updatePurchaseSql, m_dbConnection);
            updateQuantitySalesOrder.Parameters.Add(new SQLiteParameter("@difference", quantity - sold_quantity));
            updateQuantitySalesOrder.Parameters.Add(new SQLiteParameter("@seller", seller));
            updateQuantitySalesOrder.ExecuteNonQuery();

            return quantity - sold_quantity;

        }


        public int GetPurchases(int client)
        {
            string sql = "select quantity from user u, purchase_order p where u.purchase_order = p.id and u.id = @user_id";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", client));
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public int GetSales(int client)
        {
            string sql = "select quantity from user u, sales_order s where u.sales_order = s.id and u.id = @user_id";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", client));
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
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

        public void ChangeQuotation(double oldQuotation, double newQuotation, int changer)
        {
            string sql = "insert into quotation(value) values (@new_quotation)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@new_quotation", newQuotation));
            command.ExecuteNonQuery();

            if (changeQuotation != null)
            {
                Delegate[] invkList = changeQuotation.GetInvocationList();

                foreach (ChangeQuotationHandler handler in invkList)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            handler(new ChangeQuotationArgs(oldQuotation, newQuotation, changer));
                            Console.WriteLine("Invoking event handler");
                        }
                        catch (Exception)
                        {
                            changeQuotation -= handler;
                            Console.WriteLine("Exception: Removed an event handler");
                        }
                    }).Start();
                }
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

        public bool HasSales(int ClientId)
        {
            string sql = "select 1 from user u, sales_order s where u.sales_order = s.id and u.id = @user_id and quantity > 0";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            SQLiteDataReader reader = command.ExecuteReader();
            return reader.HasRows;
        }

        public bool HasPurchases(int ClientId)
        {
            string sql = "select 1 from user u, purchase_order p where u.purchase_order = p.id and u.id = @user_id and quantity > 0";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            SQLiteDataReader reader = command.ExecuteReader();
            return reader.HasRows;
        }

        public void DeletePurchase(int ClientId)
        {
            string sql = "update purchase_order set quantity = 0 where id = (select purchase_order from user where id = @user_id)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            command.ExecuteNonQuery();
        }

        public void DeleteSales(int ClientId)
        {
            string sql = "update sales_order set quantity = 0 where id = (select sales_order from user where id = @user_id)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            command.ExecuteNonQuery();
        }

        public void SetPurchaseBusy(int ClientId, bool is_busy)
        {
            string sql = "update purchase_order set is_busy = @busy where id = (select purchase_order from user where id = @user_id)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@busy", is_busy ? 1 : 0));
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            command.ExecuteNonQuery();
        }

        public void SetSalesBusy(int ClientId, bool is_busy)
        {
            string sql = "update sales_order set is_busy = @busy where id = (select sales_order from user where id = @user_id)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.Parameters.Add(new SQLiteParameter("@busy", is_busy ? 1 : 0));
            command.Parameters.Add(new SQLiteParameter("@user_id", ClientId));
            command.ExecuteNonQuery();
        }

    }
}

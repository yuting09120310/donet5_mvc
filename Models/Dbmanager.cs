using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace my_first_mvc.Models
{
    public class Dbmanager
    {
        private readonly string dbconn = @"Data Source=DESKTOP-OCAV8JE\SQLEXPRESS;Initial Catalog=Production;User ID=sa;Password=chi";

        public class Account
        {
            public string ID { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string deptname { get; set; }
        }

        public List<Account> GetAccounts(string dept)
        {
            List<Account> accounts = new List<Account>();
            SqlConnection sqlConn = new SqlConnection(dbconn);
            SqlCommand sqlComm = new SqlCommand($"SELECT * FROM Account where deptname = '{dept}'");
            sqlComm.Connection = sqlConn;
            sqlConn.Open();

            SqlDataReader reader = sqlComm.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Account account = new Account
                    {
                        ID = reader.GetString(reader.GetOrdinal("ID")),
                        password = reader.GetString(reader.GetOrdinal("password")),
                        name = reader.GetString(reader.GetOrdinal("name")),
                        deptname = reader.GetString(reader.GetOrdinal("deptname")),
                    };
                    accounts.Add(account);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConn.Close();
            return accounts;
        }

        public void newAccount(Dbmanager.Account account)
        {
            SqlConnection sqlConn = new SqlConnection(dbconn);
            SqlCommand sqlComm = new SqlCommand($"INSERT INTO account values ('{account.ID}','{account.password}','{account.name}','{account.deptname}')");
            sqlComm.Connection = sqlConn;

            //sqlcomm.Parameters.Add(new SqlParameter("@ID", account.ID));
            //sqlcomm.Parameters.Add(new SqlParameter("@password", account.password));
            //sqlcomm.Parameters.Add(new SqlParameter("@name", account.name));
            //sqlcomm.Parameters.Add(new SqlParameter("@deptname", account.deptname));
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }

        public Account getAccount(string id)
        {
            Account account = new Account();
            SqlConnection sqlConn = new SqlConnection(dbconn);
            SqlCommand sqlComm = new SqlCommand($"SELECT * FROM Account where id = '{id}'");
            sqlComm.Connection = sqlConn;
            sqlConn.Open();

            SqlDataReader reader = sqlComm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    account = new Account
                    {
                        ID = reader.GetString(reader.GetOrdinal("ID")),
                        password = reader.GetString(reader.GetOrdinal("password")),
                        name = reader.GetString(reader.GetOrdinal("name")),
                        deptname = reader.GetString(reader.GetOrdinal("deptname")),
                    };
                }
            }
            sqlConn.Close();
            return account; 
        }

        public Account EditAccount(Dbmanager.Account account)
        {
            SqlConnection sqlConn = new SqlConnection(dbconn);
            SqlCommand sqlComm = new SqlCommand($"Update account set password='{account.password}', name='{account.name}', deptname='{account.deptname}' where ID = '{account.ID}'");
            sqlComm.Connection = sqlConn;
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
            return account;
        }

        public void DelAccount(string ID)
        {
            SqlConnection sqlConn = new SqlConnection(dbconn);
            SqlCommand sqlComm = new SqlCommand($"Delete account where ID = '{ID}'");
            sqlComm.Connection = sqlConn;
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
    }
}

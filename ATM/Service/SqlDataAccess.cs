using ADO.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ADO.NET.Service
{
    public class SqlDataAccess : IDataAccess
    {
        private static string connectionString = "Data Source=DESKTOP-TDD2GFT\\SQLEXPRESS;Initial Catalog=adonet;Integrated Security=True";

        public List<AccountModel> GetAllAccounts()
        {
            List<AccountModel> accounts = new List<AccountModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Accounts", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accounts.Add(new AccountModel(reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4)));
                    }
                }
            }

            return accounts;
        }

        public List<CardModel> GetAllCards()
        {
            List<CardModel> cards = new List<CardModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Cards", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cards.Add(new CardModel(Convert.ToInt64(reader.GetDecimal(0)), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), int.Parse(reader.GetString(4)), reader.GetString(5), reader.GetString(6)));
                    }
                }
            }

            return cards;
        }

        public List<LogModel> GetAllLogs()
        {
            List<LogModel> logs = new List<LogModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Logs", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        logs.Add(new LogModel(reader.GetDateTime(1), reader.GetString(2), reader.GetString(3)));
                    }
                }
            }

            return logs;
        }

        public CardModel GetCardByAccount(string account)
        {
            // could we technically just make an SQL command that does this? Yes. Do I have the time as I procrastinated this until the last day? No.
            // besides this is shorter
            return GetAllCards().Find(x => x.Account == account);
        }

        public CardModel GetCardByAccount(AccountModel account)
        {
            // this is just so we can be lazy and throw in the model instead of it's name
            return GetAllCards().Find(x => x.Account == account.AccountNumber);
        }

        public List<LogModel> GetLogsByAccount(string account)
        {
            return GetAllLogs().Where(x => x.Account == account).ToList();
        }

        public List<LogModel> GetLogsByAccount(AccountModel account)
        {
            return GetAllLogs().Where(x => x.Account == account.AccountNumber).ToList();
        }

        public AccountModel GetAccountByCard(CardModel card)
        {
            return GetAllAccounts().Find(x => x.AccountNumber == card.Account);
        }

        public AccountModel GetAccountByCard(long cardNumber)
        {
            CardModel card = GetAllCards().Find(x => x.CardNumber == cardNumber);
            if (card != default)
            {
                return GetAccountByCard(card);
            }
            return null;
        }

        public bool WithdrawMoney(decimal amount, AccountModel account)
        {
            string sqlExpression = $"UPDATE Accounts SET Money={account.Money - amount} WHERE AccountNumber='{account.AccountNumber}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Updated {number} lines");
            }

            AddLog(DateTime.UtcNow, $"Withdrew {amount} EUR from account.", account.AccountNumber);
            return true;
        }

        public bool DepositMoney(decimal amount, AccountModel account)
        {
            string sqlExpression = $"UPDATE Accounts SET Money={account.Money + amount} WHERE AccountNumber='{account.AccountNumber}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Updated {number} lines");
            }

            AddLog(DateTime.UtcNow, $"Deposited {amount} EUR to account.", account.AccountNumber);
            return true;
        }

        public void AddLog(DateTime date, string action, string accountNumber)
        {
            string sqlExpression = $"INSERT INTO Logs (Date, Action, Account) VALUES ('{date}', '{action}', '{accountNumber}');";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Updated {number} lines");
            }
        }
    }
}

using ADO.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.NET.Service
{
    public interface IDataAccess
    {
        /// <summary>
        /// Returns a list of all bank cards.
        /// </summary>
        /// <returns>List of CardModel.</returns>
        public List<CardModel> GetAllCards();
        /// <summary>
        /// Returns a list of all bank accounts.
        /// </summary>
        /// <returns>List of AccountModel.</returns>
        public List<AccountModel> GetAllAccounts();
        /// <summary>
        /// Returns a list of all bank logs.
        /// </summary>
        /// <returns>List of LogModel.</returns>
        public List<LogModel> GetAllLogs();

        /// <summary>
        /// Returns a Card by using account name (number).
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public CardModel GetCardByAccount(string accountName);
        /// <summary>
        /// Returns a Card by using an AccountModel object.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public CardModel GetCardByAccount(AccountModel account);
        /// <summary>
        /// Returns Logs by using account name (number).
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public List<LogModel> GetLogsByAccount(string accountName);
        /// <summary>
        /// Returns Logs by using an AccountModel object.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<LogModel> GetLogsByAccount(AccountModel account);
        /// <summary>
        /// Returns an Account by using a CardModel object.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public AccountModel GetAccountByCard(CardModel card);
        /// <summary>
        /// Returns an Account by using a card number.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public AccountModel GetAccountByCard(long cardNumber);

        /// <summary>
        /// Adds a Log to the list of Activity Logs.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="action"></param>
        /// <param name="accountNumber"></param>
        public void AddLog(DateTime date, string action, string accountNumber);

        /// <summary>
        /// Withdraws money from the specified Account.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool WithdrawMoney(decimal amount, AccountModel account);
        /// <summary>
        /// Deposits money into the specified account.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool DepositMoney(decimal amount, AccountModel account);

        // Potential methods to add but this isn't ATM as much as backend related
        //public void AddAccount(AccountModel newAccount);
        //public void AddCard(CardModel newCard);
    }
}
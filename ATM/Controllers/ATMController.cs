using ADO.NET.Models;
using ADO.NET.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class ATMController : Controller
    {
        // our Data Access object
        private IDataAccess data = new SqlDataAccess();

        public IActionResult Index(string notification)
        {
            ViewData["Notification"] = notification;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string cardNumber, string cardPIN)
        {
            // Validation for whether the user gave us some valid data or bad data and whether they were smart enough to disable our HTML checks
            if (long.TryParse(cardNumber, out long number))
            {
                if (cardNumber.Length > 16)
                {
                    return Index("Invalid card number!");
                }

                if (data.GetAllCards().Find(x => x.CardNumber == number) == default)
                {
                    return Index("Card does not exist!");
                }

                if (cardPIN != data.GetAllCards().Find(x => x.CardNumber == number).PIN)
                {
                    return Index("Wrong PIN!");
                }

                AccountModel account = data.GetAccountByCard(number);
                
                if (account == default)
                {
                    return Index("No card exists with that number!");
                }

                return Options(account);
            }
            else
            {
                return Index("Invalid card number!");
            }
        }

        public IActionResult Options(AccountModel account)
        {
            // finds all the logs associated with the account and adds them to the account
            account.Logs = data.GetLogsByAccount(account);
            return View("Options", account);
        }

        public IActionResult Options(AccountModel account, string notification)
        {
            // finds all the logs associated with the account and adds them to the account
            ViewData["Notification"] = notification;
            account.Logs = data.GetLogsByAccount(account);
            return View("Options", account);
        }

        [HttpPost]
        public IActionResult Withdraw(decimal moneyAmount, string accountNumber)
        {
            AccountModel account = data.GetAllAccounts().Find(x => x.AccountNumber == accountNumber);
            account.Logs = data.GetLogsByAccount(account);

            if (moneyAmount > account.Money)
            {
                return Options(account, $"Error! Not enough money. Missing {moneyAmount - account.Money} EUR");
            }
            if (moneyAmount <= 0)
            {
                return Options(account, "Error! Cannot enter negative numbers.");
            }
            if (data.WithdrawMoney(moneyAmount, account))
            {
                // we refresh the account data here to update the money by executing this command again
                account = data.GetAllAccounts().Find(x => x.AccountNumber == accountNumber);
                return Options(account, $"Sucessfully withdrawn {moneyAmount} EUR!");
            }
            else
            {
                return Options(account, "Error sucesfully finishing transaction.");
            }
        }

        [HttpPost]
        public IActionResult Deposit(decimal moneyAmount, string accountNumber)
        {
            AccountModel account = data.GetAllAccounts().Find(x => x.AccountNumber == accountNumber);
            account.Logs = data.GetLogsByAccount(account);
            if (moneyAmount <= 0)
            {
                return Options(account, "Error! Cannot enter negative numbers or deposit nothing.");
            }
            if (moneyAmount.ToString().Length > 25)
            {
                return Options(account, "Error! You are too rich for our poor database to add that many moneys. Spend some on ice cream or something.");
            }
            if (data.DepositMoney(moneyAmount, account))
            {
                // we refresh the account data here to update the money by executing this command again
                account = data.GetAllAccounts().Find(x => x.AccountNumber == accountNumber);
                return Options(account, $"Sucessfully deposited {moneyAmount} EUR!");
            }
            else
            {
                return Options(account, "Error sucesfully finishing transaction.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
    public class AccountModel
    {
        public string AccountNumber { get; protected set; }
        public string PersonalCode { get; protected set; }
        public decimal Money { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }

        public List<LogModel> Logs;

        public AccountModel(string accountNumber, string personalCode, decimal money, string name, string surname)
        {
            AccountNumber = accountNumber;
            PersonalCode = personalCode;
            Money = money;
            Name = name;
            Surname = surname;
        }
    }
}

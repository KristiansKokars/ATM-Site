using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
    public class CardModel
    {
        public long CardNumber { get; protected set; }
        public string OwnerName { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public int CVC2 { get; protected set; }
        public string Account { get; protected set; }

        public string PIN { get; protected set; }

        public CardModel(long cardNumber, string ownerName, DateTime expirationDate, DateTime dateOfbirth, int cVC2, string account, string pIN)
        {
            CardNumber = cardNumber;
            OwnerName = ownerName;
            ExpirationDate = expirationDate;
            DateOfBirth = dateOfbirth;
            CVC2 = cVC2;
            Account = account;
            PIN = pIN;
        }
    }
}

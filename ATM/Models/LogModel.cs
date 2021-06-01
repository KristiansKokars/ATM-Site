using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
    public class LogModel
    {
        // public int Id { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Action { get; protected set; }
        public string Account { get; protected set; }

        public LogModel(DateTime date, string action, string account)
        {
            Date = date;
            Action = action;
            Account = account;
        }
    }
}

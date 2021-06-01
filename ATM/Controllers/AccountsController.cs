using ADO.NET.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class AccountsController : Controller
    {
        private IDataAccess data = new SqlDataAccess();

        public ActionResult Index()
        {
            return View("Accounts", data.GetAllAccounts());
        }
    }
}

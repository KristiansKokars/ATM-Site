using ADO.NET.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class CardsController : Controller
    {
        private IDataAccess data = new SqlDataAccess();

        public IActionResult Index()
        {
            return View("Cards", data.GetAllCards());
        }
    }
}

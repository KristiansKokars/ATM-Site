using ADO.NET.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class LogsController : Controller
    {
        private IDataAccess data = new SqlDataAccess();

        public IActionResult Index()
        {
            return View("Logs", data.GetAllLogs());
        }
    }
}

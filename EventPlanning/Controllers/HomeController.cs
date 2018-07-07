using EventPlanning.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPlanning.Controllers
{
    public class Context : PetaPoco.NetCore.Database
    {
        private IDbConnection _connection;

        public Context(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }
        public Context(string connectionStringName) : base(connectionStringName)
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
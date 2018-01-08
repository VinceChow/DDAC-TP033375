using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.Controllers
{
	public class CustomersController : Controller
	{
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Customers
		public ActionResult Index()
		{
			var customers = _context.Customers
				.Include(c => c.RegisteredBy)
				.ToList();

			return View(customers);
		}

		public ActionResult Details(int id)
		{
			throw new NotImplementedException();
		}

		public ActionResult New()
		{
			ViewBag.Title = "New Customer";
			ViewBag.Action = "Create";

			return View("CustomerForm");
		}

		public ActionResult Edit(int id)
		{
			throw new NotImplementedException();
		}

		public ActionResult Delete()
		{
			throw new NotImplementedException();
		}
	}
}
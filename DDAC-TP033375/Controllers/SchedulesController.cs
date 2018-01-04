using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.Controllers
{
	public class SchedulesController : Controller
	{
		private ApplicationDbContext _context;

		public SchedulesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: Schedules
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Details(int id)
		{
			throw new NotImplementedException();
		}

		public ActionResult Create()
		{
			return View();
		}

		public ActionResult Edit(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Schedule schedule)
		{
			throw new NotImplementedException();
		}

		[HttpDelete]
		public ActionResult Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
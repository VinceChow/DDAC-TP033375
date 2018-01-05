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
			var schedules = _context.Schedules.ToList();

			return View(schedules);
		}

		public ActionResult Details(int id)
		{
			var schedule = _context.Schedules.SingleOrDefault(s => s.Id == id);

			if (schedule == null)
				return HttpNotFound();

			return PartialView("_Details", schedule);
		}

		public ActionResult New()
		{
			return View();
		}

		public ActionResult Edit(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Exclude = "Id")] Schedule schedule)
		{
			if (!ModelState.IsValid)
			{
				return View("New");
			}

			_context.Schedules.Add(schedule);

			try
			{
				_context.SaveChanges();

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Schedule has been created successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = ex.Message;
				ModelState.Remove("DepartureTime");
				ModelState.Remove("ArrivalTime");
			}

			return View("New");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(Schedule schedule)
		{
			if (!ModelState.IsValid)
			{
				return View("New");
			}

			//	var scheduleInDb = _context.Schedules.Single(s => s.Id == schedule.Id);
			//	scheduleInDb.Origin = schedule.Origin;
			//	scheduleInDb.Destination = schedule.Destination;
			//	scheduleInDb.DepartureTime = schedule.DepartureTime;
			//	scheduleInDb.ArrivalTime = schedule.ArrivalTime;

			//try
			//{
			//	_context.SaveChanges();
			//	ViewBag.IsSuccess = true;
			//	ViewBag.Message = "Schedule has been created successfully.";
			//	return View("Create");
			//}
			//catch (Exception ex)
			//{
			//	ViewBag.IsSuccess = false;
			//	ViewBag.Message = ex.Message;
			//	return View("Create");
			//}

			ViewBag.IsSuccess = true;
			ViewBag.Message = "Schedule has been created successfully.";
			return View("New");
		}

		[HttpDelete]
		public ActionResult Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
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
			ViewBag.Title = "New Schedule";
			ViewBag.Action = "Create";

			return View("ScheduleForm");
		}

		public ActionResult Edit(int id)
		{
			var schedule = _context.Schedules.SingleOrDefault(s => s.Id == id);

			if (schedule == null)
				return HttpNotFound();

			ViewBag.Title = "Edit Schedule";
			ViewBag.Action = "Update";

			return View("ScheduleForm", schedule);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Exclude = "Id")] Schedule schedule)
		{
			if (!ModelState.IsValid)
			{
				return View("ScheduleForm");
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
				ViewBag.Message = "Create Failed.\nError: " + ex.Message;
				ModelState.Remove("DepartureTime");
				ModelState.Remove("ArrivalTime");
			}

			return View("ScheduleForm");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(Schedule schedule)
		{
			if (!ModelState.IsValid)
			{
				return View("ScheduleForm");
			}

			var scheduleInDb = _context.Schedules.Single(s => s.Id == schedule.Id);

			if (scheduleInDb == null)
				return HttpNotFound();

			scheduleInDb.Origin = schedule.Origin;
			scheduleInDb.Destination = schedule.Destination;
			scheduleInDb.DepartureTime = schedule.DepartureTime;
			scheduleInDb.ArrivalTime = schedule.ArrivalTime;

			try
			{
				_context.SaveChanges();

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Schedule has been updated successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.\nError: " + ex.Message;
				ModelState.Remove("DepartureTime");
				ModelState.Remove("ArrivalTime");
			}

			return View("ScheduleForm");
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var scheduleInDb = _context.Schedules.Single(s => s.Id == id);

			if (scheduleInDb == null)
				return HttpNotFound();

			_context.Schedules.Remove(scheduleInDb);

			try
			{
				_context.SaveChanges();

				return Json(new { success = true, responseText = "Schedule has been deleted successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Delete Failed.\nError: " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_TP033375.Models;
using DDAC_TP033375.ViewModels;

namespace DDAC_TP033375.Controllers
{
	[Authorize(Roles = RoleName.Admin)]
	public class SchedulesController : Controller
	{
		private ApplicationDbContext _context;

		public SchedulesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Schedules
		public ActionResult Index()
		{
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			var schedules = _context.Schedules.ToList();

			return View(schedules);
		}

		public ActionResult Details(int id)
		{
			var schedule = _context.Schedules.SingleOrDefault(s => s.Id == id);

			if (schedule == null)
				return HttpNotFound();

			var viewModel = new ScheduleDetailsViewModel
			{
				Schedule = schedule,
				Ships = _context.Ships.Where(s => s.ScheduleId == id).ToList()
			};

			return PartialView("_Details", viewModel);
		}

		public ActionResult New()
		{
			ViewBag.Title = "New Schedule";
			ViewBag.Action = "Create";

			return View("ScheduleForm");
		}

		public ActionResult Edit(int id)
		{
			var ships = _context.Ships.Where(s => s.ScheduleId == id).ToList();

			if (ships.Count != 0)
			{
				TempData["Message"] = "Unable to edit selected schedule.<br/><strong>Error:</strong> One or more ships have been assigned to the schedule.";

				return RedirectToAction("Index");
			}

			ViewBag.Title = "Edit Schedule";
			ViewBag.Action = "Update";

			var schedule = _context.Schedules.SingleOrDefault(s => s.Id == id);

			if (schedule == null)
				return HttpNotFound();

			return View("ScheduleForm", schedule);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Exclude = "Id")] Schedule schedule)
		{
			ViewBag.Title = "New Schedule";
			ViewBag.Action = "Create";

			if (!ModelState.IsValid)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Create Failed.";

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
				ViewBag.Message = "Create Failed.<br/><strong>Error:</strong> " + ex.Message;
				ModelState.Remove("DepartureTime");
				ModelState.Remove("ArrivalTime");
			}

			return View("ScheduleForm");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(Schedule schedule)
		{
			ViewBag.Title = "Edit Schedule";
			ViewBag.Action = "Update";

			var scheduleInDb = _context.Schedules.Single(s => s.Id == schedule.Id);

			if (scheduleInDb == null)
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.";

				return View("ScheduleForm", scheduleInDb);
			}

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
				ViewBag.Message = "Update Failed.<br/><strong>Error:</strong> " + ex.Message;
				ModelState.Remove("DepartureTime");
				ModelState.Remove("ArrivalTime");
			}

			var newScheduleInDb = _context.Schedules.Single(s => s.Id == schedule.Id);

			return View("ScheduleForm", newScheduleInDb);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var ships = _context.Ships.Where(s => s.ScheduleId == id).ToList();

			if (ships.Count != 0)
			{
				return Json(new
				{
					success = false,
					responseText = "Unable to delete selected schedule.<br/><strong>Error:</strong> One or more ships have been assigned to this schedule."
				}, JsonRequestBehavior.AllowGet);
			}

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
				return Json(new { success = false, responseText = "Delete Failed.<br/><strong>Error:</strong> " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

	}
}
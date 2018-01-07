using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_TP033375.Models;
using DDAC_TP033375.ViewModels;
using Microsoft.Ajax.Utilities;

namespace DDAC_TP033375.Controllers
{
	public class ShipsController : Controller
	{
		private ApplicationDbContext _context;

		public ShipsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Ships
		public ActionResult Index()
		{
			var ships = _context.Ships.ToList();

			return View(ships);
		}

		public ActionResult Details(int id)
		{
			var ship = _context.Ships.Include(s => s.Schedule).SingleOrDefault(s => s.Id == id);

			if (ship == null)
				return HttpNotFound();

			return PartialView("_Details", ship);
		}

		public ActionResult New()
		{
			ViewBag.Title = "New Ship";
			ViewBag.Action = "Create";

			return View("ShipForm", NewShipFormViewModel());
		}

		public ActionResult Edit(int id)
		{
			var ship = _context.Ships.Include(s => s.Schedule).SingleOrDefault(s => s.Id == id);

			if (ship == null)
				return HttpNotFound();

			ViewBag.Title = "Edit Ship";
			ViewBag.Action = "Update";

			return View("ShipForm", ExistingShipFormViewModel(ship));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Ship ship)
		{
			var viewModel = NewShipFormViewModel();

			if (!ModelState.IsValid)
			{
				return View("ShipForm", viewModel);
			}

			ship.Schedule = null;
			ship.NumberOfAvailableContainerBay = ship.NumberOfContainerBay;
			ship.IsScheduled = true;

			_context.Ships.Add(ship);

			try
			{
				_context.SaveChanges();

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Ship has been added successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Fail to add ship.\nError: " + ex.Message;
			}

			return View("ShipForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(Ship ship)
		{
			if (!ModelState.IsValid)
			{
				return View("ShipForm");
			}

			var shipInDb = _context.Ships.Include(s => s.Schedule).Single(s => s.Id == ship.Id);

			if (shipInDb == null)
				return HttpNotFound();

			// Validate Number of in used Container Bays

			int numberOfUnavailableContainerBay = shipInDb.NumberOfContainerBay - shipInDb.NumberOfAvailableContainerBay;

			if (ship.NumberOfContainerBay < numberOfUnavailableContainerBay)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.\nError: This ship currently has " + numberOfUnavailableContainerBay + " container bays in used.";

				return View("ShipForm");
			}

			// END OF VALIDATION

			shipInDb.Name = ship.Name;
			shipInDb.NumberOfAvailableContainerBay += ship.NumberOfContainerBay - shipInDb.NumberOfContainerBay;
			shipInDb.NumberOfContainerBay = ship.NumberOfContainerBay;
			shipInDb.ScheduleId = ship.ScheduleId;
			shipInDb.IsScheduled = ship.IsScheduled;

			try
			{
				_context.SaveChanges();

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Ship has been updated successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.\nError: " + ex.Message;
			}

			return View("ShipForm");
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var shipInDb = _context.Ships.Single(s => s.Id == id);

			if (shipInDb == null)
				return HttpNotFound();

			_context.Ships.Remove(shipInDb);

			try
			{
				_context.SaveChanges();

				return Json(new { success = true, responseText = "Ship has been deleted successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Delete Failed.\nError: " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult FillDestination(string origin)
		{
			var destinations = _context.Schedules
				.Where(s => s.Origin == origin)
				.ToList()
				.DistinctBy(s => s.Destination);

			return Json(destinations, JsonRequestBehavior.AllowGet);
		}

		public ActionResult FillTime(string origin, string destination)
		{
			var schedules = _context.Schedules
				.Where(s => s.Origin == origin && s.Destination == destination)
				.ToList();

			return Json(schedules, JsonRequestBehavior.AllowGet);
		}

		private ShipFormViewModel NewShipFormViewModel()
		{
			return new ShipFormViewModel
			{
				Ship = new Ship(),
				Schedules = _context.Schedules
					.DistinctBy(s => s.Origin)
					.ToList()
			};
		}

		private ShipFormViewModel ExistingShipFormViewModel(Ship ship)
		{
			return new ShipFormViewModel
			{
				Ship = ship,
				Schedules = _context.Schedules
					.DistinctBy(s => s.Origin)
					.ToList()
			};
		}

	}
}
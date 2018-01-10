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
	public class BookingsController : Controller
	{
		private ApplicationDbContext _context;

		public BookingsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Bookings
		public ActionResult Index()
		{
			var bookings = _context.Bookings
				.Include(b => b.BookedBy)
				.Include(b => b.Customer)
				.Include(b => b.Ship.Schedule)
				.ToList();

			return View(bookings);
		}

		public ActionResult Details(int id)
		{
			throw new NotImplementedException();
		}

		public ActionResult New(/*int id*/)
		{
			int id = 1;
			var viewModel = new BookingFormViewModel
			{
				Customer = _context.Customers.Single(c => c.Id == id)
			};

			return View(viewModel);
		}

		public ActionResult Edit(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(BookingFormViewModel viewModel)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public ActionResult Archive(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public ActionResult CreateContainer(Container tempContainer)
		{
			var container = new Container
			{
				ItemType = tempContainer.ItemType,
				WeightInTonne = tempContainer.WeightInTonne
			};

			_context.Containers.Add(container);

			try
			{
				_context.SaveChanges();
				var newContainer = _context.Containers.Find(container.Id);

				return Json(new {newContainer, success = true, responseText = "Container created successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Fail to create container.\nError: " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

	}
}
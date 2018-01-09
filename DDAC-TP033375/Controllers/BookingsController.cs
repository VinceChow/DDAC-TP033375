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

		public ActionResult New()
		{
			ViewBag.Title = "New Booking";
			ViewBag.Action = "Create";

			return View("BookingForm", NewBookingFormViewModel());
		}

		public ActionResult Edit(int id)
		{
			throw new NotImplementedException();
		}

		public ActionResult Archive(int id)
		{
			throw new NotImplementedException();
		}

		private BookingFormViewModel NewBookingFormViewModel()
		{
			return new BookingFormViewModel
			{
				Booking = new Booking(),
				Customers = _context.Customers.ToList()
			};
		}
	}
}
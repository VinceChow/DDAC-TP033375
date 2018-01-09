using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DDAC_TP033375.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
			var customer = _context.Customers
				.Include(c => c.RegisteredBy)
				.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			ViewBag.Title = "Edit Customer";
			ViewBag.Action = "Update";

			return View("CustomerForm", customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Exclude = "Id")] Customer customer)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Title = "New Customer";
				ViewBag.Action = "Create";
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Create Failed.";

				return View("CustomerForm");
			}

			var currentUser = _context.Users.Find(User.Identity.GetUserId());
			customer.RegisteredBy = currentUser;

			_context.Customers.Add(customer);

			try
			{
				_context.SaveChanges();

				ViewBag.Title = "New Customer";
				ViewBag.Action = "Create";
				ViewBag.IsSuccess = true;
				ViewBag.Message = "Customer has been created successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.Title = "New Customer";
				ViewBag.Action = "Create";
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Create Failed.\nError: " + ex.Message;
			}

			return View("CustomerForm");
		}

		public ActionResult Delete()
		{
			throw new NotImplementedException();
		}
	}
}
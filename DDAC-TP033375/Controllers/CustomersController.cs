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
			if (TempData["Message"] != null)
			{
				ViewBag.Message = TempData["Message"];
			}

			List<Customer> customers;

			if (User.IsInRole(RoleName.Admin))
			{
				customers = _context.Customers
					.Include(c => c.RegisteredBy)
					.ToList();
			}
			else
			{
				var currentUser = _context.Users.Find(User.Identity.GetUserId());

				customers = _context.Customers
					.Include(c => c.RegisteredBy)
					.Where(c => c.RegisteredBy.CompanyName.Equals(currentUser.CompanyName))
					.ToList();
			}

			return View(customers);
		}

		public ActionResult Details(int id)
		{
			var customer = _context.Customers.Include(c => c.RegisteredBy).SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			return PartialView("_Details", customer);
		}

		public ActionResult New()
		{
			ViewBag.Title = "New Customer";
			ViewBag.Action = "Create";

			return View("CustomerForm");
		}

		public ActionResult Edit(int id)
		{
			var currentUser = _context.Users.Find(User.Identity.GetUserId());

			var customer = _context.Customers
				.Include(c => c.RegisteredBy)
				.SingleOrDefault(c => c.Id == id);

			if (customer == null)
				return HttpNotFound();

			if (!currentUser.CompanyName.Equals(customer.RegisteredBy.CompanyName))
			{
				TempData["Message"] = "The customer you are going to edit is not registered under your company.";

				return RedirectToAction("Index");
			}

			ViewBag.Title = "Edit Customer";
			ViewBag.Action = "Update";

			return View("CustomerForm", customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Exclude = "Id")] Customer customer)
		{
			ViewBag.Title = "New Customer";
			ViewBag.Action = "Create";

			if (!ModelState.IsValid)
			{
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

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Customer has been created successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Create Failed.\nError: " + ex.Message;
			}

			return View("CustomerForm");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(Customer customer)
		{
			ViewBag.Title = "Edit Customer";
			ViewBag.Action = "Update";

			var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

			if (customerInDb == null)
				return HttpNotFound();

			if (!ModelState.IsValid)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.";

				return View("CustomerForm", customerInDb);
			}

			customerInDb.Name = customer.Name;
			customerInDb.IdentificationNumber = customer.IdentificationNumber;
			customerInDb.Email = customer.Email;
			customerInDb.PhoneNumber = customer.PhoneNumber;

			try
			{
				_context.SaveChanges();

				ViewBag.IsSuccess = true;
				ViewBag.Message = "Customer has been updated successfully.";
				ModelState.Clear();
			}
			catch (Exception ex)
			{
				ViewBag.IsSuccess = false;
				ViewBag.Message = "Update Failed.\nError: " + ex.Message;
			}

			var newCustomerInDb = _context.Customers
				.Include(c => c.RegisteredBy)
				.Single(s => s.Id == customer.Id);

			return View("CustomerForm", newCustomerInDb);
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			var customerInDb = _context.Customers.Single(c => c.Id == id);

			if (customerInDb == null)
				return HttpNotFound();

			_context.Customers.Remove(customerInDb);

			try
			{
				_context.SaveChanges();

				return Json(new { success = true, responseText = "Customer has been deleted successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Delete Failed.\nError: " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}
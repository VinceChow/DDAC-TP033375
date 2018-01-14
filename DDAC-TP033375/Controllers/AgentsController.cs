using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DDAC_TP033375.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DDAC_TP033375.Controllers
{
	[Authorize(Roles = RoleName.Admin)]
	public class AgentsController : Controller
	{
		private ApplicationDbContext _context;

		public AgentsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Agents
		public ActionResult Index()
		{
			var agents = _context.Users.ToList();
			var adminRole = _context.Roles.First(r => r.Name == RoleName.Admin);

			foreach (var agent in agents.ToList())
			{
				if (agent.Roles.Any(r => r.RoleId == adminRole.Id))
				{
					agents.Remove(agent);
				}
			}

			return View(agents);
		}

		public ActionResult Disable(string id)
		{
			var agentInDb = _context.Users.Single(u => u.Id == id);

			agentInDb.IsEnabled = false;

			try
			{
				_context.SaveChanges();

				return Json(new { success = true, responseText = "Agent account has been disabled successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Fail to disable agent account.<br/><strong>Error:</strong> " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult Enable(string id)
		{
			var agentInDb = _context.Users.Single(u => u.Id == id);

			agentInDb.IsEnabled = true;

			try
			{
				_context.SaveChanges();

				return Json(new { success = true, responseText = "Agent account has been enabled successfully." }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = "Fail to enable agent account.<br/><strong>Error:</strong> " + ex.Message }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}
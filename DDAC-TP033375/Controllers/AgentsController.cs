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
	}
}
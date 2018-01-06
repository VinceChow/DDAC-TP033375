using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.ViewModels
{
	public class ShipFormViewModel
	{
		public Ship Ship { get; set; }
		public IEnumerable<Schedule> Schedules { get; set; }
	}
}
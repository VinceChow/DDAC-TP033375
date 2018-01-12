using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.ViewModels
{
	public class ScheduleDetailsViewModel
	{
		public Schedule Schedule { get; set; }

		public List<Ship> Ships { get; set; }
	}
}
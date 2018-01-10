using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.ViewModels
{
	public class BookingFormViewModel
	{
		public Customer Customer { get; set; }

		public int CustomerId { get; set; }

		public List<int> ContainerIds { get; set; }

		public int ScheduleId { get; set; }

		public int ShipId { get; set; }
	}
}
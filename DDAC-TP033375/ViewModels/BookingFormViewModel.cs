using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.ViewModels
{
	public class BookingFormViewModel
	{
		public IEnumerable<Customer> Customers { get; set; }

		public int CustomerId { get; set; }

		public IEnumerable<int> ContainerIds { get; set; }

		public int ScheduleId { get; set; }

		public int ShipId { get; set; }
	}
}
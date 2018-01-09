using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDAC_TP033375.Models;

namespace DDAC_TP033375.ViewModels
{
	public class BookingFormViewModel
	{
		public Booking Booking { get; set; }
		public IEnumerable<Customer> Customers { get; set; }
	}
}
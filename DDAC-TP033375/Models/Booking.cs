using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDAC_TP033375.Models
{
	public class Booking
	{
		public int Id { get; set; }

		[Display(Name = "Booking ID")]
		public int BookingId { get; set; }

		public ApplicationUser BookedBy { get; set; }

		public Customer Customer { get; set; }

		public ICollection<Container> Containers { get; set; }

		public Schedule Schedule { get; set; }

		public Ship Ship { get; set; }

		[Display(Name = "Booked At (mm/dd/yyyy)")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy h:mm tt}")]
		public DateTime BookedAt { get; set; }
	}
}
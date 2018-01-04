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

		public ApplicationUser BookedBy { get; set; }

		[Required]
		public Customer Customer { get; set; }

		[Required]
		public Container Container { get; set; }

		[Required]
		public Ship Ship { get; set; }

		public DateTime BookedAt { get; set; }
	}
}
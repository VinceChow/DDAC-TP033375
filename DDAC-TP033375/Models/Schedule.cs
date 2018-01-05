using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDAC_TP033375.Models
{
	public class Schedule
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Origin { get; set; }

		[Required]
		[StringLength(50)]
		public string Destination { get; set; }

		[Required]
		[Display(Name = "Departure Time (mm/dd/yyyy)")]
		public DateTime DepartureTime { get; set; }

		[Required]
		[Display(Name = "Arrival Time (mm/dd/yyyy)")]
		public DateTime ArrivalTime { get; set; }
	}
}
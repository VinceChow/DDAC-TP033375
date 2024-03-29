﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDAC_TP033375.Models
{
	public class Ship
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		[Display(Name = "Ship Name")]
		public string Name { get; set; }

		[Required]
		[Range(10, 1000)]
		[Display(Name = "Total Container Bay")]
		public int NumberOfContainerBay { get; set; }

		[Required]
		[Display(Name = "Available Container Bay")]
		public int NumberOfAvailableContainerBay { get; set; }

		public Schedule Schedule { get; set; }

		[Required]
		public int ScheduleId { get; set; }

		[Required]
		public Boolean IsScheduled { get; set; }

		public ICollection<Container> Containers { get; set; }
	}
}
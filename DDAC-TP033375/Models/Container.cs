using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DDAC_TP033375.Models
{
	public class Container
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Item Type")]
		public string ItemType { get; set; }

		[Required]
		[Range(1, 30)]
		[Display(Name = "Weight (tonne/t)")]
		public Double WeightInTonne { get; set; }

		[Required]
		[Range(1, 100)]
		public int Amount { get; set; }
	}
}
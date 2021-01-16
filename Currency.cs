using System;
using System.Collections.Generic;
using System.Text;

namespace StockQuotePuller
{
	class Currency
	{
		public Rates rates { get; set; }
		public string @base { get; set; }
		public string date { get; set; }


	}

	public class Rates
	{
		public double CAD { get; set; }
		public double USD { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace StockQuotePuller
{
	public class Result
	{
		public string language { get; set; }
		public string region { get; set; }
		public string quoteType { get; set; }
		public double regularMarketPrice { get; set; }
		public int regularMarketTime { get; set; }
		public double regularMarketChange { get; set; }
		public int regularMarketVolume { get; set; }
		public int exchangeDataDelayedBy { get; set; }
		public string marketState { get; set; }
		public string exchange { get; set; }
		public string shortName { get; set; }
		public int priceHint { get; set; }
		public double regularMarketChangePercent { get; set; }
		public double regularMarketPreviousClose { get; set; }
		public string fullExchangeName { get; set; }
		public string longName { get; set; }
		public int sourceInterval { get; set; }
		public string exchangeTimezoneName { get; set; }
		public string exchangeTimezoneShortName { get; set; }
		public int gmtOffSetMilliseconds { get; set; }
		public bool esgPopulated { get; set; }
		public bool tradeable { get; set; }
		public bool triggerable { get; set; }
		public string market { get; set; }
		public string symbol { get; set; }
	}

	public class QuoteResponse
	{
		public List<Result> result { get; set; }
		public object error { get; set; }
	}

	public class Quote
	{
		public QuoteResponse quoteResponse { get; set; }
	}
}

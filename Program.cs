using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace StockQuotePuller
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Command Started");

			try
			{
				string line;
				string apiKeyValue = "";
				StreamReader apiKeyFile = new StreamReader("apiKey.txt");
				while ((line = apiKeyFile.ReadLine()) != null)
				{
					apiKeyValue = line;
				}
				apiKeyFile.Close();
				string apiKey = "x-rapidapi-key";

				string apiHostKey = "x-rapidapi-host";
				string apiHostValue = "apidojo-yahoo-finance-v1.p.rapidapi.com";

				string apiRequestDestination = "https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-quotes?region=CA&lang=en&symbols=";
				string separator = "%2C";

				// Read the file and display it line by line.  

				StreamReader file = new StreamReader("quotes.txt");
				while ((line = file.ReadLine()) != null)
				{
					apiRequestDestination += separator;
					apiRequestDestination += line;
				}
				file.Close();

				WebRequest request = WebRequest.Create(apiRequestDestination);

				request.Headers.Add(apiHostKey, apiHostValue);
				request.Headers.Add(apiKey, apiKeyValue);
				//send that request
				WebResponse response = request.GetResponse();
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream);
				//put into string which is JSON formatted
				string responseFromServer = reader.ReadToEnd();
				JObject parsedString = JObject.Parse(responseFromServer);
				Quote quote = parsedString.ToObject<Quote>();


				//Print to CSV file
				var filepath = "quotes.csv";
				using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
				FileMode.Create, FileAccess.Write)))
				{
					writer.WriteLine("Ticker,Price");
					foreach (Result res in quote.quoteResponse.result)
					{
						writer.WriteLine("{0},{1}", res.symbol.ToString(), res.regularMarketPrice.ToString());
					}
					request = WebRequest.Create("https://api.exchangeratesapi.io/latest?symbols=USD,CAD");
					response = request.GetResponse();
					stream = response.GetResponseStream();
					reader = new StreamReader(stream);

					//put into string which is JSON formatted
					responseFromServer = reader.ReadToEnd();
					parsedString = JObject.Parse(responseFromServer);
					Currency currency = parsedString.ToObject<Currency>();
					double exchange = GetExchangeRate(currency);
					writer.WriteLine("CADUSD,{0}", exchange.ToString());
				}

			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				Console.WriteLine("Press Enter to exit...");
				Console.ReadLine();

			}
			Console.WriteLine("Command Ended");
		}

		private static double GetExchangeRate(Currency currency)
		{
			double rate = currency.rates.USD / currency.rates.CAD;
			return rate;
		}
	
	}
}

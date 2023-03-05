using System;
using Microsoft.Extensions.Configuration;
namespace converter
{
	public class ConfigReader
	{
		public static IConfiguration GetConfig()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(System.AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json");

			return builder.Build();


		}

	}

	public class GetParama
	{
        public string? _key { get; set; }
        public string? _value { get; set; }

    }

}


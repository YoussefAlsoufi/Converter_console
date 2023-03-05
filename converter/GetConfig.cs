using System;
using Microsoft.Extensions.Configuration;

namespace converter
{
	public class GetConfig
	{

		public readonly IConfiguration _configuration;

        public GetConfig(IConfiguration configuration)
		{
			_configuration = configuration;

		}

		public void GetResults()
		{
			List<string>? test = this._configuration.GetSection("configuration:Data")?.GetChildren()?.Select(x => x.Value)?.ToList();

        }


    }


}


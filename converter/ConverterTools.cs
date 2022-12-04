using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Converter
{
	public class ConverterTools
	{
        public NameValueCollection lengthSection, dataTypeSection;
		string errorMessage = "";
        public ConverterTools()
		{
            this.lengthSection = (NameValueCollection)ConfigurationManager.GetSection("Length");
            this.dataTypeSection = (NameValueCollection)ConfigurationManager.GetSection("Data");
        }

        public string DoConvert(string num, string fromUnit, string toUnit)
		{
	
			double result = 0;
			string resultMessage;
			var (checkStatus, usedSection)  = GenericCheckInputs(num, fromUnit, toUnit);
			if (checkStatus)
			{
				double inputValue = Convert.ToDouble(num);
				double from = Convert.ToDouble(usedSection[Singularize(fromUnit)]);
				double to = Convert.ToDouble(usedSection[Singularize(toUnit)]);

				result = inputValue * from / to;

	
				 resultMessage = $"( {num} {fromUnit},  {toUnit}) -> {result.ToString()}";       

            }
			else
			{
				resultMessage = $"Please, Check your Inputs again! you have entered incorrect units or nulls.";
            }

			return resultMessage;
		}

        private (bool valid, NameValueCollection Usedsection)  GenericCheckInputs(string inputNum, string fromUnit, string toUnit)
        {
			NameValueCollection Usedsection;

            if (this.lengthSection.AllKeys.Contains(fromUnit))
			{
                Usedsection = lengthSection;
			}else
			{
                Usedsection = dataTypeSection;

            }
            bool emptyCheck = ((!String.IsNullOrEmpty(inputNum)) && (!String.IsNullOrEmpty(fromUnit)) && (!String.IsNullOrEmpty(toUnit)));
            bool validNum = int.TryParse(inputNum, out int n) && n > 0;
            bool validInput = (Usedsection.AllKeys.Contains(Singularize(fromUnit)) && Usedsection.AllKeys.Contains(Singularize(toUnit)));

            return ((validInput && validNum && emptyCheck), Usedsection);
        }

  //      private static bool checkcheckInputs(NameValueCollection section, string inputNum, string fromUnit, string toUnit)
		//{

		//	bool emptyCheck = ((!String.IsNullOrEmpty(inputNum)) && (!String.IsNullOrEmpty(fromUnit)) && (!String.IsNullOrEmpty(toUnit)));
		//	bool validNum = int.TryParse(inputNum, out int n) && n > 0;
		//	bool validInput = (section.AllKeys.Contains(Singularize(fromUnit)) && section.AllKeys.Contains(Singularize(toUnit)));

		//	return (validInput && validNum && emptyCheck);
		//}

        protected static string Singularize(string inputString)
        {
			if (!string.IsNullOrEmpty(inputString))
			{
                var Singular = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new System.Globalization.CultureInfo("en-us"));
                return Singular.Singularize(inputString).ToLower().Trim();
            }
            else
			{
				return "";
			}

            
        }

    }

}


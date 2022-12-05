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
        public NameValueCollection lengthSection, dataTypeSection, tempretureSection;
        string resultMessage = "";
        public ConverterTools()
		{
            this.lengthSection = (NameValueCollection)ConfigurationManager.GetSection("Length");
            this.dataTypeSection = (NameValueCollection)ConfigurationManager.GetSection("Data");
            this.tempretureSection = (NameValueCollection)ConfigurationManager.GetSection("Tempreture");
        }

        public string DoConvert(string num, string fromUnit, string toUnit)
		{
	
			double result = 0;

			var (checkStatus, usedSection)  = GenericCheckInputs(num, fromUnit, toUnit);
			if (checkStatus)
			{
				double inputValue = Convert.ToDouble(num);
				if (usedSection == tempretureSection)
				{
					result= tempConvert(inputValue, Singularize(fromUnit), Singularize(toUnit));
				}
				else
				{
                    double from = Convert.ToDouble(usedSection[Singularize(fromUnit)]);
                    double to = Convert.ToDouble(usedSection[Singularize(toUnit)]);

                    result = inputValue * from / to;
          
                }

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
			NameValueCollection Usedsection = new NameValueCollection();
			bool wrongInputs = true;

            if (this.lengthSection.AllKeys.Contains(Singularize(fromUnit)))
			{
                Usedsection = lengthSection;
			}
			else if((this.dataTypeSection.AllKeys.Contains(Singularize(fromUnit))))
			{
                Usedsection = dataTypeSection;

            }
			else if (this.tempretureSection.AllKeys.Contains(Singularize(fromUnit)))
            {
				Usedsection = tempretureSection;
            }
			else
			{
				 wrongInputs = false;
			}
            bool emptyCheck = ((!String.IsNullOrEmpty(inputNum)) && (!String.IsNullOrEmpty(fromUnit)) && (!String.IsNullOrEmpty(toUnit)));
			bool validNum = int.TryParse(inputNum, out int n);
            bool validInput = (Usedsection.AllKeys.Contains(Singularize(fromUnit)) && Usedsection.AllKeys.Contains(Singularize(toUnit)));
			bool positiveValue = true ? ((tempretureSection.AllKeys.Contains(Singularize(fromUnit))) || n > 0) : false;

      
            return ((validInput && validNum && emptyCheck && wrongInputs && positiveValue), Usedsection);
        }

		private static double tempConvert(double tempInput, string fromTempUnit, string toTempUnit)
		{
			if (fromTempUnit== "celsiu")
			{
				var result = (tempInput * 1.8) + 32;
				return result;
			}
			return ((tempInput - 32) / 1.8);
		}
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


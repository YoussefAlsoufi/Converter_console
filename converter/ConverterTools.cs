using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using converter;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static converter.Length;
namespace Converter
{
    public class ConverterTools
    {
        public List<GetParama>? lengthSection, dataTypeSection, tempretureSection;
        string resultMessage = "";

        public ConverterTools()
        {

            // NuGet needs including Binder and Json packages
            var config = ConfigReader.GetConfig();
            
            this.lengthSection = config.GetSection("Length").Get<List<GetParama>>();
            this.dataTypeSection = config.GetSection("Data").Get<List<GetParama>>();
            this.tempretureSection = config.GetSection("Tempreture").Get<List<GetParama>>();

        }

        public string Test(string num, string fromUnit, string toUnit)
        {
            if (Length.test().Contain("meter"))
            {

            }
        } 
        public string DoConvert(string num, string fromUnit, string toUnit)
        {


            double result;

            var (checkStatus, usedSection) = GenericCheckInputs(num, fromUnit, toUnit);
            if (checkStatus)
            {
                double inputValue = Convert.ToDouble(num);
                if (usedSection == tempretureSection)
                {
                    result = tempConvert(inputValue, Singularize(fromUnit), Singularize(toUnit));
                }
                else
                {
                    var usedSectionDic  = usedSection?.ToDictionary(x => x._key, x => x._value);
                    var te = Singularize(fromUnit);
                    var testt = usedSectionDic?[Singularize(fromUnit)];
                    var doub = double.Parse("0.3048", System.Globalization.CultureInfo.InvariantCulture);

                    double from = Convert.ToDouble(usedSectionDic?[Singularize(fromUnit)]);
                    double to = Convert.ToDouble(usedSectionDic?[Singularize(toUnit)]);

                    result = inputValue * from / to;

                }

                resultMessage = $"( {num} {fromUnit},  {toUnit}) -> {result}";
            }
            else
            {
                resultMessage = $"Please, Check your Inputs again! you have entered incorrect units or nulls.";
            }

            return resultMessage;
        }

        private (bool valid, List<GetParama> Usedsection) GenericCheckInputs(string inputNum, string fromUnit, string toUnit)
        {
            List<GetParama>? UsedSection = new();
            bool wrongInputs = true;
            bool emptyCheck, validNum, validInput, positiveValue;

            if (inputNum is not null && fromUnit is not null && toUnit is not null)
            {
                if (lengthSection is not null && (lengthSection.Select(i => i._key).ToList().Contains(Singularize(fromUnit))))
                {
                    UsedSection = lengthSection;
                }
                else if (dataTypeSection is not null && (dataTypeSection.Select(i => i._key).ToList().Contains(Singularize(fromUnit))))
                {
                    UsedSection = dataTypeSection;

                }
                else if (tempretureSection is not null && (tempretureSection.Select(i => i._key).ToList().Contains(Singularize(fromUnit))))
                {
                    UsedSection = tempretureSection;
                }
                else
                {
                    wrongInputs = false;
                }

                emptyCheck = (!String.IsNullOrEmpty(inputNum)) && (!String.IsNullOrEmpty(fromUnit)) && (!String.IsNullOrEmpty(toUnit));
                validNum = int.TryParse(inputNum, out int n);
                validInput = (UsedSection.Select(i => i._key).ToList().Contains(Singularize(fromUnit))) && (UsedSection.Select(i => i._key).ToList().Contains(Singularize(toUnit)));
                positiveValue = true ? (tempretureSection is not null && (tempretureSection.Select(i => i._key).ToList().Contains(Singularize(fromUnit))) || n > 0) : false;


                return (validInput && validNum && emptyCheck && wrongInputs && positiveValue, UsedSection);
            }
            else
            {
                Console.WriteLine("One of your inputs at least is Null, please check and try again!");
                return (false, UsedSection);


            }

        }

        private static double tempConvert(double tempInput, string fromTempUnit, string toTempUnit)
        {
            if (fromTempUnit == "celsiu")
            {
                var result = (tempInput * 1.8) + 32;
                return result;
            }
            return ((tempInput - 32) / 1.8);
        }
        public static string Singularize(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                var Singular = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(new System.Globalization.CultureInfo("en-us"));
                var test = Singular.Singularize(inputString).ToLower().Trim();
                return Singular.Singularize(inputString).ToLower().Trim();
            }
            else
            {
                return "";
            }


        }

    }

}


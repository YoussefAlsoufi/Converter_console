using System;
using System.Globalization;
using Common.Common;

namespace Converter
{
    public class ConverterTools
    {
        public Dictionary<object, object>? lengthDic, dataTypeDic, tempertureDic;
        string resultMessage = "";

        public ConverterTools()
        {
            // NuGet needs including Binder and Json packages
            var length = new Length();
            lengthDic = length.GetType()
                                 .GetFields()
                                 .Select(field => new object[] { field.Name, field.GetValue(length) })
                                 .ToArray().ToDictionary(key => key[0], value => value[1]);

            var data = new DataType();
            dataTypeDic = data.GetType()
                                 .GetFields()
                                 .Select(field => new object[] { field.Name, field.GetValue(data) })
                                 .ToArray().ToDictionary(key => key[0], value => value[1]);

            var temperture = new Temperture();
            tempertureDic = temperture.GetType()
                                 .GetFields()
                                 .Select(field => new object[] { field.Name, field.GetValue(temperture) })
                                 .ToArray().ToDictionary(key => key[0], value => value[1]);

        }


        public string DoConvert(string num, string fromUnit, string toUnit)
        {
            try
            {
                //double result;
                double result=0.0;
                var toUnitValue = Singularize(toUnit);
                var fromUnitValue = Singularize(fromUnit);
                var (checkStatus, usedSection) = GenericCheckInputs(num, fromUnitValue, toUnitValue);
                if (checkStatus)
                {
                    //double inputValue = Convert.ToDouble(num);
                    var inputValue = float.Parse(num, CultureInfo.InvariantCulture.NumberFormat);
                    if (usedSection == tempertureDic)
                    {
                        result = TempConvert(inputValue, fromUnitValue, toUnitValue);

                    }
                    else
                    {
                        double from = Convert.ToDouble(usedSection?[fromUnitValue]);
                        double to = Convert.ToDouble(usedSection?[toUnitValue]);

                        result = (double)(inputValue * from / to);
                    }

                    return $"( {num} {fromUnit},  {toUnit}) -> {result}";
                }
                else
                {
                    return $"Please, Check your Inputs again! you have entered incorrect units or nulls.";
                }
            }
            catch
            {
                return " One of your inputs isn't existed, please check again!";
            }


            
        }

        private (bool valid, Dictionary<object, object> Usedsection) GenericCheckInputs(string inputNum, string fromUnit, string toUnit)
        {
            Dictionary<object, object>? UsedSection = new();
            bool wrongInputs = true;
            bool emptyCheck, validNum, validInput, positiveValue;

            if (inputNum is not null && fromUnit is not null && toUnit is not null)
            {
                if (lengthDic is not null && (lengthDic.ContainsKey(fromUnit)))
                {
                    UsedSection = lengthDic;
                }
                else if (dataTypeDic is not null && (dataTypeDic.ContainsKey(fromUnit)))
                {
                    UsedSection = dataTypeDic;

                }
                else if (tempertureDic is not null && (tempertureDic.ContainsKey(fromUnit)))
                {
                    UsedSection = tempertureDic;
                }
                else
                {
                    wrongInputs = false;
                }

                emptyCheck = (!String.IsNullOrEmpty(inputNum)) && (!String.IsNullOrEmpty(fromUnit)) && (!String.IsNullOrEmpty(toUnit));
                validNum = int.TryParse(inputNum, out int n);
                validInput = (UsedSection.ContainsKey(fromUnit)) && (UsedSection.ContainsKey(toUnit));
                positiveValue = true ? (tempertureDic is not null && (tempertureDic.ContainsKey(fromUnit)) || n > 0) : false;


                return (validInput && validNum && emptyCheck && wrongInputs && positiveValue, UsedSection);
            }
            else
            {
                Console.WriteLine("One of your inputs at least is Null, please check and try again!");
                return (false, UsedSection);


            }

        }

        private static double TempConvert(double tempInput, string fromTempUnit, string toTempUnit)
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
                var finalValue = Singular.Singularize(inputString).ToLower().Trim();
                if (finalValue.Equals("byte"))
                {
                    return "Byte";
                }
                else
                {
                    return finalValue;
                }
                 
            }
            else
            {
                return "";
            }


        }

    }

}


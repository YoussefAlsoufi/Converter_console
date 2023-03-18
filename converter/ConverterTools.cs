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
            double result = 0.0;
            var toUnitValue = Singularize(toUnit);
            var fromUnitValue = Singularize(fromUnit);
            var (checkStatus, usedSection) = GenericCheckInputs(num, fromUnitValue, toUnitValue);
            if (checkStatus)
            {
                double inputValue = Convert.ToDouble(num);
                if (usedSection == tempertureDic)
                {
                    result = TempConvert(inputValue, fromUnitValue, toUnitValue);
                }
                if (usedSection == lengthDic)
                {

                    double from = Convert.ToDouble(lengthDic[fromUnitValue]);
                    double to = Convert.ToDouble(lengthDic[toUnitValue]);

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


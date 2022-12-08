using System.Collections.Specialized;
using System.Configuration;
using Converter;
using ConverterClassLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            {
            
            ConverterTools converter = new ConverterTools();
            var convertClass = new ConverterLibrary();

            //Test Samples by using Converter_Console_App:
            Console.Write("feet > decifeet  \n");
            Console.Write(converter.DoConvert("4", "feet", "decifeets") + "\n");

            Console.Write("feet > Picofeets  \n");
            Console.Write(converter.DoConvert("4", "feet", "Picofeets") + "\n");

            Console.Write("kiloinches > inches  \n");
            Console.Write(converter.DoConvert("4", "inches", "kiloinch") + "\n");
            
            Console.Write(" km > m \n");
            Console.Write(converter.DoConvert("2", "kilometer", "meter") + "\n");

            Console.Write(" m > mm\n");
            Console.Write(converter.DoConvert("3", "    meter", "millimeter") + "\n");

            Console.Write("m > km \n");
            Console.Write(converter.DoConvert("4", "meter", "kilometer") + "\n");

            Console.Write("mm > inch \n");
            Console.Write(converter.DoConvert("4", "millimeters", "inch") + "\n");

            Console.Write("m > inch Plural inputs \n");
            Console.Write(converter.DoConvert("4", "meters", "inches") + "\n");

            Console.Write("m > inch NumberInput Is string \n");
            Console.Write(converter.DoConvert("F", "meter", "inches") + "\n");

            Console.Write("m > inch fromUnit Is Empty \n");
            Console.Write(converter.DoConvert("F", "", "inches") + "\n");

            Console.Write("m > inch fromUnit Is Null \n");
            Console.Write(converter.DoConvert("F", null, "inches") + "\n");

            Console.Write("m > inch Negative InputNum \n");
            Console.Write(converter.DoConvert("-20", "meter", "inches") + "\n");

            Console.Write("fahrenheit > Celsius  negative Temp \n");
            Console.Write(converter.DoConvert("-122", "fahrenheit", "Celsius") + "\n");

            Console.Write("m > inch InputNum=0 \n");
            Console.Write(converter.DoConvert("0", "meter", "inches") + "\n");

            Console.Write("byte > bit  \n");
            Console.Write(converter.DoConvert("4", "bytes", "bits") + "\n");

            Console.Write("byte > kilobyte  \n");
            Console.Write(converter.DoConvert("4", "bytes", "kilobyte") + "\n");

            Console.Write("byte > meter  \n");
            Console.Write(converter.DoConvert("4", "bytes", "meters") + "\n");

            Console.Write("terabyte > byte  \n");
            Console.Write(converter.DoConvert("4", "terabyte", "byte") + "\n");

            Console.Write("feet > decifeet  \n");
            Console.Write(converter.DoConvert("4", "feet", "decifeets") + "\n");

            Console.Write("meter > feet  \n");
            Console.Write(converter.DoConvert("1", "meter", "feet") + "\n");

            Console.Write("Celsius > fahrenheit  \n");
            Console.Write(converter.DoConvert("5", "Celsius", "fahrenheit") + "\n");

            Console.Write("fahrenheit > Celsius  \n");
            Console.Write(converter.DoConvert("12", "fahrenheit", "Celsius") + "\n");

            //Test Samples by using ConverterClassLibrary after adding 'ConvertClassLibrary.dll file to the Converter.Dependencies':

            Console.Write("fahrenheit > Celsius using a converterLibrary \n");
            Console.Write(convertClass.DoConvert("12", "fahrenheit", "Celsius") + "\n");

            Console.Write("byte > kilobyte using a converterLibrary  \n");
            Console.Write(converter.DoConvert("4", "bytes", "kilobyte") + "\n");

            Console.Write("");
            }
        }
    }
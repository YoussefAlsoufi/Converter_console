using System.Collections.Specialized;
using System.Configuration;
using Converter;

    class Program
    {
        static void Main(string[] args)
        {
            {
            
            ConverterTools converter = new ConverterTools();

            //NameValueCollection lengthsSection = converter.lengthSection;

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
            Console.Write("m > inch InputNum=0 \n");
            Console.Write(converter.DoConvert("4", "meter", "inches") + "\n");

            Console.Write("byte > bit  \n");
            Console.Write(converter.DoConvert("4", "bytes", "bits") + "\n");

            Console.Write("byte > kilobyte  \n");
            Console.Write(converter.DoConvert("4", "bytes", "kilobyte") + "\n");

            Console.Write("byte > meter  \n");
            Console.Write(converter.DoConvert("4", "bytes", "meters") + "\n");


            Console.Write("");
            }
        }
    }
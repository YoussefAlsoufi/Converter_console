using System;
using System.Collections.Specialized;
using Converter;
using NUnit.Framework;
using Xunit;
using DemoLibrary;
namespace converter
{
	
	public class ConverterTests
	{
        
        [Fact]
		public void TestDoConvert()
		{
            NameValueCollection Usedsection = new NameValueCollection();
            ConverterTools converter = new ConverterTools();

            var negativeCheck = converter.DoConvert("-20", "meter", "kilometer");
			var nullCheck = ((!String.IsNullOrEmpty("")) && (!String.IsNullOrEmpty("bit")) && (!String.IsNullOrEmpty("gigabyte")));
            var longthContainsCheck = Usedsection.AllKeys.Contains(ConverterTools.Singularize("Inches")) && Usedsection.AllKeys.Contains(ConverterTools.Singularize("feet"));
			var validInput= int.TryParse("F", out int n);


            Xunit.Assert.Multiple( ()=>
			{
				Xunit.Assert.Equal(negativeCheck, (0.2).ToString());
                Xunit.Assert.True(nullCheck, "Input has not be null or Empty");
                Xunit.Assert.True(longthContainsCheck, "Both Units should be in the same section {Length, DataType, Tempertures ...}");
                Xunit.Assert.True(validInput, "The input has to be a number!");

			}
			
			);
					

				
		}
	}
}


using System.Collections.Specialized;
using Converter;
using ConverterClassLibrary;
namespace ConverterTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Tests
{
    [Category("DoConvertTest")]
    public static void DoConvertTest()
    {
        NameValueCollection Usedsection = new NameValueCollection();
        ConverterTools converter = new ConverterTools();
        ConverterLibrary convertLibrary = new ConverterLibrary();

        var negativeCheck = converter.DoConvert("-20", "meter", "kilometer");
        var nullCheck = ((!String.IsNullOrEmpty("")) && (!String.IsNullOrEmpty("bit")) && (!String.IsNullOrEmpty("gigabyte")));
        var longthContainsCheck = Usedsection.AllKeys.Contains(ConverterTools.Singularize("Inches")) && Usedsection.AllKeys.Contains(ConverterTools.Singularize("feet"));
        var validInput = int.TryParse("F", out int n);

        Assert.Equals(negativeCheck, (0.2).ToString());
        Assert.IsTrue(nullCheck, "Input has not be null or Empty");
        Assert.IsTrue(longthContainsCheck, "Both Units should be in the same section {Length, DataType, Tempertures ...}");
        Assert.IsTrue(validInput, "The input has to be a number!");
        
    }

}

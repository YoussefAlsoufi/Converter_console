using converter;
using Converter;
using Microsoft.Extensions.Configuration;

namespace ConverterTest;

public class ConverterToolsTest
{
    private readonly ConverterTools converter = new();

    
    [Fact]
    public void LengthConverterTest()
    {
        Assert.NotNull(converter.lengthDic);
        Assert.Equal("( 2 kilometer,  meter) -> 2000", converter.DoConvert("4", "feet", "decifeets"));
        Assert.Equal("( 2 kilometer,  meter) -> 2000", converter.DoConvert("2", "kilometer", "meter"));
        Assert.Equal("( 4 feet,  Picofeets) -> 400000000000000.06", converter.DoConvert("4", "feet", "Picofeets"));
        Assert.Equal("( 4 inches,  kiloinch) -> 0.004", converter.DoConvert("4", "inches", "kiloinch"));
        Assert.Equal("( 3     meter,  millimeter) -> 3000", converter.DoConvert("3", "    meter", "millimeter"));
        Assert.Equal("( 4 meter,  kilometer) -> 0.004", converter.DoConvert("4", "meter", "kilometer"));
        Assert.Equal("( 4 millimeters,  inch) -> 0.15748031496062992", converter.DoConvert("4", "millimeters", "inch"));
        Assert.Equal("( 4 meters,  inches) -> 157.48031496062993", converter.DoConvert("4", "meters", "inches"));
        Assert.Equal("Please, Check your Inputs again! you have entered incorrect units or nulls.", converter.DoConvert("F", "meter", "inches"));
        Assert.Equal("Please, Check your Inputs again! you have entered incorrect units or nulls.", converter.DoConvert("F", "", "inches"));
        Assert.Equal("Please, Check your Inputs again! you have entered incorrect units or nulls.", converter.DoConvert("-20", "meter", "inches"));
        Assert.Equal("Please, Check your Inputs again! you have entered incorrect units or nulls.", converter.DoConvert("0", "meter", "inches"));
        Assert.Equal("( 4 feet,  decifeets) -> 40", converter.DoConvert("4", "feet", "decifeets"));
        Assert.Equal("( 1 meter,  feet) -> 3.280839895013123", converter.DoConvert("1", "meter", "feet"));
    }

    [Fact]
    public void DatatypeConverterTest()
    {
        Assert.NotNull(converter.dataTypeDic);
        Assert.Equal("( 4 bytes,  bits) -> 32", converter.DoConvert("4", "bytes", "bits"));
        Assert.Equal("( 4 bytes,  kilobyte) -> 0.00390625", converter.DoConvert("4", "bytes", "kilobyte"));
        Assert.Equal("Please, Check your Inputs again! you have entered incorrect units or nulls.", converter.DoConvert("4", "bytes", "meters"));
        Assert.Equal("( 4 terabyte,  byte) -> 4398046511104", converter.DoConvert("4", "terabyte", "byte"));
    }

    [Fact]
    public void TempretureConverterTest()
    {
        Assert.NotNull(converter.tempertureDic);
        Assert.Equal("( -122 fahrenheit,  Celsius) -> -85.55555555555556", converter.DoConvert("-122", "fahrenheit", "Celsius"));
        Assert.Equal("( 5 Celsius,  fahrenheit) -> 41", converter.DoConvert("5", "Celsius", "fahrenheit"));
        Assert.Equal("( 12 fahrenheit,  Celsius) -> -11.11111111111111", converter.DoConvert("12", "fahrenheit", "Celsius"));
    }
}

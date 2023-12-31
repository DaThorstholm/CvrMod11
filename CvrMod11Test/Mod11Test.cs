using CvrMod11;
using FluentAssertions;
using Xunit.Abstractions;

namespace CvrMod11Test;

public class Mod11Test
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Mod11Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Theory]
    [InlineData(400041, true)]
    [InlineData(400042, false)]
    public void IsNumberMod11(int cvr, bool isMod11)
    {
        Mod11.CheckMod11(cvr).Should().Be(isMod11);
    }

    [Theory]
    [InlineData("48117716", true)]
    [InlineData("48117717", false)]
    public void IsStringMod11(string cvr, bool isMod11)
    {
        Mod11.CheckMod11(cvr).Should().Be(isMod11);
    }
    
    [Fact]
    public void Test1()
    {
        var mod11Legit = 0;
        var ezCount = 0;
        var reversible = 0;
        for (var i = 99999; i < 99999999; i++)
        {
            var value = i.ToString().PadLeft(8, '0');
            
            if (Mod11.CheckMod11(value))
            {
                mod11Legit++;

                var ez = Mod11.EazyToRemember(value);
                if (ez)
                {
                    ezCount++;
                }
                else
                {
                    _testOutputHelper.WriteLine(value);
                }
                
                if (Mod11.Reversible(value))
                {
                    reversible++;
                }
            }
        }
        
        _testOutputHelper.WriteLine($"legit {mod11Legit}");
        _testOutputHelper.WriteLine($"ezCount {ezCount}");
        _testOutputHelper.WriteLine($"reversible {reversible}");
    }
    
    [Theory]
    [InlineData("11234567", false)]
    [InlineData("11234566", true)]
    [InlineData("11122889", true)]
    [InlineData("11223344", true)]
    [InlineData("00022333", true)]
    public void EzToRemember(string value, bool ez)
    {
        Mod11.EazyToRemember(value).Should().Be(ez);
    }
    
    [Theory]
    [InlineData("12344321", true)]
    [InlineData("11234566", false)]
    public void Reversible(string value, bool ez)
    {
        Mod11.Reversible(value).Should().Be(ez);
    }
}
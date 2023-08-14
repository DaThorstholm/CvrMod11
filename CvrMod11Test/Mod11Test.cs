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

    [Fact]
    public void IsMod11()
    {
        var cvr = "48117716";
        var isMod11 = Mod11.CheckMod11(cvr);

        isMod11.Should().BeTrue();
    }
    
    [Fact]
    public void Test1()
    {
        var mod11Legit = 0;
        var easy2 = 0;
        var easy2Plus = 0;
        var reversible = 0;
        for (int i = 99999; i < 99999999; i++)
        {
            var value = i.ToString().PadLeft(8, '0');
            
            if (Mod11.CheckMod11(value))
            {
                mod11Legit++;

                var ez = Mod11.EazyToRemember(value);
                if (ez > 1)
                {
                    easy2++;
                }
                if (ez > 2)
                {
                    easy2Plus++;
                }

                if (Mod11.Reversible(value))
                {
                    reversible++;
                }
            }
        }
        
        _testOutputHelper.WriteLine(mod11Legit.ToString());
        _testOutputHelper.WriteLine(easy2.ToString());
        _testOutputHelper.WriteLine(easy2Plus.ToString());
        _testOutputHelper.WriteLine(reversible.ToString());
    }
    
    [Fact]
    public void EzToRemember()
    {
        var value = "11123889";
            
        if (Mod11.EazyToRemember(value) == 3)
        {
            _testOutputHelper.WriteLine(value);
        }
        
        
    }
}
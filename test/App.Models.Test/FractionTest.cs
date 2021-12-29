using Xunit;
using App.Models;

namespace App.Models;

public class FractionTest 
{
    private void assertEqualFract(Fraction a, Fraction b) 
    {
        Assert.Equal(a.Num, b.Num);
        Assert.Equal(a.Denom, b.Denom);
    }

    [Fact]
    public void NegDenomTest()
    {
        var num = 3;
        var denom = -4;

        var res = new Fraction(num, denom);
        var exp = new Fraction(-3, 4);
        
        assertEqualFract(res, exp);
    }

    [Fact]
    public void AddTest()
    {
        var a = new Fraction(1, 2);
        var b = new Fraction(1, 4);

        var sum = a + b;
        var exp = new Fraction(3, 4); 

        assertEqualFract(sum, exp);
    }

    [Fact]
    public void SubTest()
    {
        var a = new Fraction(1, 2);
        var b = new Fraction(1, 4);

        var diff = a - b;
        var exp = new Fraction(1, 4); 

        assertEqualFract(diff, exp);
    }

    [Fact]
    public void MultTest()
    {
        var a = new Fraction(10, 2);
        var b = new Fraction(7, 8);

        var mult = a * b;
        var exp = new Fraction(35, 8);

        assertEqualFract(mult, exp);
    }

    [Fact]
    public void DivTest()
    {
        var a = new Fraction(10, 2);
        var b = new Fraction(7, 8);

        var div = a / b;
        var exp = new Fraction(40, 7);

        assertEqualFract(div, exp);
    }

    [Theory]
    [InlineData(-1, 2, true)]
    [InlineData(1, 2, true)]
    [InlineData(-2, 2, false)]
    [InlineData(3, 2, false)]
    public void IsProperTest(int num, int denom, bool exp)
    {
        var f = new Fraction(num, denom);

        var isProper = f.IsProper();

        Assert.Equal(isProper, exp);
    }

    [Theory]
    [InlineData(3, 2, "3/2")]
    [InlineData(4, 1, "4")]
    public void ToStringTest(int num, int denom, string exp)
    {
        var f = new Fraction(num, denom);

        var str = f.ToString();

        Assert.Equal(str, exp);
    }
}



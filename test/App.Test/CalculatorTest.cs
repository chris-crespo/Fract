using System.Linq;
using Xunit;
using App.Models;

namespace App;

public class CalculatorTest
{
    private void assertEqualFract(Fraction a, Fraction b) 
    {
        Assert.Equal(a.Num, b.Num);
        Assert.Equal(a.Denom, b.Denom);
    }

    [Fact]
    public void ArithmeticTest()
    {
        var calc = new Calculator();
        var a = new Fraction(3, 4);
        var b = new Fraction(2, 5);

        var res = calc.Add(a, b);
        var exp = new Fraction(23, 20);

        assertEqualFract(res, exp);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(7, 4)]
    public void RetrieveLastOperationsTest(int n, int exp) 
    {
        var calc = new Calculator();

        // Añadimos 4 fracciones al historial de la calculadora.
        Enumerable.Range(1, 4).ToList()
            .ForEach(x => calc.Add(new Fraction(x, 2), new Fraction(7, x)));

        var operations = calc.RetrieveLastOperations(n);

        Assert.Equal(operations.Count, exp);
    }
}

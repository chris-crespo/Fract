using System;

namespace App.Models;

public struct Fraction
{
    public int Num { get; }
    public int Denom { get; }

    public Fraction(int num, int denom)
    {
        if (denom == 0)
            throw new Exception("Denominador 0");

        this.Num   = denom < 0 ? -num : num;
        this.Denom = Math.Abs(denom);
    }

    public static int gcd(int a, int b) => b == 0 ? a : gcd(b, a % b);

    private static Fraction Simplify(int num, int denom) {
        var g = gcd(num, denom);
        return new Fraction(num / g, denom / g);
    }
    public Fraction Simplify() => Simplify(Num, Denom);

    public bool IsProper() => Math.Abs(Num) < Denom;

    public static Fraction operator +(Fraction a, Fraction b) =>
        Simplify(a.Num * b.Denom + b.Num * a.Denom, a.Denom * b.Denom);

    public static Fraction operator -(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Denom - b.Num * a.Denom, a.Denom * b.Denom);

    public static Fraction operator *(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Num, a.Denom * b.Denom);

    public static Fraction operator /(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Denom, a.Denom * b.Num);

    public override string ToString() 
        => Denom == 1 ? $"{Num}" : $"{Num}/{Denom}";
}

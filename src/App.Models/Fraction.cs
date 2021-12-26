using System;

namespace App.Models;

public struct Fraction
{
    public int Num;
    public int Denom;

    public Fraction(int num, int denom)
    {
        this.Num   = denom < 0 ? -num : num;
        this.Denom = Math.Abs(denom);
    }

    public static int gcd(int a, int b) => b == 0 ? a : gcd(b, a % b);

    private static Fraction Simplify(int num, int denom) {
        var g = gcd(num, denom);
        return new Fraction(num / g, denom / g);
    }
    public static Fraction Simplify(Fraction f) => Simplify(f.Num, f.Denom);

    public static Fraction operator +(Fraction a, Fraction b) =>
        Simplify(a.Num * b.Denom + b.Num * a.Denom, a.Denom * b.Denom);

    public static Fraction operator -(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Denom - b.Num * a.Denom, a.Denom * b.Denom);

    public static Fraction operator *(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Num, a.Denom * b.Denom);

    public static Fraction operator /(Fraction a, Fraction b) => 
        Simplify(a.Num * b.Denom, a.Denom * b.Num);
}

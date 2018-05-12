using System;
using System.Diagnostics;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
    {
        var rationalNumber = (double)r.Numerator / (double)r.Denominator;
        return Math.Pow(realNumber, rationalNumber);
    }
}

public struct RationalNumber
{

    private readonly int _numerator;
    private readonly int _denominator;

    public int Numerator { get { return _numerator; } }
    public int Denominator { get { return _denominator; } }

    public RationalNumber(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator of a rational number cannot be 0");
        }

        var gcd = Gcd(numerator, denominator);
        this._numerator = numerator / gcd;
        this._denominator = denominator / gcd;
    }

    public RationalNumber Add(RationalNumber r)
    {
        var numerator = (this.Numerator * r.Denominator) + (r.Numerator * this.Denominator);
        var denominator = this.Denominator * r.Denominator;
        return new RationalNumber(numerator, denominator);
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
    {
        return r1.Add(r2);
    }

    public RationalNumber Sub(RationalNumber r)
    {
        // r1 - r2 = a1/b1 - a2/b2 = (a1 * b2 - a2 * b1) / (b1 * b2)
        var numerator = (this.Numerator * r.Denominator) - (r.Numerator * this.Denominator);
        var denominator = this.Denominator * r.Denominator;
        return new RationalNumber(numerator, denominator);
    }

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
    {
        return r1.Sub(r2);
    }

    public RationalNumber Mul(RationalNumber r)
    {
        var numerator = this.Numerator * r.Numerator;
        var denominator = this.Denominator * r.Denominator;
        return new RationalNumber(numerator, denominator);
    }

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
    {
        return r1.Mul(r2);
    }

    public RationalNumber Div(RationalNumber r)
    {
        var numerator = this.Numerator * r.Denominator;
        var denominator = this.Denominator * r.Numerator;
        return new RationalNumber(numerator, denominator);
    }

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
    {
        return r1.Div(r2);
    }

    public RationalNumber Abs()
    {
        var numerator = Math.Abs(this.Numerator);
        var denominator = Math.Abs(this.Denominator);
        return new RationalNumber(numerator, denominator);
    }

    public RationalNumber Reduce()
    {
        // The constructor already reduces a rational number
        return new RationalNumber(this.Numerator, this.Denominator);
    }

    public RationalNumber Exprational(int power)
    {
        var absPower = Math.Abs(power);
        var numerator = IntPow(this.Numerator, absPower);
        var denominator = IntPow(this.Denominator, absPower);
        return new RationalNumber(numerator, denominator);
    }

    public double Expreal(int baseNumber)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public override string ToString()
    {
        return $"{this.Numerator}/{this.Denominator}";
    }

    private static int Gcd(int a, int b)
    {
        return b == 0 ? a : Gcd(b, a % b);
    }

    public static int IntPow(int baseValue, int power)
    {
        var exponent = 1;
        for (var i = 0; i < power; i++)
        {
            exponent *= baseValue;
        }
        return exponent;
    }
}
using System;
using System.Diagnostics;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r)
    {
        return r.Expreal(realNumber);
    }
}

[DebuggerDisplay("{Numerator} / {Denominator}")]
public struct RationalNumber
{
    public int Numerator { get; }
    public int Denominator { get; }

    public RationalNumber(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator of a rational number cannot be 0");
        }

        var gcd = Gcd(numerator, denominator);
        this.Numerator = numerator / gcd;
        this.Denominator = denominator / gcd;
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
        var rationalNumber = (double)this.Numerator / (double)this.Denominator;
        return Math.Pow(baseNumber, rationalNumber);
    }

    public override string ToString()
    {
        return $"{this.Numerator}/{this.Denominator}";
    }

    private static int Gcd(int a, int b)
    {
        // Tail recursive euclidian gcd
        return b == 0 ? a : Gcd(b, a % b);
    }

    private static int IntPow(int value, int power, int accumulator = 1)
    {
        // Tail recursive exponentiation by squaring
        return (power == 0)
                ? accumulator
                : (power % 2 == 0)
                    ? IntPow(value * value, power / 2, accumulator)
                    : IntPow(value * value, (power - 1) / 2, value * accumulator);
    }
}
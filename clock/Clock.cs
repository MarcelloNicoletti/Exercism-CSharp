public struct Clock
{
    private readonly int _minutes;
    
    public Clock(int hours, int minutes)
    {
        _minutes = FloorDivMod((hours * 60) + minutes, 24 * 60);
    }

    public int Hours => _minutes / 60;

    public int Minutes => _minutes % 60;

    public Clock Add(int minutesToAdd)
    {
        return new Clock(Hours, Minutes + minutesToAdd);
    }

    public Clock Subtract(int minutesToSubtract)
    {
        return Add(-minutesToSubtract);
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}";
    }

    private static int FloorDivMod(int a, int n)
    {
        // Floor division modulo handles negative dividends properly for a clock situation.
        var r = a % n;
        return r < 0 ? r + n : r;
    }
}
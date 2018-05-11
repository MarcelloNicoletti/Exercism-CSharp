using System;
using System.Linq;
using System.Collections.Generic;

public class School
{
    private readonly IDictionary<int, ICollection<string>> _roster = new Dictionary<int, ICollection<string>>();

    public void Add(string student, int grade)
    {
        if (!_roster.ContainsKey(grade))
        {
            _roster.Add(grade, new List<string>());
        }

        _roster[grade].Add(student);
    }

    public IEnumerable<string> Roster()
    {
        return _roster.OrderBy(x => x.Key).SelectMany(x => x.Value.OrderBy(y => y));
    }

    public IEnumerable<string> Grade(int grade)
    {
        if (!_roster.ContainsKey(grade))
        {
            return Enumerable.Empty<string>();
        }

        return _roster[grade].OrderBy(x => x);
    }
}
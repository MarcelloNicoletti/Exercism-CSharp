using System;
using System.Linq;
using System.Collections.Generic;

public class School
{
    private readonly IDictionary<int, ISet<string>> _roster = new SortedDictionary<int, ISet<string>>();

    public void Add(string student, int grade)
    {
        if (!_roster.ContainsKey(grade))
        {
            _roster.Add(grade, new SortedSet<string>());
        }

        _roster[grade].Add(student);
    }

    public IEnumerable<string> Roster()
    {
        foreach (var grade in _roster.Keys)
        {
            foreach (var student in _roster[grade])
            {
                yield return student;
            }
        }
    }

    public IEnumerable<string> Grade(int grade)
    {
        if (!_roster.ContainsKey(grade))
        {
            return Enumerable.Empty<string>();
        }

        return _roster[grade];
    }
}
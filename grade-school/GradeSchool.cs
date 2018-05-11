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
        foreach (var grade in _roster.Keys.OrderBy(x => x))
        {
            foreach (var student in _roster[grade].OrderBy(x => x))
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

        return _roster[grade].OrderBy(x => x);
    }
}
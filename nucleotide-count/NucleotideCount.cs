using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class NucleotideCount
{
    private static readonly List<char> ValidNucleotides = new List<char> {'A', 'C', 'G', 'T'};

    private readonly IDictionary<char, int> _nucleotideCounts;

    public NucleotideCount(string sequence)
    {
        if (sequence.Any(x => !ValidNucleotides.Contains(x)))
        {
            throw new InvalidNucleotideException();
        }

        var nucleotideCounts = countNucleotides(sequence);
        _nucleotideCounts = new ReadOnlyDictionary<char, int>(nucleotideCounts);
    }

    public IDictionary<char, int> NucleotideCounts
    {
        get { return _nucleotideCounts; }
    }

    private IDictionary<char, int> countNucleotides(string sequence)
    {
        var nucleotideCounts = new Dictionary<char, int>();
        ValidNucleotides.ForEach(x => nucleotideCounts.Add(x, 0));

        foreach (var nucleotide in sequence)
        {
            nucleotideCounts[nucleotide] += 1;
        }

        return nucleotideCounts;
    }
}

public class InvalidNucleotideException : Exception { }

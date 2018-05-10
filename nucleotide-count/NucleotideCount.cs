using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class NucleotideCount
{
    private static readonly List<char> ValidNucleotides = new List<char> {'A', 'C', 'G', 'T'};

    private readonly IDictionary<char, int> _nucleotideCounts;

    public NucleotideCount(string sequence)
    {
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
            if (!ValidNucleotides.Contains(nucleotide))
            {
                throw new InvalidNucleotideException();
            }

            nucleotideCounts[nucleotide] += 1;
        }

        return nucleotideCounts;
    }
}

public class InvalidNucleotideException : Exception { }

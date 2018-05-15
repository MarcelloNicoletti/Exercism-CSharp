using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

public class KindergartenGarden
{
    private const int NumPlantsPerStudentPerRow = 2;

    private static readonly IEnumerable<string> DefaultClass = new List<string>
    {
        "Alice",
        "Bob",
        "Charlie",
        "David",
        "Eve",
        "Fred",
        "Ginny",
        "Harriet",
        "Ileana",
        "Joseph",
        "Kincaid",
        "Larry"
    };

    private readonly IDictionary<string, List<Plant>> _studentPlants;

    public KindergartenGarden(string diagram) : this(diagram, DefaultClass)
    {
    }

    public KindergartenGarden(string diagram, IEnumerable<string> students)
    {
        _studentPlants = ParseDiagram(diagram, students);
    }

    public IEnumerable<Plant> Plants(string student)
    {
        return _studentPlants.ContainsKey(student) ? _studentPlants[student] : Enumerable.Empty<Plant>();
    }

    private static Dictionary<string, List<Plant>> ParseDiagram(string diagram, IEnumerable<string> students)
    {
        var plantsForStudent = new Dictionary<string, List<Plant>>();
        var diagramRowStrings = diagram.Split("\n");
        if (diagramRowStrings.Any(x => x.Length != diagramRowStrings[0].Length))
        {
            throw new ArgumentException($"{diagram} is not a valid diagram.");
        }

        var diagramPlants = diagramRowStrings.Select(rowString => rowString.Select(ParsePlantChar));
        var studentsCache = students.ToImmutableList();

        foreach (var row in diagramPlants)
        {
            var plantsFromRowByStudent = row.Partition(NumPlantsPerStudentPerRow).ToImmutableList();
            for (var i = 0; i < plantsFromRowByStudent.Count; i++)
            {
                var student = studentsCache[i];
                var plantsForStudentFromRow = plantsFromRowByStudent[i];

                if (!plantsForStudent.ContainsKey(student))
                {
                    plantsForStudent[student] = new List<Plant>();
                }

                plantsForStudent[student].AddRange(plantsForStudentFromRow);
            }
        }

        return plantsForStudent;
    }

    private static Plant ParsePlantChar(char plantChar)
    {
        switch (plantChar)
        {
            case 'v':
            case 'V':
                return Plant.Violets;
            case 'r':
            case 'R':
                return Plant.Radishes;
            case 'c':
            case 'C':
                return Plant.Clover;
            case 'g':
            case 'G':
                return Plant.Grass;
            default:
                throw new ArgumentException($"Plant type {plantChar} unknown.");
        }
    }
}

public static class StackOverflowLinqExtensions
{
    public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
    {
        // From https://stackoverflow.com/a/438208
        T[] array = null;
        var count = 0;
        foreach (var item in source)
        {
            if (array == null)
            {
                array = new T[size];
            }

            array[count++] = item;
            if (count != size)
            {
                continue;
            }

            yield return new ReadOnlyCollection<T>(array);
            array = null;
            count = 0;
        }

        if (array == null)
        {
            yield break;
        }

        Array.Resize(ref array, count);
        yield return new ReadOnlyCollection<T>(array);
    }
}
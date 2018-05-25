using System;
using System.Collections.Generic;
using System.Linq;

public class Allergies
{
    private readonly AllergyEnum _flags;

    public Allergies (int mask)
    {
        _flags = (AllergyEnum)mask;
    }

    public bool IsAllergicTo (string allergy)
    {
        return _flags.Has(allergy);
    }

    public IList<string> List ()
    {
        return _flags.GetAllergyList();
    }
}

[Flags]
public enum AllergyEnum
{
    Eggs = 1 << 0,
    Peanuts = 1 << 1,
    Shellfish = 1 << 2,
    Strawberries = 1 << 3,
    Tomatoes = 1 << 4,
    Chocolate = 1 << 5,
    Pollen = 1 << 6,
    Cats = 1 << 7
}

public static class AllergyEnumExtensions
{
    private static readonly IDictionary<AllergyEnum, string> AllergyEnumToString = new Dictionary<AllergyEnum, string>
    {
        {AllergyEnum.Eggs, "eggs"},
        {AllergyEnum.Peanuts, "peanuts"},
        {AllergyEnum.Shellfish, "shellfish"},
        {AllergyEnum.Strawberries, "strawberries"},
        {AllergyEnum.Tomatoes, "tomatoes"},
        {AllergyEnum.Chocolate, "chocolate"},
        {AllergyEnum.Pollen, "pollen"},
        {AllergyEnum.Cats, "cats"}
    };

    private static readonly IDictionary<string, AllergyEnum> AllergyStringToEnum =
        AllergyEnumToString.ToDictionary(entry => entry.Value, entry => entry.Key);

    public static bool Has (this AllergyEnum flags, string allergy)
    {
        if (!AllergyStringToEnum.ContainsKey(allergy))
        {
            return false;
        }

        var flag = AllergyStringToEnum[allergy];
        return flags.HasFlag(flag);
    }

    public static IList<string> GetAllergyList (this AllergyEnum flags)
    {
        var allergies = new List<string>();

        foreach (AllergyEnum allergy in Enum.GetValues(typeof(AllergyEnum)))
        {
            if (flags.HasFlag(allergy))
            {
                allergies.Add(AllergyEnumToString[allergy]);
            }
        }

        return allergies;
    }
}

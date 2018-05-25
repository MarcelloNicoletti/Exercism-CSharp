using System;
using System.Collections.Generic;
using System.Linq;

public class Allergies
{
    private readonly Allergens _allergenFlags;

    public Allergies (int mask)
    {
        _allergenFlags = (Allergens)mask;
    }

    public bool IsAllergicTo (string allergy)
    {
        return _allergenFlags.HasFlagFromString(allergy);
    }

    public IList<string> List ()
    {
        return _allergenFlags.GetAllergyStrings().ToList().AsReadOnly();
    }
}

[Flags]
public enum Allergens
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

public static class AllergensExtensions
{
    private static readonly IDictionary<Allergens, string> AllergyEnumToString = new Dictionary<Allergens, string>
    {
        {Allergens.Eggs, "eggs"},
        {Allergens.Peanuts, "peanuts"},
        {Allergens.Shellfish, "shellfish"},
        {Allergens.Strawberries, "strawberries"},
        {Allergens.Tomatoes, "tomatoes"},
        {Allergens.Chocolate, "chocolate"},
        {Allergens.Pollen, "pollen"},
        {Allergens.Cats, "cats"}
    };

    private static readonly IDictionary<string, Allergens> AllergyStringToEnum =
        AllergyEnumToString.ToDictionary(entry => entry.Value, entry => entry.Key);

    public static bool HasFlagFromString (this Allergens flags, string allergy)
    {
        if (!AllergyStringToEnum.ContainsKey(allergy))
        {
            return false;
        }

        var flag = AllergyStringToEnum[allergy];
        return flags.HasFlag(flag);
    }

    public static IEnumerable<string> GetAllergyStrings (this Allergens flags)
    {
        foreach (Allergens allergen in Enum.GetValues(typeof(Allergens)))
        {
            if (flags.HasFlag(allergen))
            {
                yield return AllergyEnumToString[allergen];
            }
        }
    }
}

using System;
using System.Collections.Generic;

class Resistivity {
    // Resistivity of some commonly used metals (Ω·m)
    private const double CopperResistivity = 15.7e-9;
    private const double GoldResistivity = 22.8e-9;
    private const double PlatinumResistivity = 98e-9;
    private const double SilverResistivity = 14.6e-9;

    // Resistivity ranges of some typical semiconductors (Ω·m)
    private static readonly Dictionary<string, (double Min, double Max)> ResistivityRangesSemiconductor = new()
    {
        { "Germanium", (1e-3, 10) },
        { "Silicon", (0.01, 1e3) },
        { "Zinc Oxide", (1e-2, 1e4) },
        { "Gallium Arsenide", (1e-6, 1e2) }
    };

    // Resistivity ranges of some typical insulators (Ω·m)
    private static readonly Dictionary<string, (double Min, double? Max)> ResistivityRangesInsulators = new()
    {
        { "Wood (damp)", (1e3, 1e4) },
        { "Deionized Water", (1e5, null) },
        { "Glass", (1e10, 1e14) },
        { "Fused Quartz", (1e17, null) }
    };

    // Main method
    static void Main() {
        Console.WriteLine("--- Resistivity Values of Metals (Ω·m) ---");
        Console.WriteLine($"Copper: {CopperResistivity}");
        Console.WriteLine($"Gold: {GoldResistivity}");
        Console.WriteLine($"Platinum: {PlatinumResistivity}");
        Console.WriteLine($"Silver: {SilverResistivity}");

        Console.WriteLine("\n--- Resistivity Ranges of Semiconductors (Ω·m) ---");
        foreach (var semiconductor in ResistivityRangesSemiconductor) {
            Console.WriteLine($"{semiconductor.Key}: {semiconductor.Value.Min} to {semiconductor.Value.Max}");
        }

        Console.WriteLine("\n--- Resistivity Ranges of Insulators (Ω·m) ---");
        foreach (var insulator in ResistivityRangesInsulators) {
            string range = insulator.Value.Max.HasValue
                ? $"{insulator.Value.Min} to {insulator.Value.Max.Value}"
                : $"{insulator.Value.Min}";
            Console.WriteLine($"{insulator.Key}: {range}");
        }

        Console.Write("\nEnter value of length (L) in meters: ");
        double length = double.Parse(Console.ReadLine());
        Console.Write("Enter value of area (A) in square meters: ");
        double area = double.Parse(Console.ReadLine());

        Console.WriteLine("\nChoose material type:");
        Console.Write("Enter 'C' for Conductors, 'S' for Semiconductors, or 'I' for Insulators: ");
        char choice = char.ToUpper(Console.ReadLine()[0]);

        switch (choice) {
            case 'C':
                HandleConductorChoice(length, area);
                break;

            case 'S':
                HandleSemiconductorChoice(length, area);
                break;

            case 'I':
                HandleInsulatorChoice(length, area);
                break;

            default:
                Console.WriteLine("Invalid material type!");
                break;
        }
    }




    // Handle conductor choice
    private static void HandleConductorChoice(double length, double area) {
        Console.WriteLine("\nChoose specific metal:");
        Console.WriteLine("1. 'C' for Copper\n2. 'G' for Gold\n3. 'P' for Platinum\n4. 'S' for Silver");
        Console.Write("Choose: ");
        char metalChoice = char.ToUpper(Console.ReadLine()[0]);

        double resistivity = metalChoice switch {
            'C' => CopperResistivity,
            'G' => GoldResistivity,
            'P' => PlatinumResistivity,
            'S' => SilverResistivity,
            _ => -1 // Invalid choice
        };

        if (resistivity != -1) {
            double resistance = Resistance_Resistivity(resistivity, length, area);
            Console.WriteLine($"Resistance: {resistance} Ω");
        } else {
            Console.WriteLine("Invalid choice!");
        }
    }

    // Handle semiconductor choice
    private static void HandleSemiconductorChoice(double length, double area) {
        Console.WriteLine("\nChoose specific semiconductor:");
        Console.WriteLine("1. 'G' for Germanium\n2. 'S' for Silicon\n3. 'O' for Zinc Oxide\n4. 'A' for Gallium Arsenide");
        Console.Write("Choose: ");
        char semiChoice = char.ToUpper(Console.ReadLine()[0]);

        string material = semiChoice switch {
            'G' => "Germanium",
            'S' => "Silicon",
            'O' => "Zinc Oxide",
            'A' => "Gallium Arsenide",
            _ => null
        };

        if (material != null && ResistivityRangesSemiconductor.TryGetValue(material, out var resistivityRange)) {
            PerformResistivityRange(resistivityRange.Min, resistivityRange.Max, material, length, area);
        } else {
            Console.WriteLine("Invalid choice!");
        }
    }

    // Handle insulator choice
    private static void HandleInsulatorChoice(double length, double area) {
        Console.WriteLine("\nChoose specific insulator:");
        Console.WriteLine("1. 'W' for Wood (damp)\n2. 'D' for Deionized Water\n3. 'G' for Glass\n4. 'F' for Fused Quartz");
        Console.Write("Choose: ");
        char insChoice = char.ToUpper(Console.ReadLine()[0]);

        string material = insChoice switch {
            'W' => "Wood (damp)",
            'D' => "Deionized Water",
            'G' => "Glass",
            'F' => "Fused Quartz",
            _ => null
        };

        if (material != null && ResistivityRangesInsulators.TryGetValue(material, out var resistivityRange)) {
            PerformResistivityRange(resistivityRange.Min, resistivityRange.Max ?? resistivityRange.Min, material, length, area);
        } else {
            Console.WriteLine("Invalid choice!");
        }
    }

    // Calculate resistance range and display results
    private static void PerformResistivityRange(double minR, double maxR, string material, double length, double area) {
        double resistanceMin = Resistance_Resistivity(minR, length, area);
        double resistanceMax = Resistance_Resistivity(maxR, length, area);

        Console.WriteLine($"{material} Resistance Range: {resistanceMin} to {resistanceMax} Ω");
    }

    // Calculate resistance using resistivity
    protected static double Resistance_Resistivity(double ro, double l, double a) {
        return ro * l / a;
    }
}
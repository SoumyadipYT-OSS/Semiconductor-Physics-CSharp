using System;

class OHMs_law {
    static void Main() {
        Console.Write("Enter 'V' to calculate Voltage, 'I' for current, 'R' for resistance: ");
        char choice = char.ToUpper(Console.ReadLine()[0]);

        double v = 0, i = 0, r = 0;
        
        switch (choice) {
            case 'V':
                Console.Write("Enter 'I': ");
                i = double.Parse(Console.ReadLine());
                Console.Write("Enter 'R': ");
                r = double.Parse(Console.ReadLine());
                Console.WriteLine($"Voltage: {i * r} volts");
                break;

            case 'I':
                Console.Write("Enter 'V': ");
                v = double.Parse(Console.ReadLine());
                Console.Write("Enter 'R': ");
                r = double.Parse(Console.ReadLine());
                Console.WriteLine($"Current: {v/r} amperes");
                break;

            case 'R':
                Console.Write("Enter 'V': ");
                v = double.Parse(Console.ReadLine());
                Console.Write("Enter 'I': ");
                i = double.Parse(Console.ReadLine());
                Console.WriteLine($"Resistance: {v/i} ohms");
                break;

            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}
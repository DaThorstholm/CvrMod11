namespace CvrMod11;

public static class Mod11
{
    public static bool CheckMod11(int number)
    {
        return CheckMod11(number.ToString());
    }
    
    public static bool CheckMod11(string numberAsString)
    {
        if (numberAsString.Length > 8)
        {
            throw new ArgumentException($"{nameof(numberAsString)} must be 8 char's or less.");
        }
        
        var numberAsArray = numberAsString
            .PadLeft(8, '0')
            .ToCharArray()
            .Select(s => s - '0')
            .ToArray();
        
        return CheckMod11(numberAsArray);
    }
    
    private static bool CheckMod11(IReadOnlyList<int> numberAsArray) 
    {
        var cvrMod11Factors = new [] { 2, 7, 6, 5, 4, 3, 2 };

        var sum = 0;
        for (var i = 0; i < 7; i++)
        {
            var mul = numberAsArray[i] * cvrMod11Factors[i];
            sum += mul;
        }

        var mod11Value = sum % 11;
        
        var controlDigit = (11 - mod11Value) % 11;

        return controlDigit == numberAsArray[^1];
    }

    public static int EazyToRemember(string str)
    {
        var digits = new List<Digit>();
        
        var distinctChars = str.Distinct();
        foreach (var c in distinctChars)
        {
            var digit = new Digit
            {
                Char = c,
                Occurrence = Occurrence(str, c),
                Concurrent = Concurrent(str, c),
            };
            
            digits.Add(digit);
        }
        
        return digits.Count(s => s.Concurrent >= 2 || s.Occurrence >= 2);
    }

    public static bool Reversible(string s)
    {
        return s.SequenceEqual(s.Reverse());
    }

    public static int Occurrence(string s, char c)
    {
        return s.Count(t => t == c);
    }
    
    public static int Concurrent(string s, char c)
    {
        var max = 0;
        var current = 0;

        var previousChar = ' ';
        foreach (var t in s)
        {
            if (t == c && t == previousChar)
            {
                current++;
                
                if (max < current)
                {
                    max = current;
                }
            }
            else
            {
                current = 0;
            }

            previousChar = t;
        }
        
        return max;
    }
}

public class Digit
{
    public char Char { get; set; }
    public int Occurrence { get; set; }
    public int Concurrent { get; set; }
}
using System.Collections.Generic;

public class DataCollections
{
    public  Dictionary<string, List<Dictionary<string, int>>> Collections = new()
    {
        {
        "SetA", new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> { { "one", 1 } },
            new Dictionary<string, int> { { "two", 2 } }
        }
        },
        {
            "SetB", new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "three", 3 } },
                new Dictionary<string, int> { { "four", 4 } }
            }
        },
        {
            "SetC", new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "five", 5 } },
                new Dictionary<string, int> { { "six", 6 } }
            }
        },
        {
            "SetD", new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "seven", 7 } },
                new Dictionary<string, int> { { "eight", 8 } }
            }
        },
        {
            "SetE", new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "nine", 9} },
                new Dictionary<string, int> { { "ten", 10 } }
            }
        },
    
    };
}

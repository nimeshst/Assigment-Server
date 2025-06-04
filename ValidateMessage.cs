
using System.Collections.ObjectModel;

class Validation
{
    public Dictionary<string, List<Dictionary<string, int>>> collections;
    public Validation()
    {
        collections = new DataCollections().Collections;
    }
    public int ValidateMessage(string messages)
    {
        try
        {
            List<string> message = messages.Split("-").ToList();
            string set = message[0];
            string n = message[1];
            if (collections.TryGetValue(set, out List<Dictionary<string, int>> setVal))
            {
                foreach (Dictionary<string, int> count in setVal)
                {
                    if (count.TryGetValue(n, out int m))
                    {
                        return m;
                    }

                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
        
        return -1;
    }
}
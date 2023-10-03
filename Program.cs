using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        List<Tuple<string, List<List<string>>>> data = new List<Tuple<string, List<List<string>>>>();

        try
        {
            string json = File.ReadAllText("Data.json");
            JObject jsonObject = JObject.Parse(json);

            foreach (var property in jsonObject.Properties())
            {
                JArray array = (JArray)property.Value;
                List<List<string>> innerList = new List<List<string>>();

                foreach (var item in array)
                {
                    var innerArray = (JArray)item;
                    List<string> itemData = new List<string>
                    {
                        innerArray[0].ToString(),
                        innerArray[1].ToString(),
                        innerArray[2].ToString()
                    };
                    innerList.Add(itemData);
                }

                data.Add(new Tuple<string, List<List<string>>>(property.Name, innerList));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        /*        foreach (var tuple in data)
                {
                    Console.WriteLine($"Key: {tuple.Item1}");
                    Console.WriteLine("Data:");
                    foreach (var itemData in tuple.Item2)
                    {
                        Console.WriteLine(itemData);
                    }
                    Console.WriteLine();
                }*/

        string input = Console.ReadLine();

        char q = input[0];
        int indexQ = 0;

        
        char nextChar = input[indexQ + 1];

        foreach(var item in data)
        {
            if (item.Item1 == nextChar.ToString())
            {
                for(int i = 0; i < item.Item2[0].Count;i++)
                {
                    if (q.ToString() == i.ToString())
                    {

                        char[] charArray = input.ToCharArray();

                        char temp = charArray[indexQ];
                        charArray[indexQ] = charArray[indexQ + 1];
                        charArray[indexQ + 1] = temp;

                        input = new string(charArray);


                        q = item.Item2[i][0];
                        if (item.Item2[i][2] == "r")
                            indexQ+=2;
                        else
                            indexQ-=2;

                        Console.WriteLine("q" + q);
                        break;
                    }
                }
            }
        }

        Console.WriteLine(input);


    }
}

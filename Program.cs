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
                Console.WriteLine(itemData[0] + " " + itemData[1] + " " + itemData[2]);
            }
        }*/

        string input = Console.ReadLine();

        string q = input[0].ToString();
        int indexQ = 0;
        char nextChar;
        bool flag = true;
        while (flag)
        {
            if (indexQ + 1 < input.Length)
            {
                nextChar = input[indexQ + 1];  
            }
            else
            {
                nextChar = 'b';
                input += 'b';
            }
            foreach (var item in data)
            {
                if (item.Item1 == nextChar.ToString())
                {
                    for (int i = 0; i < item.Item2.Count; i++)
                    {
                        if (q == i.ToString())
                        {
                            if (item.Item2[i][0] == "-")
                            {
                                flag = !flag;
                                break;
                            }
                            q = item.Item2[i][0][1].ToString();
                            Console.WriteLine("q" + q);

                            char[] charArray = input.ToCharArray();
                            charArray[indexQ + 1] = item.Item2[i][1][0];
                            if (item.Item2[i][2] == "r")
                            {
                                char temp = q[0];
                                charArray[indexQ] = charArray[indexQ+1];
                                charArray[indexQ + 1] = temp;
                                indexQ += 1;
                            }
                            else if (item.Item2[i][2] == "l")
                            {
                                char temp = q[0];
                                charArray[indexQ] = charArray[indexQ - 1];
                                charArray[indexQ - 1] = temp;
                                indexQ -= 1;
                            }
                            input = new string(charArray);
                            break;
                        }
                    }
                    break;
                }
            }
            for (int k = 0; k < input.Length; k++)
            {
                if (k == indexQ)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(input[k]);
                Console.ResetColor();
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }

    }
}

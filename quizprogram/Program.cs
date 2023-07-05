using System;
using System.IO;
using System.Linq.Expressions;
using System.Text.Json;

class Program
{
    static void Main()
    {
        try
        {
            string filePath;

           string userinput = Console.ReadLine();
           if (userinput == "1")
            {
                filePath = "questions.json";
            }

           if (userinput == "2")
            {
                filePath = "questions2.json";
            }
           else
            {
                Console.WriteLine("Wrong input please choose by typing in a correct number and pressing 'enter'.");
                goto retry
            }
            
            string jsonString = File.ReadAllText(filePath);
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            JsonElement root = jsonDocument.RootElement;

            // Get the quiz questions using a loop, up to 6 questions.
            for (int i = 1; i <= 6; i++)
            {
                string questionKey = $"spørgsmål{i}";
                if (root.TryGetProperty(questionKey, out JsonElement question))
                {
                    PresentQuestion(question);
                }
            }

            jsonDocument.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    static void PresentQuestion(JsonElement question)
    {
        string spørgsmål = question.GetProperty("spørgsmål").GetString();
        Console.WriteLine(spørgsmål + "\n");

        foreach (JsonElement svar in question.GetProperty("svarmuligheder").EnumerateArray())
        {
            Console.WriteLine(svar.GetString());
        }

        string rigtigtSvar = question.GetProperty("rigtigtSvar").GetString();
        string forkertSvar = question.GetProperty("forkertSvar").GetString();
        Console.Write("Indtast svar: ");
        string brugerSvar = Console.ReadLine();

        if (string.Equals(brugerSvar, rigtigtSvar, StringComparison.OrdinalIgnoreCase))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRigtigt!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nForkert!\n");
            Console.ResetColor();
            Console.WriteLine(forkertSvar);
        }

        Console.WriteLine("\n------------\n");
    }
}

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
            string filePath = "questions.json";
            Console.WriteLine("Quiz 1 or 2? Type '1' or '2' and press enter to choose");
            string userinput = Console.ReadLine();
           if (userinput == "1")
            {
                filePath = "questions.json";
            }

           else if (userinput == "2")
            {
                filePath = "questions2.json";
            }
           else
            {
                Console.WriteLine("Wrong input please choose by typing in a correct number and pressing 'enter'.");
                
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
            //Dispose of the used resources.
            jsonDocument.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    //looks for the JsonElemement 'question'
    static void PresentQuestion(JsonElement question)
    {
        //It now looks for the property 'spørgsmål' and gets the string
        string spørgsmål = question.GetProperty("spørgsmål").GetString();
        Console.WriteLine(spørgsmål + "\n"); //prints out the s

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

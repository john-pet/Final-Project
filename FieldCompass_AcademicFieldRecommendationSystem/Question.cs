using Spectre.Console;

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    public abstract class Question : FileHandling
    {
        public List<int> AskQuestions(int count)
        {
            List<int> answers = new List<int>();
            bool confirmChoices;
            do
            {
                answers.Clear();
                answers.AddRange(SpecificQuestions(count));
                confirmChoices = ConfirmSelections();
                
            } while (!confirmChoices);
            return answers;
        }
        
        public abstract List<int> SpecificQuestions(int count);

        public static void DisplayOptions(List<string> options)
        {
            Console.WriteLine("Choose from the list of options (choose multiple by entering numbers, e.g., 1,3,5):");
            Console.WriteLine();
            for (int i = 0; i < options.Count; i++)
            {
                AnsiConsole.MarkupLine($"[cyan]({i + 1})[/] {options[i]}");
            }
            Console.WriteLine();
        }

        internal static List<int> SelectedOptions(string category, List<string> options)
        {
            int repeat;
            List<int> selectedOptions;
            do
            {
                repeat = 0;
                string input = Console.ReadLine(); //this is where your input like 1,2,3 gets stored as a string
                selectedOptions = new List<int>();

                /*
                    With input.Split(',') i will split the the previous inputted string like 1,2,3 to an array of substrings containing like ["1", " 2", " 3"]
                    Then it will loop to each substring and store each of them to the variable number
                */
                foreach (var number in input.Split(','))
                {
                    if (int.TryParse(number.Trim(), out int index) && index >= 1 && index <= options.Count)
                    {
                        if (!selectedOptions.Contains(index))
                        {
                            selectedOptions.Add(index); // Avoid duplicates
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[bold red]Invalid input detected: '{number}'. Please enter numbers between 1 and {options.Count}.[/]\n");
                        repeat = 1;
                    }
                }
            } while (repeat == 1);
            

            CreateFile(category, selectedOptions, options);

            return selectedOptions;
        }
        
        public static bool ConfirmSelections()
        {
            AnsiConsole.MarkupLine("\n[yellow]Confirm Choices? (Yes/No)[/]");
            string confirmation = Console.ReadLine();

            if (confirmation.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (confirmation.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                return false;
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Invalid Input. Please only answer Yes or No[/]");
                AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                Console.ReadKey();
                Console.Clear();
                return false;
            }


        }
    }
}

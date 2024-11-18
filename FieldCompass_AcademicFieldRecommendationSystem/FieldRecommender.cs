using Spectre.Console;
using System.Linq;


namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class FieldRecommender
    {
        internal static List<AcademicField> RecommendFields(UserProfile userProfile, List<AcademicField> fields)
        {
            List<AcademicField> recommendedCourses = new List<AcademicField>();

            foreach (var field in fields)
            {
                float count = 0;
                bool interestOneMatch = MatchesAny(field.InterestsOptionsOne, userProfile.InterestsAnswersOne);
                bool interestTwoMatch = MatchesAny(field.InterestsOptionsTwo, userProfile.InterestsAnswersTwo);
                bool passionOneMatch = MatchesAny(field.PassionsOptionsOne, userProfile.PassionsAnswersOne);
                bool skillstrengthOneMatch = MatchesAny(field.SkillsAndStrengthsOptionsOne, userProfile.SkillsAndStrengthsAnswersOne);
                bool skillstrengthTwoMatch = MatchesAny(field.SkillsAndStrengthsOptionsTwo, userProfile.SkillsAndStrengthsAnswersTwo);
                bool skillstrengthThreeMatch = MatchesAny(field.SkillsAndStrengthsOptionsThree, userProfile.SkillsAndStrengthsAnswersThree);

                if (interestOneMatch) count++;
                if (interestTwoMatch) count++;
                if (passionOneMatch) count++;
                if (skillstrengthOneMatch) count++;  
                if (skillstrengthTwoMatch) count++;
                if (skillstrengthThreeMatch) count++;
                
                if(count >= 3)
                {
                    field.MatchPercentage = (count / 6) * 100;
                    recommendedCourses.Add(field);
                }
                    
            }
            return recommendedCourses;
        }

         
        private static bool MatchesAny(int[] options, List<int> answers)
        {
            return answers.Any(answer => Array.Exists(options, option => option.Equals(answer)));
        }

        public static void DisplayRecommendations(List<AcademicField> recommendedCourses)
        {
            if (recommendedCourses.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No field matches both your skills and passions.[/]");
                return;
            }

            // Sort courses based on MatchPercentage
            recommendedCourses.Sort((course1, course2) => course2.MatchPercentage.CompareTo(course1.MatchPercentage));

            // Fill the menu with the recommended fields
            List<string> menu = new List<string>();
            foreach (var field in recommendedCourses)
            {
                menu.Add(field.Name);
            }
            menu.Add("Back");  // Add a "Back" option

            bool running = true;
            int selectedOption = 0;

            while (running)
            {
                // Clear the console before displaying the updated table
                Console.Clear();

                CenterTexts.TextCenterer("Use Up/Down arrows to navigate and Enter to select.", "yellow");

                // Create the table with 2 rows: Field Name and Additional Info
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[bold][green]Field Name[/][/]")  // First column: Field Name
                    .AddColumn("[bold][green]Additional Info[/][/]")  // Second column: Additional Info
                    .Expand();

                // Initialize column content
                string column1 = "";
                string column2 = "";

                // Loop through the menu to build column1 content
                for (int i = 0; i < menu.Count; i++)
                {
                    if (i == selectedOption)
                    {
                        // Highlight the selected option
                        column1 += $"[bold cyan]> {menu[i]}[/]\n";
                    }
                    else
                    {
                        column1 += $"{menu[i]}\n";
                    }
                }

                // Determine the content of column2 based on the selected option
                if (selectedOption < recommendedCourses.Count)
                {
                    var selectedField = recommendedCourses[selectedOption];
                    column2 = $"[bold cyan]{selectedField.Name}[/]\n\n" +
                              $"[bold yellow]Match Percentage:[/] {selectedField.MatchPercentage:F0}%\n\n" +
                              $"[bold yellow]Summary:[/] {selectedField.FieldDetails}\n\n" +
                              $"[bold yellow]Possible Career Paths:[/] {selectedField.CareerPaths}";
                }
                else // "Back" option selected
                {
                    column2 = "[bold yellow]Press ENTER to return to the previous menu.[/]";
                }
                
                table.AddRow(column1.TrimEnd(), column2);

                // Display the table with dynamic padding
                int consoleWidth = Console.WindowWidth;
                int tableWidth = consoleWidth - 10; // Full screen width with padding of 5 on each side
                int padding = Math.Max((consoleWidth - tableWidth) / 2, 0);
                AnsiConsole.Write(new Padder(table, new Padding(padding, 0, padding, 0)));

                // Read user input for navigation
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = (selectedOption == 0) ? menu.Count - 1 : selectedOption - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = (selectedOption == menu.Count - 1) ? 0 : selectedOption + 1;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (selectedOption == menu.Count - 1) // "Back" selected
                    {
                        running = false; // Exit the loop
                    }
                }
            }
        }




    }
}

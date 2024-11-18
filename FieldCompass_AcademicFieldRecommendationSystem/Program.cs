using Spectre.Console;

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class Program : FileHandling
    {
        static void Main(string[] args)
        {
            Message.WelcomeMessage();

            Console.Clear();
            UserProfile userProfile = new UserProfile();

            bool running = true;
            int selectedOption = 0;
            string[] mainMenu = { "Interests", "Passion", "Skills and Strengths", "Exit" };
            int[] visited = { 0, 0, 0 }; // an array that tracks if the questions are answered

            while (running)
            {
                Console.Clear();
                int displayRecommend = 0;

                // This tests if all questions are answered
                for (int i = 0; i < visited.Length; i++)
                {
                    if (visited[i] == 0)
                    {
                        displayRecommend = 0;
                        break; // break immediately because it means that at least one question is not answered
                    }
                    else
                    {
                        displayRecommend = 1;
                    }
                }

                if (displayRecommend == 1)
                {
                    Array.Resize(ref mainMenu, 6); // Expand the menu to include additional options
                    mainMenu[3] = "Display Answers";
                    mainMenu[4] = "Recommend Fields";
                    mainMenu[5] = "Exit";
                }

                // Instruction for navigation with yellow color
                CenterTexts.TextCenterer("Use Up/Down arrows to navigate and Enter to select.\n", "yellow");

                for (int i = 0; i < mainMenu.Length; i++)
                {
                    string text = mainMenu[i];

                    // Highlight the selected option in cyan, the rest in white
                    if (i == selectedOption)
                    {
                        CenterTexts.TextCenterer("==> " + text, "cyan"); // Highlight the selected item in cyan
                    }
                    else
                    {
                        CenterTexts.TextCenterer(text, "white"); // Default color for other items
                    }
                }

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = (selectedOption == 0) ? mainMenu.Length - 1 : selectedOption - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = (selectedOption == mainMenu.Length - 1) ? 0 : selectedOption + 1;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    switch (selectedOption)
                    {
                        case 0:
                            // Handling Interests
                            mainMenu[selectedOption] = "Interests (Edit Answers)";
                            visited[selectedOption] = 1;
                            Interests interests = new Interests();
                            userProfile.InterestsAnswersOne = interests.AskQuestions(1);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            userProfile.InterestsAnswersTwo = interests.AskQuestions(2);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 1:
                            // Handling Passions
                            mainMenu[selectedOption] = "Passions (Edit Answers)";
                            visited[selectedOption] = 1;
                            Passions passions = new Passions();
                            userProfile.PassionsAnswersOne = passions.AskQuestions(1);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            // Handling Skills and Strengths
                            mainMenu[selectedOption] = "Skills and Strengths (Edit Answers)";
                            visited[selectedOption] = 1;
                            SkillsStrengths skillsStrengths = new SkillsStrengths();
                            userProfile.SkillsAndStrengthsAnswersOne = skillsStrengths.AskQuestions(1);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            userProfile.SkillsAndStrengthsAnswersTwo = skillsStrengths.AskQuestions(2);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            userProfile.SkillsAndStrengthsAnswersThree = skillsStrengths.AskQuestions(3);
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 3:
                            if (displayRecommend == 0)
                            {
                                running = false; // Exit the loop and end the program
                                DeleteFile();
                                Message.ExitMessage();
                                break;
                            }
                            else
                            {
                                ReadFile();
                                AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                                Console.ReadKey();
                                break;
                            }
                        case 4:
                            // Instantiating an object of class FieldDatabase
                            FieldDatabase fieldDatabase = new FieldDatabase();
                            // This will retrieve the information from the database that we made about the fields and their related information
                            List<AcademicField> fields = fieldDatabase.InitializeFieldDatabase();

                            // RecommendField() is a static method which can be called directly without creating an instance of the class FieldRecommender
                            // This will pass the userProfile (including skills and passions) and fields to the recommender
                            List<AcademicField> recommendedFields = FieldRecommender.RecommendFields(userProfile, fields);

                            // Display recommendations
                            FieldRecommender.DisplayRecommendations(recommendedFields);
                            break;
                        case 5:
                            running = false; // Exit the loop and end the program
                            DeleteFile();
                            Message.ExitMessage();
                            break;
                    }
                }
            }
        }
    }
}

using Spectre.Console;

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    public class FileHandling
    {
        // Set the base directory to the project root, and specify the "Answers" folder
        static readonly string baseDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Answers"));
        static readonly List<string> fileNames = new List<string>
        {
            "InterestsAnswersOne",
            "InterestsAnswersTwo",
            "PassionsAnswersOne",
            "SkillsStrengthsAnswersOne",
            "SkillsStrengthsAnswersTwo",
            "SkillsStrengthsAnswersThree",
        };

        internal static void CreateFile(string category, List<int> selectedOptions, List<string> options)
        {
            string filePath = Path.Combine(baseDirectory, $"{category}.txt");

            // Ensure the "Answers" folder exists
            Directory.CreateDirectory(baseDirectory);

            AnsiConsole.MarkupLine("\n[yellow]Choices: [/]");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (int num in selectedOptions)
                    {
                        string optionText = $"[cyan]{num}[/] - {options[num - 1]}";
                        AnsiConsole.MarkupLine(optionText);
                        writer.WriteLine(optionText);
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                AnsiConsole.MarkupLine($"[bold red]Directory not found: {ex.Message}[/]");
            }
            catch (ArgumentException ex)
            {
                AnsiConsole.MarkupLine($"[bold red]Invalid path: {ex.Message}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[bold red]Failed to create the file: {ex.Message}[/]");
            }
        }

        internal static void ReadFile()
        {
            int count = 0;
            foreach (string fileName in fileNames)
            {
                if (count == 0)
                {
                    AnsiConsole.MarkupLine("[bold green]Interest[/]");
                }
                else if (count == 2)
                {
                    AnsiConsole.MarkupLine("[bold green]Passions[/]");
                }
                else if(count == 3)
                {
                    AnsiConsole.MarkupLine("[bold green]Skills and Strengths[/]");
                }
                string filePath = Path.Combine(baseDirectory, $"{fileName}.txt");
                try
                {
                    using (StreamReader reader = File.OpenText(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            AnsiConsole.MarkupLine(line);
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    AnsiConsole.MarkupLine($"[bold red]File not found: {fileName}.txt[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]Failed to read the file {fileName}: {ex.Message}[/]");
                }
                Console.WriteLine();
                count++;
            }
        }

        internal static void DeleteFile()
        {
            foreach (string fileName in fileNames)
            {
                string filePath = Path.Combine(baseDirectory, $"{fileName}.txt");
                try
                {
                    File.Delete(filePath);
                }
                catch (FileNotFoundException)
                {
                    AnsiConsole.MarkupLine($"[bold red]File not found: {fileName}.txt[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]An error occurred while deleting the file {fileName}: {ex.Message}[/]");
                }
            }
        }
    }
}

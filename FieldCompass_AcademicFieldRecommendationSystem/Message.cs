using Spectre.Console;
using System.Text.RegularExpressions;

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class Message
    {
        internal static void WelcomeMessage()
        {
            Console.Clear();

            // Display an enlarged, blinking title "FIELD COMPASS" until the user presses a key
            bool blinking = true;
            int blinkInterval = 500; // Adjust blinking speed
            while (blinking)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("\n\n\n");

                // The "FIELD COMPASS" title in large font with blinking effect (green and cyan alternating colors)
                var color = (DateTime.Now.Millisecond / blinkInterval) % 2 == 0 ? Color.Green : Color.Cyan3;

                AnsiConsole.Write(
                    new FigletText("FIELD COMPASS")
                        .Centered()
                        .Color(color));  // Alternate colors based on time

                Thread.Sleep(blinkInterval);  // Adjust the blinking speed (500ms interval)

                // Check for key press to stop blinking
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);  // Clear the key press buffer
                    blinking = false;       // Stop the blinking when a key is pressed
                }
            }

            // Display the introduction with simple centered text
            DisplayCenteredText("Discover your path, unlock your potential.");
            Thread.Sleep(1000);

            DisplayCenteredText("Let us guide you through the exciting world of academia!");
            Thread.Sleep(1000);

            DisplayCenteredText("Explore tailored recommendations based on your passions and strengths.");
            Thread.Sleep(1000);

            DisplayCenteredText("Get ready to embark on a journey to find the perfect field for you.");
            Thread.Sleep(1000);

            // Proceed directly to the next message
            DisplayCenteredText("Are you ready to begin?");
            Thread.Sleep(1000);
            DisplayCenteredText("[bold yellow]Press any key to start your adventure![/]");

            Console.ReadKey();
        }

        internal static void ExitMessage()
        {
            Console.Clear();

            // Display an enlarged, blinking title "GOODBYE" until the user presses a key
            bool blinking = true;
            while (blinking)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("\n\n\n");

                // The "GOODBYE" title in large font with blinking effect (red and yellow alternating colors)
                AnsiConsole.Write(
                    new FigletText("THANK YOU")
                        .Centered()
                        .Color(blinking ? Color.Red : Color.Yellow));  // Alternate colors for blinking effect
                Thread.Sleep(500);  // Adjust the blinking speed (500ms interval)

                // Check for key press to stop blinking
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);  // Clear the key press buffer
                    blinking = false;       // Stop the blinking when a key is pressed
                }
            }

            // Display the exit message with simple centered text
            DisplayCenteredText("Thank you for using FieldCompass!");
            Thread.Sleep(1000);

            DisplayCenteredText("We hope you found the perfect field for your future. Goodbye!");
            Thread.Sleep(1000);

            DisplayCenteredText("[bold yellow]Press any key to exit...[/]");

            Console.ReadKey();
        }

        private static void DisplayCenteredText(string text)
        {
            // Strip formatting tags to get the correct length of the text
            string strippedText = Regex.Replace(text, @"\[[^\]]*\]", "");

            // Calculate the number of spaces to center the text
            int spaces = (Console.WindowWidth - strippedText.Length) / 2;

            // Create the centered text and write it to the console
            var paddedText = new string(' ', spaces) + text;  // Centering the text manually
            AnsiConsole.MarkupLine(paddedText);  // Use MarkupLine for formatted text output
        }
    }
}

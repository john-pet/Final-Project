using System.Text;
using Spectre.Console;

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class CenterTexts
    {
        //this is a class that helps center texts or paragraphs
        internal static void TextCenterer(string text, string color = "white")
        {
            int consoleWidth = Console.WindowWidth;
            int textWidth = text.Length;

            // Calculate the number of spaces needed to center the text
            int numberOfSpaces = (consoleWidth - textWidth) / 2;
            if (numberOfSpaces < 0) numberOfSpaces = 0;

            // Use AnsiConsole.Markup for color formatting
            string centeredText = new string(' ', numberOfSpaces) + $"[{color}]{text}[/]";
            AnsiConsole.MarkupLine(centeredText);
        }
    }
}

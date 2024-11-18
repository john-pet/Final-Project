namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class Passions : Question
    {
        private static List<string> PassionsAnswersOne = new List<string>
        {
            "Participating in hands-on projects", "Learning about new technologies", "Solving technical problems",
            "Creating or building things", "Volunteering in community events", "Engaging in physical activities",
            "Conducting experiments", "Exploring the outdoors", "Participating in science-related activities",
            "Engaging in business activities", "Engaging in stocks, cryptocurrencies, trading", "Helping others",
            "Participating in advocacies and change", "Creating art or literature",
            "Participating or attending cultural events", "Exploring various forms of media",
            "Solving puzzles and problems", "Engaging in collaborative discussions", "Traveling to other places"
        };

        public override List<int> SpecificQuestions(int count)
        {
            List<int> answers = new List<int>();

            string category = "PassionsAnswersOne";
            Console.WriteLine("Which of these aligns with your long-term goals?");
            DisplayOptions(PassionsAnswersOne);
            answers.AddRange(SelectedOptions(category, PassionsAnswersOne));

            return answers;
        }
    }
}

using Newtonsoft.Json; // Ensure to include Newtonsoft.Json for JSON deserialization

namespace FieldCompass_AcademicFieldRecommendationSystem
{
    internal class FieldDatabase
    {
        // Path to the JSON file containing field details
        static readonly string FilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\FieldDatabase.txt"));
        // Path to the JSON file containing field details

        // This method returns a list of field objects read from a JSON file
        internal List<AcademicField> InitializeFieldDatabase()
        {
            try
            {
                // Read the file content
                string jsonContent = File.ReadAllText(FilePath);

                // Deserialize JSON content to a list of Field objects
                List<AcademicField> fieldsDetails = JsonConvert.DeserializeObject<List<AcademicField>>(jsonContent);

                if (fieldsDetails == null)
                {
                    return new List<AcademicField>();
                }
                else
                {
                    return fieldsDetails;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file '{FilePath}' was not found.");
                Console.ReadLine();
                return new List<AcademicField>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error parsing JSON content: {jsonEx.Message}");
                return new List<AcademicField>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the field database: {ex.Message}");
                Console.ReadLine();
                return new List<AcademicField>();
            }
        }
    }
}
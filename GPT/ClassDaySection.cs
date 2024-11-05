using Newtonsoft.Json;
using System.Text;

namespace GPT
{
    public class Day
    {
        public int day { get; set; }
        public int difficulty { get; set; }
        public int unusual { get; set; }

        public string description { get; set; }
        public string succeed { get; set; }
        public string failed { get; set; }
    }

    public class Days
    {
        public List<Day> days { get; set; }
    }

    public class ClassDaySection
    {
        public static async Task CreateDaysJson(SchoolClass theClass, string modifier = ".")
        {
            var filename = $"./{theClass.name.Replace(" ", "_")}_days.json";
            string exceptions = "";

            if (File.Exists(filename))
            {
                var lines = File.ReadAllLines(filename).Where(x => !string.IsNullOrEmpty(x)).ToList();
                foreach (var dayJson in lines)
                {
                    var day = JsonConvert.DeserializeObject<Day>(dayJson);
                    exceptions += @$"""{day.description}"",";
                }
            }

            if (exceptions != "")
            {
                exceptions = "The description should not be like any of the following: " + exceptions;
            }

            var request = await GPTRequestBuilder.DefaultPersona()
            .AddContent(Prompts.GetTimeline())
            .AddContent(Prompts.GetCountries())
            .AddContent(Prompts.GetSecrets())
            .AddContent(Prompts.GetOlive())
            .AddContent(Prompts.GetAttributes())
            .AddContent(GetClassTaskPrompt($"an art class on {theClass.name}", "an old friendly man", modifier, exceptions))
            .FormatAsJSON()
            .Build()
            .Run<Days>();

            foreach (var day in request.days)
            {
                File.AppendAllText(filename, JsonConvert.SerializeObject(day) + "\n");
            }
        }

        public static void DaysToParseDays(SchoolClass theClass)
        {
            var name = theClass.name.Replace(" ", "_");
            var filename = $"./{name}_days.json";

            var filenameOut = $"./{name}_parse_days.json";

            var lines = File.ReadAllLines(filename).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var days = new List<Day>();
            foreach (var dayJson in lines)
            {
                days.Add(JsonConvert.DeserializeObject<Day>(dayJson));
            }

            days = days.OrderBy(x => x.difficulty).ThenBy(x => x.unusual).ToList();

            var text = new StringBuilder();

            foreach (var day in days)
            {
                day.succeed = ClassPrompts.Parse(day.succeed, day.description);
                day.failed = ClassPrompts.Parse(day.failed, day.description);
                text.AppendLine(JsonConvert.SerializeObject(day));
            }

            File.WriteAllText(filenameOut, text.ToString());

            for (var i = 1; i <= 3; i++)
            {
                Console.WriteLine($"Difficulty {i}: {days.Count(x => x.difficulty == i)}");
            }

            for (var i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Rarity {i}: {days.Count(x => x.unusual == i)}");
            }
        }

        public static void ParseDaysToSections(SchoolClass theClass)
        {
            var name = theClass.name.Replace(" ", "_");
            var lines = File.ReadAllLines($"./{name}_parse_days.json").Where(x => !string.IsNullOrEmpty(x)).ToList();

            var days = new List<Day>();
            foreach (var dayJson in lines)
            {
                days.Add(JsonConvert.DeserializeObject<Day>(dayJson));
            }

            Console.WriteLine($"Days: {days.Count}");

            var theText = new StringBuilder();

            foreach (var day in days.OrderBy(x => x.difficulty).ThenBy(x => x.unusual))
            {
                if ((day.succeed.StartsWith(day.description) && day.failed.StartsWith(day.description))
                    || (!day.succeed.StartsWith("Olive") && !day.failed.StartsWith("Olive")))
                {
                    var description = day.description.Replace(" ", "_");
                    var difficult = day.difficulty;
                    var progress = "Rule: progress >= 70";
                    if (day.difficulty == 1)
                    {
                        progress = "Rule: progress <= 15";
                    }
                    if (day.difficulty == 2)
                    {
                        progress = "Rule: progress > 15\nRule: progress < 70";
                    }
                    var rarity = "common";
                    if (day.unusual == 2)
                    {
                        rarity = "uncommon";
                    }
                    if (day.unusual == 3)
                    {
                        rarity = "rare";
                    }
                    if (day.unusual == 4)
                    {
                        rarity = "epic";
                    }
                    if (day.unusual == 5)
                    {
                        rarity = "legendary";
                    }

                    var section = $@"
# section: scenario_{description}
Name: {description}
{progress}
LocationImage: DayOutcomes/{name}_location
ActionImage: DayOutcomes/{name}_action
ActionText: {day.description.Replace("Olive", "!name!")} ...
SuccessText: {ClassPrompts.Parse(day.succeed, day.description)}
SuccessImage: DayOutcomes/{name}_success
FailureText: {ClassPrompts.Parse(day.failed, day.description)}
FailureImage: DayOutcomes/{name}_failure
Rarity: {rarity}

";

                    theText.Append(section);
                }

                var filename = $"./{theClass.name.Replace(" ", "_")}_sections.json";
                File.WriteAllText(filename, theText.ToString());
            }
        }

        public static string GetClassTaskPrompt(string name, string teacher, string modifier = ".", string exceptions = "")
        {
            Console.WriteLine(exceptions);

            return $@"There is a class about {name} which is taught by {teacher}.

Olive attends this class every day for 30 days{modifier}
For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words with one version where she succeeds and one where she fails. 
Use the present tense.
Both versions should start with the same 3-6 words. The description is these words. The description should start with ""Olive"".
{exceptions}

Rate how difficult it is between 1 and 3 where 1 is beginner, 2 is intermediate and 3 is advanced.
Rate how ununsual it is between 1 and 5 where 1 is very common and 5 is very unlikely.

Return in JSON format like this example: {{ days: [ {{ day, description, succeed, failed, difficulty, unusual }} ]  }}
";
        }
    }
}

// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words. Start this sentence with 4-6 words, then complete this sentence with one version where she succeeds in that task and one where she fails.
// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words  one version where she succeeds and one where she fails. On each day, both versions should start with the same 5 words.

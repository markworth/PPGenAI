using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GPT
{
    public class AttributeValue
    {
        public string attribute { get; set; }
        public int value { get; set; }
    }


    public class ClassDialogue
    {
        public List<Dialogue> fromtheheart { get; set; }
        public List<Dialogue> matteroffact { get; set; }
        public List<Dialogue> whimsical { get; set; }
        public List<Dialogue> pompous { get; set; }
        public List<Dialogue> newcomer { get; set; }
        public List<Dialogue> firstClass { get; set; }
        public List<Dialogue> welcomeBack { get; set; }
        public List<Dialogue> backToClass { get; set; }
        public List<Dialogue> regular { get; set; }
        public List<Dialogue> fewTimes { get; set; }
        public AttributeValue learntnothing { get; set; }
        public AttributeValue learntsomething { get; set; }
        public AttributeValue learntEverything { get; set; }
        public string mostImportant { get; set; }
        public int stress { get; set; }
        public int difficulty { get; set; }
    }

    public class ClassPrompts
    {
        public static string Parse(string text, string description)
        {
            var parsed = text.Replace(description, "").Replace(", but", " but");
            if (parsed.StartsWith(","))
            {
                parsed = parsed.Substring(1);
            }

            return parsed.Trim();
        }

        public static void DialogueToInkDialogue(SchoolClass theClass)
        {
            var filename = $"./{theClass.name.Replace(" ", "_")}_dialog.json";
            if (File.Exists(filename))
            {
                var dialogs = JsonConvert.DeserializeObject<List<ClassDialogue>>(File.ReadAllText(filename));

                var theText = new StringBuilder();

                theText.AppendLine("Intros:");
                var whimsical = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.whimsical); return x; });
                var pompous = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.pompous); return x; });
                var newcomer = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.newcomer); return x; });
                var fromtheheart = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.fromtheheart); return x; });
                var matteroffact = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.matteroffact); return x; });
                var welcomeBack = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.welcomeBack); return x; });
                var backToClass = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.backToClass); return x; });

                theText.Append(@$"{{ taken > 1 and random >= 0 and random <= 100:
{{ shuffle:
{Dialogue.GetShuffleInkFromDialog(whimsical)}
{Dialogue.GetShuffleInkFromDialog(pompous)}
{Dialogue.GetShuffleInkFromDialog(fromtheheart)}
{Dialogue.GetShuffleInkFromDialog(matteroffact)}
{Dialogue.GetShuffleInkFromDialog(welcomeBack)}
{Dialogue.GetShuffleInkFromDialog(backToClass)}
}}
}}
");

                theText.AppendLine("firstClass:");
                var firstClass = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.firstClass); return x; });
                theText.Append(@$"{{ taken == 1 and random >= 0 and random <= 100:
{Dialogue.GetShuffleInkFromDialog(newcomer)}
}}
}}
");
                theText.Append(@$"{{ taken == 1 and random >= 0 and random <= 100:
{Dialogue.GetInkFromDialog(firstClass)}
}}
");

                var regular = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.regular); return x; });
                var fewTimes = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.fewTimes); return x; });
                theText.Append(@$"{{ progress > 70 and random >= 0 and random <= 100:
{Dialogue.GetInkFromDialog(fewTimes)}
");

                theText.Append(@$"{{ progress > 70 and random >= 0 and random <= 100:
{{ shuffle:
{Dialogue.GetShuffleInkFromDialog(regular)}
}}
}}
");

                var output = $"./{theClass.name.Replace(" ", "_")}_inkdialog.txt";
                File.WriteAllText(output, theText.ToString());
            }
        }

        public static async Task CreateClassDialog(SchoolClass theClass)
        {
            var filename = $"./{theClass.name.Replace(" ", "_")}_dialog.json";
            var dialogs = new List<ClassDialogue>();

            if (File.Exists(filename))
            {
                dialogs = JsonConvert.DeserializeObject<List<ClassDialogue>>(File.ReadAllText(filename));
            }

            var request = await GPTRequestBuilder.DefaultPersona()
            .AddContent(Prompts.GetTimeline())
            .AddContent(Prompts.GetCountries())
            .AddContent(Prompts.GetSecrets())
            .AddContent(Prompts.GetOlive())
            .AddContent(Prompts.GetAttributes())
            .AddContent(GetClassDialogue($"an art class on {theClass.name}", "an old friendly man"))
            .FormatAsJSON()
            .Build()
            .Run<ClassDialogue>();

            dialogs.Add(request);

            File.WriteAllText(filename, JsonConvert.SerializeObject(dialogs));
        }

        public static string GetClassDialogue(string name, string teacher)
        {
            return $@"There is a class about {name} which is taught by {teacher}.

In the dialog lines, make it relevant for this class. The dialog should be in the style of Gearge R Martin but must be family friendly. Olive may participate. She does not call the teacher sir or madam.

Write 4-8 lines of dialog from the teacher introducing the class. This introduction should be whimsical.
Write 4-8 lines of dialog from the teacher introducing the class. This introduction should be matter of fact.
Write 4-8 lines of dialog from the teacher introducing the class. This introduction should be from the heart.
Write 4-8 lines of dialog from the teacher introducing the class. This introduction should be pompous.
Write 2-5 lines of dialog from the teacher introducing the class to a newcomer.

Write 2-5 lines of dialog between the teacher and Olive when she takes her first class. This should be between 20-30 words and introduce the subject. Olive should have at least 1 line.
Write 2-5 lines of dialog from the teacher welcoming Olive back to the class. 
Write 2-5 lines of dialog between Olive and the teacher when she comes back to class. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive is a regular to the class. 
Write 2-5 lines of dialog between Olive and the teacher after Olive has attended a few times. This should be between 20-30 words Olive should have at least 1 line.

Describe what attributes might change for Olive if:
* she learnt nothing in the class
* she learnt something in the class
* she learnt everything in the class

Examples of an attribute change: BRV 1, CHM -1

What attribute would be most important for this class?

On a rating of 1-5, how stressful is this class?
On a rating of 1-10, how difficult is this class?

Return in JSON format like this example: {{ regular: [  {{ person, text }} ] ], fewTimes: [  {{ person, text }} ] ], matteroffact: [  {{ person, text }} ] ], fromtheheart: [  {{ person, text }} ] ], whimsical: [  {{ person, text }} ] ], pompous: [  {{ person, text }} ] ], newcomer: [  {{ person, text }} ] ], firstClass: [  {{ person, text }} ] ], welcomeBack: [  {{ person, text }} ] ], backToClass: [  {{ person, text }} ] ], learntnothing: {{ attribute, value }}, learntsomething: {{ attribute, value }}, learntEverything: {{ attribute, value }}, mostimportant, stress, difficulty }}
";



//In the dialog lines, make it relevant for this class. The dialog should be in the style of Gearge R Martin but must be family friendly. Olive may participate. She does not call the teacher sir or madam.

//Olive attends this class every day for 30 days. 
//For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words with one version where she succeeds and one where she fails. Both versions should start with the same 5 words.


//Describe what attributes might change for Olive if:
//* she learnt nothing in the class
//* she learnt something in the class
//* she learnt everything in the class

//Describe what attribute would be most important for this class.

//On a rating of 0-5, how stressful is this class?
//On a rating of 0-10, how difficult is this class?

//Write a 1 sentence description of what she might do in the class when she had completed everything the course had to offer of 8-15 words.
//Starting with the same 4-6 words, have one version where she succeeds and one where she fails.

//Return in JSON format like this example: {{ dialog: {{ whimsical: [ dialog: [ {{ person, text }} ] ], pompous: [ dialog: [ {{ person, text }} ] ], newcomer: [ dialog: [ {{ person, text }} ] ], firstClass: [ dialog: [ {{ person, text }} ] ], welcomeBack: [ dialog: [ {{ person, text }} ] ], backToClass: [ dialog: [ {{ person, text }} ] ], regular: [ dialog: [ {{ person, text }} ] ], fewTimes: [ dialog: [ {{ person, text }} ] ], noLongerLearn: [ dialog: [ {{ person, text }} ] ], noLongerPay: [ dialog: [ {{ person, text }} ] ], noLongerPayAngry: [ dialog: [ {{ person, text }} ] ], learntNothing: [ dialog: [ {{ person, text }} ] ], didntLearn: [ dialog: [ {{ person, text }} ] ], learntVeryLittle: [ dialog: [ {{ person, text }} ] ], learntNotMuch: [ dialog: [ {{ person, text }} ] ], learntSomething: [ dialog: [ {{ person, text }} ] ], learntQuiteALot: [ dialog: [ {{ person, text }} ] ], learntEverything: [ dialog: [ {{ person, text }} ] ], learntallTheTopics: [ dialog: [ {{ person, text }} ] ], notInterested: [ dialog: [ {{ person, text }} ] ], distracted: [ dialog: [ {{ person, text }} ] ], stressed: [ dialog: [ {{ person, text }} ] ] }}, days: [ {{ day, succeed, failed }} ], learntnothing: {{ attribute, value }}, learntsomething: {{ attribute, value }}, learntEverything: {{ attribute, value }}, mostimportant, completedSuccess, completedFailed  }}
//";
        }
    }
}

// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words. Start this sentence with 4-6 words, then complete this sentence with one version where she succeeds in that task and one where she fails.
// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words  one version where she succeeds and one where she fails. On each day, both versions should start with the same 5 words.

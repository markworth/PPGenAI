using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GPT
{
    public class TeacherDialogue
    {
        public List<Dialogue> veryWell { get; set; }
        public List<Dialogue> extremelyWell { get; set; }
        public List<Dialogue> noLongerLearn { get; set; }
        public List<Dialogue> noLongerPay { get; set; }
        public List<Dialogue> noLongerPayAngry { get; set; }
        public List<Dialogue> learntNothing { get; set; }
        public List<Dialogue> didntLearn { get; set; }
        public List<Dialogue> learntVeryLittle { get; set; }
        public List<Dialogue> learntNotMuch { get; set; }
        public List<Dialogue> learntSomething { get; set; }
        public List<Dialogue> learntQuiteALot { get; set; }
        public List<Dialogue> learntEverything { get; set; }
        public List<Dialogue> learntallTheTopics { get; set; }
        public List<Dialogue> notInterested { get; set; }
        public List<Dialogue> distracted { get; set; }
        public List<Dialogue> stressed { get; set; }
    }

    public class TeacherPrompts
    {
        public static async Task CreateTeacherDialogJson(string name, string description)
        {
            var filename = $"./{name.Replace(" ", "_")}_dialog.json";
            //string exceptions = "";

            var dialogs = new List<TeacherDialogue>();

            if (File.Exists(filename))
            {
                dialogs = JsonConvert.DeserializeObject<List<TeacherDialogue>>(File.ReadAllText(filename));
            }

            var request = await GPTRequestBuilder.DefaultPersona()
            .AddContent(Prompts.GetTimeline())
            .AddContent(Prompts.GetCountries())
            .AddContent(Prompts.GetSecrets())
            .AddContent(Prompts.GetOlive())
            .AddContent(Prompts.GetAttributes())
            .AddContent(GetTeacherDialogue(description))
            .FormatAsJSON()
            .Build()
            .Run<TeacherDialogue>();

            dialogs.Add(request);

            File.WriteAllText(filename, JsonConvert.SerializeObject(dialogs));
        }

        public static void DialogueToDialogueInk(string name)
        {
            var filename = $"./{name.Replace(" ", "_")}_dialog.json";
            if (File.Exists(filename))
            {
                var dialogs = JsonConvert.DeserializeObject<List<TeacherDialogue>>(File.ReadAllText(filename));

                var theText = new StringBuilder();

                theText.AppendLine("totalfailure:");
                var learntNothing = dialogs.Aggregate(new List<Dialogue>(), (x, y) => { x.AddRange(y.learntNothing); return x; });

                var totalFailure = new List<List<Dialogue>>();
                totalFailure = dialogs.Aggregate(totalFailure, (x, y) => { x.Add(y.learntNothing); return x; });
                totalFailure = dialogs.Aggregate(totalFailure, (x, y) => { x.Add(y.notInterested); return x; });
                totalFailure = dialogs.Aggregate(totalFailure, (x, y) => { x.Add(y.didntLearn); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("totalfailure == 1", totalFailure));

                var totalSuccess = new List<List<Dialogue>>();
                totalSuccess = dialogs.Aggregate(totalSuccess, (x, y) => { x.Add(y.veryWell); return x; });
                totalSuccess = dialogs.Aggregate(totalSuccess, (x, y) => { x.Add(y.extremelyWell); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("totalSuccess == 1", totalSuccess));

                var progress100 = new List<List<Dialogue>>();
                progress100 = dialogs.Aggregate(progress100, (x, y) => { x.Add(y.learntallTheTopics); return x; });
                progress100 = dialogs.Aggregate(progress100, (x, y) => { x.Add(y.learntEverything); return x; });
                progress100 = dialogs.Aggregate(progress100, (x, y) => { x.Add(y.noLongerLearn); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("progress >= 100", progress100));

                var failure = new List<List<Dialogue>>();
                failure = dialogs.Aggregate(failure, (x, y) => { x.Add(y.learntVeryLittle); return x; });
                failure = dialogs.Aggregate(failure, (x, y) => { x.Add(y.learntNotMuch); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("failure == 1", failure));

                var success = new List<List<Dialogue>>();
                success = dialogs.Aggregate(success, (x, y) => { x.Add(y.learntSomething); return x; });
                success = dialogs.Aggregate(success, (x, y) => { x.Add(y.learntQuiteALot); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("success == 1", success));

                var totalfailurestress = new List<List<Dialogue>>();
                totalfailurestress = dialogs.Aggregate(totalfailurestress, (x, y) => { x.Add(y.distracted); return x; });
                totalfailurestress = dialogs.Aggregate(totalfailurestress, (x, y) => { x.Add(y.stressed); return x; });

                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("totalfailurestress == 1", totalfailurestress));

                var noLongerPay = new List<List<Dialogue>>();
                noLongerPay = dialogs.Aggregate(noLongerPay, (x, y) => { x.Add(y.noLongerPay); return x; });

                theText.AppendLine("=== notpaid ===");
                var noLongerPayAngry = new List<List<Dialogue>>();
                noLongerPayAngry = dialogs.Aggregate(noLongerPayAngry, (x, y) => { x.Add(y.noLongerPayAngry); return x; });
                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("days == 0", noLongerPayAngry));
                theText.AppendLine(Dialogue.GetShuffleInkFromDialog("days > 0", noLongerPay));
                theText.AppendLine("-> END");
                theText.AppendLine("");

                Console.WriteLine(theText.ToString());
                var output = $"./{name.Replace(" ", "_")}_inkdialog.txt";
                File.WriteAllText(output, theText.ToString());
            }
        }

        public static string GetTeacherDialogue(string teacher)
        {
            return $@"Olive is taking a class which is taught by {teacher}.
Write 2-5 lines of dialog from the teacher when Olive is a regular to the class. 
Write 2-5 lines of dialog between Olive and the teacher after Olive has attended a few times. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive can no longer learn anything more from the teacher. 
Write 2-5 lines of dialog between Olive and the teacher when Olive has learnt everything from the teacher. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive can no longer pay for the class and the teacher will not continue for free. 
Write 2-5 lines of dialog from the teacher when the teacher is angry that Olive can no longer pay for the class and the teacher will not continue for free. 

Write 2-5 lines of dialog from the teacher when Olive learnt nothing from the class. 
Write 2-5 lines of dialog between Olive and the teacher when Olive didn't learn from the class. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive learnt very little from the class. 
Write 2-5 lines of dialog between Olive and the teacher when Olive learnt not much from the class. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive learnt something from the class. 
Write 2-5 lines of dialog between Olive and the teacher when Olive learnt quite a lot from the class. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog from the teacher when Olive did very well in the class. 
Write 2-5 lines of dialog from the teacher when Olive did extremely well in the class. 

Write 2-5 lines of dialog from the teacher when Olive learnt everything from the class. 
Write 2-5 lines of dialog between Olive and the teacher when Olive learnt all the topics from the class. This should be between 20-30 words Olive should have at least 1 line.

Write 2-5 lines of dialog between Olive and the teacher when she is not interested in the class. This should be between 20-30 words Olive should have at least 1 line.
Write 2-5 lines of dialog between Olive and the teacher when she is distracted. This should be between 20-30 words Olive should have at least 1 line.
Write 2-5 lines of dialog between Olive and the teacher when she is stressed. This should be between 20-30 words Olive should have at least 1 line.

Return in JSON format like this example: {{ veryWell: [  {{ person, text }} ] ], extremelyWell: [  {{ person, text }} ] ], regular: [  {{ person, text }} ] ], fewTimes: [  {{ person, text }} ] ], noLongerLearn: [  {{ person, text }} ] ], noLongerPay: [  {{ person, text }} ] ], noLongerPayAngry: [  {{ person, text }} ] ], learntNothing: [  {{ person, text }} ] ], didntLearn: [  {{ person, text }} ] ], learntVeryLittle: [  {{ person, text }} ] ], learntNotMuch: [  {{ person, text }} ] ], learntSomething: [  {{ person, text }} ] ], learntQuiteALot: [  {{ person, text }} ] ], learntEverything: [  {{ person, text }} ] ], learntallTheTopics: [  {{ person, text }} ] ], notInterested: [  {{ person, text }} ] ], distracted: [  {{ person, text }} ] ], stressed: [  {{ person, text }} ] ] }}
";
        }
    }
}
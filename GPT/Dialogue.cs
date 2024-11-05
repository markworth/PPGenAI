using System.Text;

namespace GPT
{
    public class Dialogue
    {
        public string person { get; set; }
        public string text { get; set; }


        public static string GetShuffleInkFromDialog(string condition, List<List<Dialogue>> dialogue)
        {
            if (dialogue == null || !dialogue.Any()) return "";
            var text = new StringBuilder();

            var shuffles = new List<Dialogue>();
            var conversations = new List<List<Dialogue>>();
            
            foreach(var dial in dialogue.Where(x => x != null))
            {
                var personsSpeaking = dial.Select(x => x.person).Distinct().Count();
                if (personsSpeaking == 1) shuffles.AddRange(dial);
                if (personsSpeaking > 1) conversations.Add(dial);
            }

            if (shuffles.Any())
            {
                text.AppendLine($"{{ {condition} and random >= 0 and random <= 100:");
                text.AppendLine("{ shuffle:");
                foreach(var shuffle in shuffles)
                {
                    text.AppendLine($" - {shuffle.text.Replace("Olive", "!name!").Trim()}");
                }
            }

            foreach(var conversation in conversations)
            {
                var c = new StringBuilder();

                var isCentre = conversation.Select(x => x.person).Distinct().Count() == 1;

                var teacherSide = isCentre ? "centre" : "left";
                var princessSide = isCentre ? "centre" : "left";

                foreach (var d in conversation)
                {
                    if (d == conversation.Last() && d.person == "Teacher")
                    {
                        c.AppendLine("# princess: off");
                        c.AppendLine($"# live2dposition: centre");
                    }
                    else
                    if (d.person.ToLower() == "teacher")
                    {
                        c.AppendLine($"# live2dposition: {teacherSide}");
                    }
                    else
                    {
                        c.AppendLine($"# princess: {princessSide}");
                    }
                    c.AppendLine($"{d.text.Replace("Olive:", "").Replace("Olive", "{player_name}").Replace("Teacher:", "").Trim()}");
                }

                text.AppendLine($" - {c.ToString().Trim()}");
            }

            text.AppendLine("}");

            return text.ToString();
        }

        public static string GetShuffleInkFromDialog(List<Dialogue> dialogue)
        {
            if (dialogue == null || !dialogue.Any()) return "";
            var text = new StringBuilder();
            foreach (var d in dialogue)
            {
                text.AppendLine($" - {d.text.Replace("Olive", "!name!").Trim()}");
            }
            return text.ToString();
        }

        public static string GetInkFromDialog(List<Dialogue> dialogue)
        {
            var text = new StringBuilder();
            if (dialogue == null || !dialogue.Any()) return "";

            // {{ shuffle:

            if (dialogue.Select(x => x.person).Distinct().Count() > 1)
            {
                text.AppendLine("");
                foreach (var d in dialogue)
                {
                    if (d == dialogue.Last() && d.person == "Teacher")
                    {
                        text.AppendLine("# princess: off");
                        text.AppendLine("# live2dposition: centre");
                    }
                    else
                    if (d.person == "Teacher")
                    {
                        text.AppendLine("# live2dposition: left");
                    }
                    else
                    {
                        text.AppendLine("# princess: right");
                    }
                    text.AppendLine($"{d.text.Replace("Olive", "!name!").Trim()}");
                }
            }
            else
            {
                foreach (var d in dialogue)
                {
                    text.AppendLine($" - {d.text.Replace("Olive", "!name!").Trim()}");
                }

                //text.AppendLine($"{{~{dialogue[0].text}");
                //foreach(var d in dialogue.Skip(1))
                //{
                //    text.Append($"|{d.text.Replace("Olive", "!name!").Trim()}");
                //}
                //text.Append("}");
            }

            return text.ToString();
        }
    }
}

// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words. Start this sentence with 4-6 words, then complete this sentence with one version where she succeeds in that task and one where she fails.
// For each day, describe what she does in the classroom in a 1 sentence description of 8-15 words  one version where she succeeds and one where she fails. On each day, both versions should start with the same 5 words.

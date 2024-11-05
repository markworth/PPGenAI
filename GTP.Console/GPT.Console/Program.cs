using GPT;
using Newtonsoft.Json;

//var onlyInMasan = "The entire arc must happen in Masan. All steps are in Masan and they cannot travel anywhere. No travel.";

//var content = Prompts.GetCharacter("female, young, shy, intelligent. 14 years old",
//    "They should initially be likeable, not be proactive and mildly competant.",
//    "proactive and competant doing something small scale whilst staying in Masan using magic.");

//var request = GPTRequestBuilder.DefaultPersona()
//.AddContent(Prompts.GetTimeline())
//.AddContent(Prompts.GetCountries())
//.AddContent(Prompts.GetSecrets())
//.AddContent(content)
//.AddContent(onlyInMasan)
//.AddContent($"Make sure not of the scenarios are similar to the following scenarios: {Prompts.GetArcs()}")
//.FormatAsJSON()
//.Build()
//.Run<Character>();

//File.WriteAllText("./output.json", JsonConvert.SerializeObject(request));

for (var i = 0; i < 20; i++)
{
    //await TeacherPrompts.CreateTeacherDialogJson("artguy", "an old friendly man");
    //Thread.Sleep(TimeSpan.FromSeconds(5));
}
TeacherPrompts.DialogueToDialogueInk("artguy");
return;

var classTaxonomy = JsonConvert.DeserializeObject<Classes>(File.ReadAllText("./classes.json"));

foreach (var artClass in classTaxonomy.art.Skip(1).Take(1))
{
    //await ClassDaySection.CreateDaysJson(artClass);
    //await ClassDaySection.CreateDaysJson(artClass, ". Every day something incredible happens.");

    //await ClassPrompts.CreateClassDialog(artClass);

    ClassPrompts.DialogueToInkDialogue(artClass);

    //ClassPrompts.ParseClassJson(artClass);
    //ClassPrompts.ClassJson2Sections(artClass);
}

Console.ReadKey();
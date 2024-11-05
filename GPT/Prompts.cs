using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class Prompts
    {

        public static string GetTimeline()
        {
            return @"Timeline of the world:
Legend (10000 BFE): Zuha, a hero from Zuhail, ends the second era of darkness. The Knights of the Fallen Star, from Venen, defeat The Evil One and his monsters.
Arrival of Vesunni (530 BFE): The Vesunni people, dragon tamers from the north, migrate to Centralia. Dragons played significant roles in human struggles. Golden dragon ridden by Zuha. The Fallen Star was a dragon used by the Knights.
Subjugation of East Coast (520 BFE): The Vesunni subdue the peoples of the east coast into vassal states.
Ash War (500 BFE - 497 BFE): Conflict sparked by Vesunni attempts to control the Lentia River, resulting in the destruction of the Elen kingdom and the creation of the Lands of Ash.
Colonization of Aruel and Venen (488 BFE): The Vesunni establish colonies in Aruel and Venen.
Empire Expansion under Talimar The Child (480 BFE): The Vesunni Empire expands to include the lands of the Free Ports of Mistdenn.
Creation of the Anointed One (499 BFE): The Vesunni create the figure of the Anointed One, a central leader for governance and military direction.
Last Dragon Born (391 BFE): The final dragon is born from the ceremonial pyres of the Vesunni, an ochre-colored specimen.
Death of Last House Dragon (302 BFE): Soirac, the last ""house dragon,"" dies.
Century of Reforms (246 BFE - 150 BFE): Period marked by significant reforms under Savona The Gallant, including intermarriage and cultural exchange.
Alliance Boycott (78 BFE): The alliance of Venen, Aruel, and Mistdenn port cities stop paying tributes, leading to negotiations and state reforms.
State Reformation (75 BFE): The Imperial Courts are established, replacing the Council of Dragons, with representation for colonies and vassal states.
Overthrow of Sitri Governor (12 BFE): The imperial militia overthrows Arigat, governor of Sitri, leading to widespread regional unrest.
Crimson Wars (12 BFE - 1 AFE): A series of conflicts resulting in the collapse of the Vesunni Empire and the rise of multiple independent states.
Adoption of Crimson Banner (9 BFE): The Alliance adopts the crimson banner during the Crimson Wars.
Declaration of Independence by Sitri (12 BFE): Sitri declares independence, followed by Nasan and Masan, leading to the Crimson Wars.
";
        }

        public static string GetSecrets()
        {
            return @"Additional facts about the world
* The Black Brotherhood, also known as the Choir of Warlocks, is a secret society of arcane scholars and practitioners of dark arts. They are rumored to possess hidden knowledge and lost magical abilities, blending seamlessly into society as ordinary individuals like bakers, healers, and politicians. The Brotherhood is whispered to meet annually at the ruins of Elezoc to plan their actions and cast spells that could alter the world's fate.
* The Black Serpent is an ancient Vesunni myth that tells of Nirgos, the first being who created the world and separated his virtues into men (reason) and dragons (magic). When men and dragons united, Nirgos, feeling threatened, attempted to remake the world but was betrayed and transformed into a huge black snake. Swearing revenge, Nirgos formed a retinue to prepare for his return, and this myth possibly ties to the secretive Black Brotherhood, who may be working towards his resurrection.
* Dragonsteel was produced at the Victia Forge until 325 BFE in very small quantities which required the flame of an adult dragon. This is a metal that harbors mysterious qualities, such as its extreme durability and glow under lunar light.

Country: Secret Kingdom of the Zaine
Notable features: A mythical realm known for its magical feats and the use of the purple-sapped Usha trees for various arcane purposes.";
        }

        public static string GetCountries()
        {
            return @"Country: No Man's Land
Geography: Dense jungles, steaming mountains, and swamps.
Notable Features: Mordhen River, untamed wildlife, and legends of wild dragons.
Cities: Gofors (ruins of Zaine civilization near the mouth of the Mordhen River).
Economy: The dense jungles and dangerous creatures of No Man's Land remain unexplored and unclaimed.

Country: Lands of Ash
Geography: Central grasslands turned to ashes from historical conflicts. Desolate wastelands resulting from dragon fire, filled with hidden mysteries and dangers.
Notable Features: Petrified dragon skeletons, the Lake of Sorrows, and the ruined city of Elezoc. 

Country: Eryx
Geography: Transition zone between high grasslands and dense jungles, located near the Mordhen River.
Cities: Victia is a significant port city in the north.
Notable features: The Antium, former capital of the Vesunni Empire, now the Confederation's capital. It includes the Great Imperial Palace and The Hive, a structure housing ancient dragon dens.
Economy: Fertile lands used for various crops.

Country: Corcyra
Geography: Evergreen mountains, arable slopes, and valleys near the coast. Vesunni-influenced region, part of the initial Vesunni colonies. 
Cities: Cineta, located at the easternmost end of the Warm Bay, important for trade.
Economy: Produces various agricultural products and silks

Country: Celeia
Geography: Evergreen mountains, valleys, and coastal lowlands. Vesunni-influenced colony, significant for its strategic importance. 
Cities: Atrans, a city at the mouth of the Lentia River, crucial for trade.
Notable features: Victia Forge, historical site where dragonsteel was once produced.
Economy: Fertile lands support agricultural activities

Eryx, Corcyra, and Celeia are known as the ""three daughters"" or ""three sisters,"" these states form the Confederation of Lordships, maintaining a political system similar to the old empire with a focus on autonomy and collective governance.

Country: Sitri
Geography: Situated in the eastern part of Centralia, involved in ongoing disputes with Masan.
Cities: Sorana at the mouth of the Vendable River
Economy: Oligarchy focused on trade and agriculture, mostly olives and wine making

Country: Masan
Geography: Located east of Centralia, known for its rich lands and ongoing disputes with Sitri.
Notable Features: Great Bani River.
Economy: Republic known for its agricultural production, especially grains and spices

Country: Disputed Lands
Geography: Contested area between Masan and Sitri.
Cities: Soutes The Floating One (key city).
Economy: Periodically controlled by Sitri and Masan, with ongoing conflicts over territory

Country: Nasan
Geography: Rich, jungle-like lands around the Bani River, with farmland to the south and deserts near the Zuhail border.
Cities: Ramaesh, Bajpai, and the capital Nias.
Economy: A prosperous region ruled by the Shreekar royal family, known for its fabrics, cocoa, coffee, and salt production.

Country: Anomaria
Geography: Landlocked state with a mountainous and rocky landscape.
Cities: Vala (trade center) and Borrivino (mining town near the Shining Mountain).
Economy: A small, silver-rich state established by a nobleman from Mistdenn.

Country: Empire of Zuhail
Geography: Desert-based, with many subterranean cities, surrounded by mountains to the north.
Notable Features: Known for its resilient architecture and theocratic rule.
Economy: A theocratic empire with impressive subway cities and strong traditions, ruled by His Highness Zuha XXXII.

Country: Mistdenn
Geography: Marshy area in southern Centralia, full of rivers and brackish water lagoons.
Notable Features: Extracts shellfish, mollusks, and pearls from lagoons.
Economy: A confederation of wealthy port cities governed by the Assembly of Ships.

Country: Castlecliff
Geography: A relatively poor grand duchy known for its sheep grazing, having separated from Mistden

Country: Aruel
Geography: Kingdom with a coastal border featuring lighthouses and naval bases controlling passage through the Warm Bay.
Economy: A rich and diverse kingdom with a strong economy and a well-established royal lineage.

Country: Venen
Geography: Not detailed in the provided text, part of historical alliances with Aruel and Mistdenn.
Economy: A large and prosperous kingdom known for its beautiful landscapes and the Great Archive of Myosia, ruled by Queen Valia of Venen.";
        }


        public static string GetAttributes()
        {
            return @"Olive has the following attributes: Bravery, Charm, Constitution, Fame, Imagination, Intelligence, Morality. They are abbreviated to BRV, CHM, CON, FME, IMG, INT and MOR.
Each attribute has a value of 0-99. Use the abbreviated form. Use only these attributes for Olive.";
        }

        public static string GetItems()
        {
            return @"Consider items such as outfits, books, flowers, ornaments, perfume, jewelry, gems, dragon eggs, potions, weapons like swords and bows, artifacts and food items";
        }

        public static string GetCharacter(string character_attributes, string character_characteristics, string character_development)
        {
            return @$"
Create a character who is: {character_attributes}
Tell me about her motivation and why they can't have it with each starting with the name of the character.
Describe what their flaws and strengths are. They should have 2-5 flaws. Each flaw should be 2-5 sentences. They should have 2-5 strengths. Each strengths should be 2-5 sentences. Each sentence should start with the name of the character.
Describe their appearance in the form of a prompt that is safe for model dall-e-3.
Describe their clothes starting with the name of the character.
Describe their background starting with the name of the character.
Describe why they are currently in a village in Masan starting with the name of the character.
Consider likeability, proactiveness and competance, each is on a scale of 0-5.
{character_characteristics}
Describe 10 character arcs where they become {character_development}. Each arc has 3-8 steps. Each step should be 2-5 sentences. Each step should contain the name of the character.
Each arc should be different and at least 5 contain details from the world. There should be at least 1 arc with 8 steps. Most arcs should contain at least 5 steps. Do not name the character in the step and only refer to them by he or she.
Think step by step.
Return in JSON format like this example: {{name, motivation, whytheycanthaveit, characteristics: {{""initial likeability rating"" as ""likeability"", ""initial proactiveness rating"" as ""proactiveness"", ""initial competence rating"" as ""competence""}}, flaws: [flaw, flaw], strengths: [strength, strength], appearanceprompt, background, ""Current Situation in Masan"" as ""masan"", arcs: [ {{ name, steps: [{{ step, location }}]}}] }}";
        }

        public static string GetScenario(string about_char, string name, string before_story, string story)
        {
            return $@"
{about_char}
{name} is a friend of Olive.
Olive is a young girl in Masan.

The events that happen before the story: ""{before_story}""

Consider the story ""{story}""
Give a title to the story.

Olive has the following attributes: Bravery, Charm, Constitution, Fame, Imagination, Intelligence, Morality. They are abbreviated to BRV, CHM, CON, FME, IMG, INT and MOR.
Each attribute has a value of 0-99. Use the abbreviated form. Use only these attributes for Olive. Examples of an attribute change: BRV +1, CHM -1

Olive has money in the form of gold coins. If something costs 1 gold coin it is a little, 50 gold coins is medium amount and 250 gold coins is a lot.
";
        }

        public static string GetOlive()
        {
            return @"Olive is a young girl who lives in Masan.";
        }

        public static string GetWorld()
        {
            return GetTimeline() + GetCountries() + GetSecrets() + GetItems();
        }

        public static string GetArcs()
        {
            var arcs = JsonConvert.DeserializeObject<List<Arc>>(File.ReadAllText("./arcs.json"));

            return string.Join('\n', arcs.Select(x => $"Scenario: {x.name}. Steps: {string.Join(' ', x.steps.Select(x => x.step))}"));
        }
    }
}

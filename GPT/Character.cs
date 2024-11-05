using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public class Arc
    {
        public string name { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Characteristics
    {
        public int likeability { get; set; }
        public int proactiveness { get; set; }
        public int competence { get; set; }
    }

    public class Character
    {
        public string name { get; set; }
        public string motivation { get; set; }
        public string whytheycanthaveit { get; set; }
        public Characteristics characteristics { get; set; }
        public List<string> flaws { get; set; }
        public List<string> strengths { get; set; }
        public string appearanceprompt { get; set; }
        public string clothes { get; set; }
        public string background { get; set; }
        public string masan { get; set; }
        public List<Arc> arcs { get; set; }
    }

    public class Step
    {
        public string step { get; set; }
        public string location { get; set; }
    }
}

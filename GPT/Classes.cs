using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SchoolClass
    {
        public string name { get; set; }
        public int level { get; set; }
    }

    public class Classes
    {
        public List<SchoolClass> art { get; set; }
        public List<SchoolClass> lady { get; set; }
        public List<SchoolClass> showbiz { get; set; }
        public List<SchoolClass> combat { get; set; }
        public List<SchoolClass> crafting { get; set; }
        public List<SchoolClass> animals { get; set; }
        public List<SchoolClass> magic { get; set; }
        public List<SchoolClass> demonology { get; set; }
        public List<SchoolClass> necromancy { get; set; }
    }


}

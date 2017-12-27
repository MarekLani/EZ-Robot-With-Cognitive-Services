using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{
    class Speakers
    {
        public static List<Speaker> ListOfSpeakers = new List<Speaker>() { new Speaker() { Name = "Marek", ProfileId = "d64ff595-162e-42ef-9402-9aa0ef72d7fb" } };
    }

    class Speaker
    {
        public string Name { get; set; }
        public string ProfileId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{

        /*
         * Following phrases are used:
         * Start face recognition
         * */
        public sealed class Phrases
        {


            public readonly String name;
            private readonly int value;

            public static readonly Phrases RECOGNIZEFACE = new Phrases(1, "Start face recognition");
            public static readonly Phrases WINDOWSAUTHENTICATION = new Phrases(2, "WINDOWS");
            public static readonly Phrases SINGLESIGNON = new Phrases(3, "SSN");

            private Phrases(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }

            private static readonly Dictionary<string, Phrases> instance = new Dictionary<string, Phrases>();

            public static explicit operator Phrases(string str)
            {
                Phrases result;
                if (instance.TryGetValue(str, out result))
                    return result;
                else
                    throw new InvalidCastException();
            }


    }
}

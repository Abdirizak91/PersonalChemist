using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChemist.Landing
{
    public class InhalerType
    {
        public string Inhaler { get; set; }

        public int ID { get; set; }

        public override string ToString()
        {
            return this.ID + "  " + this.Inhaler;
        }

    }
}

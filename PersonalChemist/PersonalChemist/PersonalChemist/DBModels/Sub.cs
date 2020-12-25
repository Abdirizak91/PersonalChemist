using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalChemist.DBModels
{
    public class Sub
    {
        public string SUB_TYPE { get; set; }

        public int SUB_COST { get; set; }

        public int CATERS_FOR { get; set; }

        public string SUB_START { get; set; }

        public string SUB_END { get; set; }
    }
}

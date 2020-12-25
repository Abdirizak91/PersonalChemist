using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalChemist.DBModels
{
    public class User
    {
        [Required]
        public string EMAIL { get; set; }

        [Required]
        public string PHONE_NUM { get; set; }

        public string NAME { get; set; }

        [Required]
        public int SUBSCRIPTION_ID { get; set; }
    }
}

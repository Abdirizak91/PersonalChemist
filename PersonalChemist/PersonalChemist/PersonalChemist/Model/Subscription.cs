using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalChemist.Model
{
    public class Subscription
    {
        public string Customer { get; set; }

        public string Product { get; set; }

        public int Price { get; set; }

        public string Subscriptions { get; set; }

        public string Invoice { get; set; }

        public bool PaymentIntent { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalChemist.Subscription
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Subscriptions : ContentPage
    {
        string mycustomer;
        string getchargedID;
        
        public Subscriptions()
        {
            InitializeComponent();
            EmailFrame.BackgroundColor = Color.FromHex("FF6161");
            
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
                                         // Stripe Testing Key   
            StripeConfiguration.ApiKey = "pk_live_51HP7jEE1THVYmYKHOX1h8MOjYelXCWz4j3gQUJgfZodSCkMdlXVMXg9E78V6Gm0w12GTEA0LNxlPkiiPpcPo0ugJ00sqW6bkHC";




            //This are the sample test data use MVVM bindings to send data to the ViewModel




            // one off payment



            //// payment card
            //Stripe.TokenCardOptions stripcard = new Stripe.TokenCardOptions();
            //stripcard.Number = "4000000000003055";
            //stripcard.ExpYear = 2020;
            //stripcard.ExpMonth = 11;
            //stripcard.Cvc = "199";



            ////Step 1 : Assign Card to Token Object and create Token

            //Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            //token.Card = stripcard;
            //Stripe.TokenService serviceToken = new Stripe.TokenService();
            //Stripe.Token newToken = serviceToken.Create(token);

            //// Step 2 : Assign Token to the Source

            //var options = new SourceCreateOptions
            //{
            //    Type = SourceType.Card,
            //    Currency = "usd",
            //    Token = newToken.Id
            //};

            //var service = new SourceService();
            //Source source = service.Create(options);

            ////Step 3 : Now generate the customer who is doing the payment

            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
            {
                Name = "Samir",
                Email = "SamirGc@gmail.com",
                Description = "Customer for jenny.rosen@example.com",

            };

            //var customerService = new Stripe.CustomerService();
            //Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

            //mycustomer = stripeCustomer.Id; // Not needed

            ////Step 4 : Now Create Charge Options for the customer. 

            //var chargeoptions = new Stripe.ChargeCreateOptions
            //{
            //    Amount = 1124,
            //    Currency = "USD",
            //    ReceiptEmail = "samirgc112@gmail.com",
            //    Customer = stripeCustomer.Id,
            //    Source = source.Id

            //};



            //// Creating Payment Method

            //var PaymentMethodoptions = new PaymentMethodCreateOptions
            //{
            //    Type = "card",
            //    Card = new PaymentMethodCardOptions
            //    {
            //        Number = CardNumEntry.Text,
            //        ExpMonth = long.Parse(ExpMonthEntry.Text),
            //        ExpYear = long.Parse(ExpYearEntry.Text),
            //        Cvc = CVCEntry.Text,

            //    },

            //};

            //var PaymentMethodservice = new PaymentMethodService();
            //var PaymentInfo = PaymentMethodservice.Create(PaymentMethodoptions);


            ////Step 5 : Perform the payment by  Charging the customer with the payment. 
            //var service1 = new Stripe.ChargeService();
            //Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment

            //getchargedID = charge.Id; // Not needed




            //Attach payment Method to a customer
            //var AttachPaymentMethodoptions = new PaymentMethodAttachOptions
            //{
            //    Customer = "cus_IDLX9DlrxFwYjW",

            //};
            //var AttachPaymentMethodservice = new PaymentMethodService();
            //AttachPaymentMethodservice.Attach(
            //  $"{PaymentInfo.Id}",
            //  AttachPaymentMethodoptions
            //);





            ////udpate customer, make customer card 'default' so subscriptions work
            //var UpdateCustomeroptions = new CustomerUpdateOptions
            //{

            //    InvoiceSettings = new CustomerInvoiceSettingsOptions
            //    {
            //        //card ID
            //        DefaultPaymentMethod = PaymentInfo.Id,
            //    },


            //};

            //var UpdateCustomerservice = new CustomerService();
            //UpdateCustomerservice.Update("cus_IDLX9DlrxFwYjW", UpdateCustomeroptions);









            // Create new Product 
            var Productoptions = new ProductCreateOptions
            {
                Name = "Home Subscription",
            };
            var Productservice = new ProductService();
            Productservice.Create(Productoptions);



            // Create Price For Product Price
            var Priceoptions = new PriceCreateOptions
            {
                UnitAmount = 2000,
                Currency = "gbp",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                Product = "prod_IDLGlyH5sTApFb",
            };
            var Priceservice = new PriceService();
            Priceservice.Create(Priceoptions);



            // Create new Subscription
            var Options = new SubscriptionCreateOptions
            {
                
                //Customer = "cus_IDLX9DlrxFwYjW",
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                        {
                            Price = "price_1HcucaE1THVYmYKHSyiEq54o",
                            
                        },
                    
                },
                
            };
            var Subservice = new SubscriptionService();
            Subservice.Create(Options);

        }
    }
}
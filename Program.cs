using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewtonsoftDemos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(serializeJSONExample());
            Console.WriteLine();

            Movie m = deserializeJSONExample();
            Console.WriteLine($"Name:{m.Name}, Release Date:{m.ReleaseDate.ToString("dd/MM/yyyy")}, GENRES:{String.Join(",", m.Genres)}");
            Console.WriteLine();

            Console.WriteLine(linqToJSON());
            Console.WriteLine();

            Console.WriteLine(JsonConvert.SerializeObject(createCustomer()));
            Console.WriteLine();

            AnotherCase();
        }

        //Serialize JSON example
        static string serializeJSONExample()
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2022, 11, 15);
            product.Sizes = new string[] { "Small" };

            string json = JsonConvert.SerializeObject(product);

            return json;
        }

        //Deserialize JSON example
        static Movie deserializeJSONExample()
        {
            string json = @"{
                  'Name': 'Bad Boys',
                  'ReleaseDate': '1995-4-7T00:00:00',
                  'Genres': [
                    'Action',
                    'Comedy'
                  ]
                }";

            Movie m = JsonConvert.DeserializeObject<Movie>(json);

            return m;
        }

        static string linqToJSON()
        {
            JArray array = new JArray();
            array.Add("Hello World");
            array.Add(new DateTime(2022, 11, 4));

            JObject o = new JObject();
            o["MyArray"] = array;

            string json = o.ToString();

            return json;
        }

        // Create a dynamic object with as many attributes one desires
        static dynamic createCustomer()
        {
            dynamic myObject = new JObject();

            JArray myCart = createCustomerCartItems();

            myObject.Add("Id", Guid.NewGuid());
            myObject.Add("Name", "Vasilis");
            myObject.Add("EShopVisitDate", DateTime.UtcNow);
            myObject.Add("EShopCartList", myCart);

            return myObject;
        }

        static dynamic createCustomerCartItems()
        {
            JArray myCartItems = new JArray();

            // Just an item
            dynamic anItem = new JObject();

            anItem.Add("SKU", Guid.NewGuid());
            anItem.Add("Description", "Mixer");
            anItem.Add("Price", 67.89m);

            // Cart items mixed with a dynamic object (item)
            // and two more anonymous objects
            var cartItems = new List<dynamic>()
            {
                anItem,
                new {
                    SKU = Guid.NewGuid(),
                    Description = "Washing Machine",
                    Price = 350.50m
                },
                new {
                    SKU = Guid.NewGuid(),
                    Description = "Toaster",
                    Price = 45.60m
                }
            };

            // Convert the above dynamic list into JArray
            myCartItems = JArray.FromObject(cartItems);

            // Just another item
            dynamic anotherItem = new JObject();

            anotherItem.Add("SKU", Guid.NewGuid());
            anotherItem.Add("Description", "Cooker");
            anotherItem.Add("Price", 450.78m);

            // Add the 'anotherItem' into the JArray myCartItems
            myCartItems.Add(anotherItem);

            return myCartItems;
        }

        static void AnotherCase()
        {
            List<string> aList = new List<string>();

            aList.Add($"{{ \"Key\": \"Id\", \"KeyType\": \"string\", \"Value\": \"1234\" }}");
            aList.Add($"{{ \"Key\": \"Description\", \"KeyType\": \"string\", \"Value\": \"Washing Machine\" }}");
            aList.Add($"{{ \"Key\": \"Price\", \"KeyType\": \"decimal\", \"Value\": {250m} }}");

            JArray myCases = new JArray();

            foreach(var doc in aList)
            {
                dynamic docObj = JsonConvert.DeserializeObject(doc);
                docObj.Add("MyKey", docObj.Key);
                myCases.Add(docObj);
            }

            Console.WriteLine(JsonConvert.SerializeObject(myCases));
        }
    }

    class Product
    {
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
        public string [] Sizes { get; set; }
    }

    class Movie
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string [] Genres { get; set; }
    }
}
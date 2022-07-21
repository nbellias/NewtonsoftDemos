using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewtonsoftDemos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(JsonConvert.SerializeObject(Customer()));
        }

        // Create a dynamic object with as many attributes you want
        static dynamic Customer()
        {
            dynamic myObject = new JObject();

            JArray myCart = CustomerCartItems();

            myObject.Add("Id", Guid.NewGuid());
            myObject.Add("Name", "Vasilis");
            myObject.Add("EShopVisitDate", DateTime.UtcNow);
            myObject.Add("EShopCartList", myCart);

            return myObject;
        }

        static dynamic CustomerCartItems()
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using RestaurantAPI.Entities;
using Address = RestaurantAPI.Entities.Address;

namespace RestaurantAPI
{
    public class DataGenerator
    {
        public static void Seed(RestaurantApiContext context)
        {
            Randomizer.Seed = new Random(997);

            var addressGenerator = new Faker<Address>()
                .RuleFor(a=>a.City, f=>f.Address.City())
                .RuleFor(a=>a.Street, f=>f.Address.StreetName())
                .RuleFor(a=>a.PostalCode, f=>f.Address.ZipCode("##-###"));


            var dishesGenerator = new Faker<Dish>()
                .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                .RuleFor(d => d.Description, f => f.Commerce.ProductDescription())
                .RuleFor(d => d.Price, f => Convert.ToDecimal( f.Commerce.Price(10m, 145m, 3)));


            var restaurant = new Faker<Restaurant>()
                .RuleFor(r => r.Name, f => f.Hacker.Adjective() + " " + f.Hacker.Noun())
                .RuleFor(r => r.Description, f => f.Lorem.Sentences())
                .RuleFor(r => r.Category, f => f.Commerce.Department())
                .RuleFor(r => r.HasDelivery, f => f.Random.Bool())
                .RuleFor(r => r.ContactEmail, f => f.Internet.Email())
                .RuleFor(r => r.ContactNumber, f => f.Phone.PhoneNumber("###-###-###"))
                .RuleFor(r => r.Address, f=>addressGenerator.Generate())
                .RuleFor(r => r.Dishes, f => dishesGenerator.Generate(10));
                

            var restaurants = restaurant.Generate(15);
            
            //context.AddRange(restaurants);
            //context.SaveChanges();
        }
    }
}

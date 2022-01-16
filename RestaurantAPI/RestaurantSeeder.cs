using RestaurantAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name ="KFC",
                    Category ="Fast food",
                    Description ="KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in US",
                    ContactEmail="contact@kfc.com",
                    HasDelivery=true,
                    Dishes=new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Nashville Hot Chicken",
                            Price=10.30M,
                        },

                        new Dish()
                        {
                            Name="Chicken Nuggets",
                            Price=4.25M,
                        },
                    },
                    Address=new Address()
                    {
                        City="Krakow",
                        Street="Dluga 5",
                        PostalCode="30-001"
                    }
                },

                new Restaurant()
                {
                    Name="McDonald",
                    Category="Fast food",
                    Description="Chain of fast food restaurants around the world",
                    ContactEmail ="contact@mcdonald.com",
                    HasDelivery =true,
                    Dishes= new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Big Mac",
                            Price=23.10M
                        },

                        new Dish()
                        {
                            Name="Happy meal",
                            Price=22.0M
                        }
                    },
                    Address=new Address()
                    {
                        City="Warszawa",
                        Street="Diamentowa 43",
                        PostalCode="34-234"

                    }
                }
            };

            return restaurants;
        }
    }
}

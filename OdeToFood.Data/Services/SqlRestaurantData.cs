﻿using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData

    {
        private readonly OdeToFoodDBContext db;

        public SqlRestaurantData(OdeToFoodDBContext db)
        {
            this.db = db;
        }
        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            if (restaurant!=null)
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
            }
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            //return db.Restaurants.OrderBy(r => r.Name);
            return from r in db.Restaurants
                   orderby r.Name ascending
                   select r;
              
        }

        public void Update(Restaurant restaurant)
        {
            //var r = Get(restaurant.Id);
            //r.Name = "modified name";
            //db.SaveChanges();

            var entry = db.Entry(restaurant);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}

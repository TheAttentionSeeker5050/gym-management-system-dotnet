using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;



namespace gym_management_system.Models
{
    // handle user model mongodb operations
    public class UserDBHandler
    {
        private string connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        public UserDBHandler() { }

        // create user
        public void createUser(User user)
        {
            try
            {

                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<User>("users");
                // collection.InsertOne(user);
                Console.WriteLine("User created");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void loginUser(User user)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<User>("users");
                /*var filter = Builders<User>.Filter.Eq("email", user.email);
                var result = collection.Find(filter).ToList();
                if (result.Count == 0)
                {
                    Console.WriteLine("User not found");
                }
                else
                {
                    Console.WriteLine("User found");
                }*/

                Console.WriteLine("User logged in");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

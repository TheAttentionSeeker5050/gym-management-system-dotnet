using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gym_management_system.Models;

namespace gym_management_system.Models
{
    // this is a model class for the employee user, and this is a data class

    public class User
    {

        private DBConnection _connection = new DBConnection();
        // constructor
        public User() { }

        // private fields ---------------------------------------------
        private string id = "";
        private string userName = "";
        private string userPassword = "";
        private string email = "";
        private string fullName = "";


        // public getter and setter methods ------------------------------
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        // public methods -----------------------------------------------
        public void createUser(User user)
        {
            try
            {

                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<User>("users");
                // collection.InsertOne(user);
                Console.WriteLine("User created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void loginUser(User user)
        {
            try
            {
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }





    }
}

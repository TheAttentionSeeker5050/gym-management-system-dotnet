﻿using MongoDB.Driver.Core.Configuration;
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
        public User() {
            _id = "";
            _userName = "";
            _userPassword = "";
            _email = "";
            _fullName  = "";

        }

        // private fields ---------------------------------------------
        private string _id;
        private string _userName;
        private string _userPassword;
        private string _email;
        private string _fullName;


        // public getter and setter methods ------------------------------
        public string Id { get { return _id; } set { _id = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string UserPassword { get { return _userPassword; } set { _userPassword = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string FullName { get { return _fullName; } set { _fullName = value; } }

        // public methods -----------------------------------------------
        public void createUser(User user)
        {
            try
            {

                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<User>("users");
                // collection.InsertOne(user);
                // Console.WriteLine("User created");
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

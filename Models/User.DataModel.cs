using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gym_management_system.Models;

// cryptography using BCrypt
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace gym_management_system.Models
{
    // this is a model class for the employee user, and this is a data class

    public class User
    {
        
        private DBConnection _connection = new DBConnection();


        // private constants --------------------------------------------
        private byte[] encryption_salt;


        // constructor
        public User() {
            // make id a mongo ObjectId
            _id = ObjectId.GenerateNewId();
            _userName = "";
            _userPassword = "";
            _email = "";
            _fullName  = "";

            string env_salt = Environment.GetEnvironmentVariable("HASH_SECRET_SALT").ToString();

            encryption_salt = Encoding.ASCII.GetBytes(env_salt);

        }

        // private fields ---------------------------------------------
        private ObjectId _id;
        private string _userName;
        private string _userPassword;
        private string _email;
        private string _fullName;


        // public getter and setter methods ------------------------------
        public ObjectId Id { get { return _id; } set { _id = value; } }
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not create user");
            }
        }

        public User loginUser(User user)
        {
            User userResult = new User();
            try
            {
                
                // initialize the mongo client and get the database and collection
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<User>("users");


                // add filters fpr searching the user
                var filter = Builders<User>.Filter.Eq("Email", user.Email);
                userResult = collection.Find(filter).FirstOrDefault();


                if (userResult.Id == null)
                {
                    throw new Exception("Incorrect user credentials");
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not log in user");
            }

            try
            {
                // now compare the passwords using the method compare passwords
                bool passwordsMatch = ComparePasswords(user.UserPassword, userResult.UserPassword);

                if (passwordsMatch == false)
                {
                    Console.WriteLine("Passwords do not match");
                    throw new Exception("Incorrect user credentials");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);

            }

            return userResult;

        }


        public string hashPassword(string password)
        {
            try
            {

                // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: encryption_salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return hashed;

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not hash password");
            }
        }

        public bool ComparePasswords(string inPassword, string hashedDBPassword)
        {
            // this method compares the password entered by the user (inPassword) with the hashed password in the database hashedDBPassword
            try
            {

                string hashedInPassword = hashPassword(inPassword);

                // compare the two hashed passwords, return true if they are the same, false if not
                if (hashedInPassword == hashedDBPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not hash password");
            }
        }


    }
}

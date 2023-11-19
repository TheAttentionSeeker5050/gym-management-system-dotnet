// general imports
using System;
using System.Text;

// model components
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

// mongo imports
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// cryptography using BCrypt
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace gym_management_system.Models
{
    // this is a model class for the employee user, and this is a data class

    public class UserRegister
    {
        
        private DBConnection _connection = new DBConnection();


        // private constants --------------------------------------------
        private byte[] encryption_salt;


        // constructor
        public UserRegister() {
            // make id a mongo ObjectId
            _id = ObjectId.GenerateNewId();
            _userName = "";
            _userPassword = "";
            _userPasswordConfirm = "";
            _hashedPassword = "";
            _email = "";
            _fullName  = "";

            string env_salt = Environment.GetEnvironmentVariable("HASH_SECRET_SALT").ToString();

            encryption_salt = Encoding.ASCII.GetBytes(env_salt);

        }

        // private fields ---------------------------------------------
        private ObjectId _id;
        private string _userName;
        private string _userPassword;
        private string _userPasswordConfirm;
        private string _hashedPassword;
        private string _email;
        private string _fullName;


        // public getter and setter methods ------------------------------
        [BsonId]
        public ObjectId Id { get { return _id; } set { _id = value; } }
        
        [Required, MaxLength(50), DisplayName("User Name"), BsonElement("UserName")]
        // regex to check for alphanumeric characters only, between 8 and 50 characters
        [RegularExpression(@"^[a-zA-Z0-9]{8,50}$", ErrorMessage = "User name must be between 8 and 50 characters, and contain only alphanumeric characters")]
        public string ?UserName { get { return _userName; } set { _userName = value; } }

        [Required, DisplayName("Password"), BsonElement("UserPassword")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Password must be between 8 and 32 characters, and contain at least one uppercase, one lowercase, one number, and one special character")]
        public string UserPassword { get { return _userPassword; } set { _userPassword = value; } }


        [Required, MaxLength(32), DisplayName("Confirm Password"), BsonElement("UserPasswordConfirm")]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match")]
        // regex to contain at least one uppercase, one lowercase, one number, one special character, and be between 8 and 32 characters
        public string ?UserPasswordConfirm { get { return _userPasswordConfirm; } set { _userPasswordConfirm = value; } }


        public string ?HashedPassword { get { return _hashedPassword; } set { _hashedPassword = value; } }


        [Required, MaxLength(50), DisplayName("Email"), BsonElement("Email")]
        // regex to check for valid email address
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get { return _email; } set { _email = value; } }

        [Required, MaxLength(50), DisplayName("Full Name"), BsonElement("FullName")]
        // regex to check for valid full name
        [RegularExpression(@"^[a-zA-Z\s]{1,50}$", ErrorMessage = "Please enter a valid name")]
        public string ?FullName { get { return _fullName; } set { _fullName = value; } }



        // public methods -----------------------------------------------
        public void createUser(UserRegister user)
        {
            try
            {

                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<UserRegister>("users");
                // collection.InsertOne(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not create user");
            }
        }

        public UserRegister loginUser(UserRegister user)
        {
            UserRegister userResult = new UserRegister();
            try
            {
                
                // initialize the mongo client and get the database and collection
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<UserRegister>("users");


                // add filters fpr searching the user
                var filter = Builders<UserRegister>.Filter.Eq("Email", user.Email);
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
                bool passwordsMatch = ComparePasswords(user.UserPassword, userResult.HashedPassword);
                

                if (passwordsMatch == false)
                {
                    Console.WriteLine("Passwords do not match");
                    throw new Exception("Incorrect user credentials");
                }

                // delete the password confirm field from the user object
                userResult.UserPasswordConfirm = null;
                userResult.UserPassword = null;

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

                Console.WriteLine("Hashed password: " + hashed);

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

    public class UserLogin
    {

        private DBConnection _connection = new DBConnection();


        // private constants --------------------------------------------
        private byte[] encryption_salt;


        // constructor
        public UserLogin()
        {
            // make id a mongo ObjectId
            _id = ObjectId.GenerateNewId();
            _userName = "";
            _userPassword = "";
            _hashedPassword = "";
            _email = "";
            _fullName = "";

            string env_salt = Environment.GetEnvironmentVariable("HASH_SECRET_SALT").ToString();

            encryption_salt = Encoding.ASCII.GetBytes(env_salt);

        }

        // private fields ---------------------------------------------
        private ObjectId _id;
        private string _userName;
        private string _userPassword;
        private string _userPasswordConfirm;
        private string _hashedPassword;
        private string _email;
        private string _fullName;


        // public getter and setter methods ------------------------------
        [BsonId]
        public ObjectId Id { get { return _id; } set { _id = value; } }
        
        [Required, MaxLength(50), DisplayName("Email"), BsonElement("Email")]
        // regex to check for valid email address
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get { return _email; } set { _email = value; } }
        
        [Required, MaxLength(32), DisplayName("Password"), BsonElement("UserPassword")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Password must be between 8 and 32 characters, and contain at least one uppercase, one lowercase, one number, and one special character")]
        public string UserPassword { get { return _userPassword; } set { _userPassword = value; } }

        public string ?UserPasswordConfirm { get { return _userPasswordConfirm; } set { _userPasswordConfirm = value; } }


        public string? UserName { get { return _userName; } set { _userName = value; } }

        public string HashedPassword { get { return _hashedPassword; } set { _hashedPassword = value; } }

        public string? FullName { get { return _fullName; } set { _fullName = value; } }



        // public methods -----------------------------------------------
        public void createUser(UserLogin user)
        {
            try
            {

                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<UserLogin>("users");
                // collection.InsertOne(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Something went wrong: Could not create user");
            }
        }

        public UserLogin loginUser(UserLogin user)
        {
            UserLogin userResult = new UserLogin();
            try
            {

                // initialize the mongo client and get the database and collection
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<UserLogin>("users");


                // add filters fpr searching the user
                var filter = Builders<UserLogin>.Filter.Eq("Email", user.Email);
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
                bool passwordsMatch = ComparePasswords(user.UserPassword, userResult.HashedPassword);


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

                Console.WriteLine("Hashed password: " + hashed);

                return hashed;

            }
            catch (Exception e)
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

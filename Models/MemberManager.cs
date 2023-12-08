using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;


namespace gym_management_system.Models {
    public class MemberManager {
        // this will CRUD the GymMember document
        // this is a composed document, so we will need to use the mongodb driver

        // define the connection string
        private DBConnection _connection = new DBConnection();

        public MemberManager() {
            // constructor
        }

        // private variables --------------------------------------
        private List<GymMember> _members = new List<GymMember>();
        private GymMember _member = new GymMember();

        // public getters and setters ------------------------------
        // Members array
        public List<GymMember> Members {
            get { return _members; }
            set { _members = value; }
        }

        // Member object
        public GymMember Member {
            get { return _member; }
            set { _member = value; }
        }

        // public methods ------------------------------------------
        public void CreateMember(GymMember member) {
            // create a new member
            var client = new MongoClient(_connection.MONGO_CONN_STRING);
            var db = client.GetDatabase("gym_management_system");
            var collection = db.GetCollection<GymMember>("gymMembers");

            var errors = new Dictionary<string, string>();

            // find by username to assert uniqueness
            var filter = Builders<GymMember>.Filter.Eq("UserName", member.UserName);
            var result = collection.Find(filter).ToList();
            if (result.Count > 0)
            {
                errors.Add("Email", "Email already exists");
            }

            // find by email to assert uniqueness
            filter = Builders<GymMember>.Filter.Eq("Email", member.Email);
            result = collection.Find(filter).ToList();
            if (result.Count > 0)
            {
                errors.Add("UserName", "Username already exists");
            }

            if (errors.Count > 0)
            {
                throw new MultipleFieldException(errors);
            }

            // insert the member
            collection.InsertOne(member);

            // validate the insert by getting the member
            GetMemberByEmail(member.Email);

            // if the member is null, throw an exception
            if (_member == null)
            {
                throw new Exception("Error creating member");
            }
            
        }

        // update member info
        public void UpdateMemberInfo(ObjectId objectId, GymMember member)
        {
            // update a member
            var dbMember = _member;
            if (dbMember == null)
            {
                throw new Exception("Member not found");
            }

            /*GetAllMembers();*/

            var client = new MongoClient(_connection.MONGO_CONN_STRING);
            var db = client.GetDatabase("gym_management_system");
            var collection = db.GetCollection<GymMember>("gymMembers");

            // make an error dictionary
            var errors = new Dictionary<string, string>();

            // find by username to assert uniqueness
            var filter = Builders<GymMember>.Filter.Eq("UserName", member.UserName);
            GetMembersWithFilter(filter);
            // var result = _members;
            // if the username is not the same as the current member, and the username already exists
            if (_members.Count > 0 && _members[0]._id.ToString() != objectId.ToString())
            {
                errors.Add("UserName", "Username already exists");
            }

            // find by email to assert uniqueness
            filter = Builders<GymMember>.Filter.Eq("Email", member.Email);
            GetMembersWithFilter(filter);
            // result = _members;
            // if the email is not the same as the current member, and the email already exists
            if (_members.Count > 0 && _members[0]._id.ToString() != objectId.ToString())
            {
                errors.Add("Email", "Email already exists");
            }

            // if there are errors, throw an exception
            if (errors.Count > 0)
            {
                throw new MultipleFieldException(errors);
            }

            // use the generic method to update the member
            filter = Builders<GymMember>.Filter.Eq("_id", objectId);
            var update = Builders<GymMember>.Update.Set("UserName", member.UserName)
                .Set("Email", member.Email)
                .Set("FullName", member.FullName)
                .Set("PhoneNumber", member.PhoneNumber)
                .Set("Address", member.Address)
                .Set("DateOfBirth", member.DateOfBirth)
                .Set("DateJoined", member.DateJoined)
                .Set("Memberships", member.Memberships)
                .Set("BioMetrics", member.BioMetrics)
                .Set("Bills", member.Bills);

            // update the member
            collection.UpdateOne(filter, update);

        }

        // delete methods -----------------------------
        // delete member
        public void DeleteMember(ObjectId memberId)
        {
            // delete a member
            try
            {
                


                // use the mongodb driver to delete the member
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);

                var result = collection.DeleteOne(filter);
                if (result.DeletedCount == 0)
                {
                    throw new Exception("Member not found");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // create the membership object
        /*public void CreateMembership(ObjectId memberId, Membership membership) {
            // create a new membership
            try {
                // get the member using method, it will throw an exception if not found
                GetMemberById(memberId);

                // add the membership to the member, membership is an array inside the member document
                // use our member object to update the member, and push the membership object to the array
                _member.Memberships.Append(membership);

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);
                var update = Builders<GymMember>.Update.Set("Memberships", _member.Memberships);
                
                // use the generic method to update the member
                UpdateMembershipGeneric(filter, update);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }*/

        // create the biometric object
        /*public void CreateBiometric(ObjectId memberId, BioMetric biometric) {
            // create a new biometric
            try {
                // get the member using method, it will throw an exception if not found
                GetMemberById(memberId);

                // add the biometric to the member, biometric is an array inside the member document
                // use our member object to update the member, and push the biometric object to the array
                _member.BioMetrics.Append(biometric);

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);
                var update = Builders<GymMember>.Update.Set("BioMetrics", _member.BioMetrics);
                
                // use the generic method to update the member
                UpdateMembershipGeneric(filter, update);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }*/

        // create the bill object
        /*public void CreateBill(ObjectId memberId, Bill bill) {
            // create a new bill
            try {
                // get the member using method, it will throw an exception if not found
                GetMemberById(memberId);

                // add the bill to the member, bill is an array inside the member document
                // use our member object to update the member, and push the bill object to the array
                _member.Bills.Append(bill);

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);
                var update = Builders<GymMember>.Update.Set("Bills", _member.Bills);
                
                // use the generic method to update the member
                UpdateMembershipGeneric(filter, update);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }*/





        /*// delete membership
        public void DeleteMembership(ObjectId memberId, ObjectId membershipId) {
            // delete a membership
            try {
                // get the member using method, it will throw an exception if not found
                GetMemberById(memberId);

                // remove the membership from the member, membership is an array inside the member document
                // use our member object to update the member, and push the membership object to the array
                _member.Memberships.All(m => m._id != membershipId);
                
                // console log
                Console.WriteLine(_member.Memberships);

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);
                var update = Builders<GymMember>.Update.Set("Memberships", _member.Memberships);
                
                // use the generic method to update the member
                UpdateMembershipGeneric(filter, update);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }*/

        /*// delete biometric
        public void DeleteBiometric(ObjectId memberId, ObjectId biometricId) {
            // delete a biometric
            try {
                // get the member using method, it will throw an exception if not found
                
                GetMemberById(memberId);

                // remove the biometric from the member, biometric is an array inside the member document
                // use our member object to update the member, and push the biometric object to the array
                _member.BioMetrics.All(m => m._id != biometricId);
                
                // console log
                Console.WriteLine(_member.BioMetrics);

                // create filters and update builders
                var filter = Builders<GymMember>.Filter.Eq("_id", memberId);
                var update = Builders<GymMember>.Update.Set("BioMetrics", _member.BioMetrics);
                
                // use the generic method to update the member
                UpdateMembershipGeneric(filter, update);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }*/

        // update member generic method, using filter and update as parameters
        /*public void UpdateMembershipGeneric(FilterDefinition<GymMember> filter, UpdateDefinition<GymMember> update)
        {
            // create a new membership
            try
            {

                // create the membership inside the member document
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // update the member
                collection.UpdateOne(filter, update);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error updating membership");
            }
        }*/



        // get member and members methods: -------------------------
        // ---------------------------------------------------------

        // get member with filter object
        private GymMember GetMember(FilterDefinition<GymMember> filter) {
            // we will use filters for DRY code
            try {
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // get the member
                var result = collection.Find(filter).ToList();
                if (result.Count > 0) {
                    return result[0];
                } else {
                    throw new Exception("Member not found");
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception("Error getting member");
            }
        }

        // using the above method, we can get a member by id, email or username
        public void GetMemberById(ObjectId id) {
            // get member by id
            try {
                var filter = Builders<GymMember>.Filter.Eq("_id", id);
                _member = GetMember(filter);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void GetMemberByEmail(string email) {
            // get member by email
            try {
                var filter = Builders<GymMember>.Filter.Eq("Email", email);
                _member = GetMember(filter);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void GetMemberByUsername(string username) {
            // get member by username
            try {
                var filter = Builders<GymMember>.Filter.Eq("UserName", username);
                _member = GetMember(filter);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        // get all members
        public void GetAllMembers() {
            // get all members
            try {
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // get all the members, we will use an empty filter selector
                var result = collection.Find(_ => true).ToList();
                
                _members = result;

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception("Error getting members");
            }
        }

        // get members with paginations, using limit and offset
        public void GetMembersWithPagination(int limit, int offset) {
            // get all members
            try {
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // get the member
                var result = collection.Find(_ => true).Skip(offset).Limit(limit).ToList();

                _members = result;

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception("Error getting members");
            }
        }

        // get members with filter
        public void GetMembersWithFilter(FilterDefinition<GymMember> filter) {
            // get all members
            try {
                var client = new MongoClient(_connection.MONGO_CONN_STRING);
                var db = client.GetDatabase("gym_management_system");
                var collection = db.GetCollection<GymMember>("gymMembers");

                // get the member
                var result = collection.Find(filter).ToList();

                _members = result;

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw new Exception("Error getting members");
            }
        }

        
    }


    // exception handlers
    public class CustomException : Exception
    {
        // handle unique field exceptions
        public string FieldName { get; private set; }
        public CustomException(string fieldName, string message) : base(message)
        {
            FieldName = fieldName;
        }
    }

    public class MultipleFieldException : Exception
    {
        public IDictionary<string, string> Errors { get; private set; }

        public MultipleFieldException(IDictionary<string, string> errors)
            : base("There are multiple validation errors.")
        {
            Errors = errors;
        }
    }
}
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace gym_management_system.Models
{


    [BsonIgnoreExtraElements]
    public class GymMember
    {

        // this is the document schema for mongodb driver support
        // use bson data annotations to map the schema to the document
        // field names are in pascal case (capitalized each word)
        [BsonId]
        [BsonElement(elementName: "_id")]
        public ObjectId _id { get; set; }

        // this should be required and unique
        [BsonElement(elementName: "UserName")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string UserName { get; set; }

        [BsonElement(elementName: "Email")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement(elementName: "FullName")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string FullName { get; set; }

        [BsonElement(elementName: "PhoneNumber")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string PhoneNumber { get; set; }

        [BsonElement(elementName: "Address")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string Address { get; set; }

        [BsonElement(elementName: "DateOfBirth")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);

        [BsonElement(elementName: "DateJoined")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateJoined { get; set;} = DateTime.Now;

        // an object array called membership and biometrics, default as empty array
        public List<Membership> Memberships { get; set; } = new List<Membership>();

        public List<BioMetric> BioMetrics { get; set; } = new List<BioMetric>();

        public List<Bill> Bills { get; set; } = new List<Bill>();

    }



}

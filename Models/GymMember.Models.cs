using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace gym_management_system.Models
{

    

    public class GymMember
    {
        
        // this is the document schema for mongodb driver support
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        // public string UserPasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);
        public DateTime DateJoined { get; set;} = DateTime.Now;

        // an object array called membership and biometrics, default as empty array
        public Membership[] Memberships { get; set; }

        public BioMetric[] BioMetrics { get; set; }

        public Bill[] Bills { get; set; } = new Bill[0];

    }


}

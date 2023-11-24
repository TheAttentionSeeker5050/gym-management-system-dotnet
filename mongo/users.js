// with the following data model:
/*public class User {
public string Id { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    // public string Role { get; set; }
}*/

// add a mongo js transaction file to add 3 users to the database
// for the moment password is not hashed

db = db.getSiblingDB('gym_management_system');

db.users.insertMany([
    {
        "UserName": "admin",
        // password is Admin123**
        "HashedPassword": "p/nF8oGYqdO0q3tfDNwyUOopYuHXTdsqFO+Wc78LtC4=", 
        "Email": "admin@email.com",
        "FullName": "Mr. Admin"
    },
    {
        "UserName": "employee1",
        // password is Admin123**
        "HashedPassword": "p/nF8oGYqdO0q3tfDNwyUOopYuHXTdsqFO+Wc78LtC4=",
        "Email": "employee1@email.com",
        "FullName": "Mr. Employee 1"
    },
    {
        "UserName": "employee2",
        // password is Admin123**
        "HashedPassword": "p/nF8oGYqdO0q3tfDNwyUOopYuHXTdsqFO+Wc78LtC4=",
        "Email": "employee1@email.com",
        "FullName": "Mr. Employee 2"
    }
]);

// add the gym members from the following data model:
// 
//         public string UserName { get; set; }
//         public string Email { get; set; }
//         public string FullName { get; set; }
//         public string PhoneNumber { get; set; }
//         public string Address { get; set; }
//         public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);
//         public DateTime DateJoined { get; set;} = DateTime.Now;

//         // an object array called membership and biometrics, default as empty array
//         public Membership[] Memberships { get; set; }

//         public BioMetric[] BioMetrics { get; set; }

//         public Bill[] Bills { get; set; } = new Bill[0];

db.gymMembers.insertMany([
    {
        "UserName": "member1",
        "Email": "member1@email.com",
        "FullName": "Mr. Member 1",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    },
    {
        "UserName": "member2",
        "Email": "member2@email.com",
        "FullName": "Mr. Member 2",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    },
    {
        "UserName": "member3",
        "Email": "member3@email.com",
        "FullName": "Mr. Member 3",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    },
    {
        "UserName": "member4",
        "Email": "member4@email.com",
        "FullName": "Mr. Member 4",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    },
    {
        "UserName": "member5",
        "Email": "member5@email.com",
        "FullName": "Mr. Member 5",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    },
    {
        "UserName": "member6",
        "Email": "member6@email.com",
        "FullName": "Mr. Member 6",
        "PhoneNumber": "123456789",
        "Address": "123 Street",
        "DateOfBirth": new Date("2000-01-01"),
        "DateJoined": new Date("2020-01-01"),
        "Memberships": [],
        "BioMetrics": [],
        "Bills": []
    }
]);
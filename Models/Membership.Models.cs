

using MongoDB.Bson;

namespace gym_management_system.Models
{

    

    public class Membership
    {
        // some constants
        public const string MembershipTypeRegular = "Regular"; // just pay for using the installation
        public const string MembershipTypePremium = "Premium"; // pay for using the installation and get a personalized meal plan and excerise routine, as well as 1 medical checkup 3 months
        public const string MembershipTypePlatinum= "Platinum"; // pay for using the installation and get a personalized meal plan and excerise routine, as well as 2 medical checkup per month, and 1 dedicated personal trainer sessions per week

        public ObjectId _id { get; set; } 
        public string MembershipType { get; set; }
        public bool MembershipStatusActive { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }


}



namespace gym_management_system.Models
{

    

    public class GymMember
    {
        // this is the document schema for mongodb driver support
        public string Id { get; set; }
        public string UserName { get; set; }
        // public string UserPasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set;}

        // an object array called membership and biometrics, default as empty array
        public Membership[] Memberships { get; set; } = new Membership[0];

        public BioMetric[] BioMetrics { get; set; } = new BioMetric[0];

        public Bill[] Bills { get; set; } = new Bill[0];

    }

    public interface MemberObjectConstants
    {
        // some constants
        public const string MembershipTypeRegular = "Regular"; // just pay for using the installation
        public const string MembershipTypePremium = "Premium"; // pay for using the installation and get a personalized meal plan and excerise routine, as well as 1 medical checkup 3 months
        public const string MembershipTypePlatinum= "Platinum"; // pay for using the installation and get a personalized meal plan and excerise routine, as well as 2 medical checkup per month, and 1 dedicated personal trainer sessions per week

        public const string BillTypeMembershipMonthly = "Monthly";
        public const string BillTypeMembershipYearly = "Yearly";


    }

    public class Membership
    {

        public string Id { get; set; } = "";
        public string MembershipType { get; set; }
        public bool MembershipStatusActive { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }

    public class BioMetric
    {
        public string Id { get; set; } = "";
        public float Weight { get; set; }
        public float Height { get; set; }
        public float BMI { get; set; }
        public float BodyFat { get; set; }

    }

    public class Bill
    {
        public string Id { get; set; } = "";
        public string BillType { get; set; }

    }

}

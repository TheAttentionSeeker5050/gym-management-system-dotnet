
using MongoDB.Bson;

namespace gym_management_system.Models
{

    public class Bill
    {
        public const string BillTypeMembershipMonthly = "Monthly";
        public const string BillTypeMembershipYearly = "Yearly";

        public ObjectId _id { get; set; } 
        public string BillType { get; set; }

    }

}
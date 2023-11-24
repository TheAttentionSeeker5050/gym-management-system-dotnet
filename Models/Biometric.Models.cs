

using MongoDB.Bson;

namespace gym_management_system.Models
{


    public class BioMetric
    {
        public ObjectId _id { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public float BMI { get; set; }
        public float BodyFat { get; set; }

    }

}

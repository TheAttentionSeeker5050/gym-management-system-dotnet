using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;


namespace gym_management_system.Models
{
    public class DBConnection
    {
        
        // thi should act as a constant
        public string MONGO_CONN_STRING => Environment.GetEnvironmentVariable("MONGODB_URI").ToString() ?? "";

        
    }
}
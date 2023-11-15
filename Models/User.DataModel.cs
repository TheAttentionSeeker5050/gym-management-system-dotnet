namespace gym_management_system.Models
{
    // this is a model class for the employee user, and this is a data class

    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        // public string Role { get; set; }
    }
}

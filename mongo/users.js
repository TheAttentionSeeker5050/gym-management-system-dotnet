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
        // password is admin
        "UserPassword": "XdnyLLNYhVx3BEF/INK+S6YfqoNf7fSx5IWPiz5bNEk=", 
        "Email": "admin@email.com",
        "FullName": "Mr. Admin"
    },
    {
        "UserName": "employee1",
        // password is admin
        "UserPassword": "XdnyLLNYhVx3BEF/INK+S6YfqoNf7fSx5IWPiz5bNEk=",
        "Email": "employee1@email.com",
        "FullName": "Mr. Employee 1"
    },
    {
        "UserName": "employee1",
        // password is admin
        "UserPassword": "XdnyLLNYhVx3BEF/INK+S6YfqoNf7fSx5IWPiz5bNEk=",
        "Email": "employee1@email.com",
        "FullName": "Mr. Employee 1"
    }
]);

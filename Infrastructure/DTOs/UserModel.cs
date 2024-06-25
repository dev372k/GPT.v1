
namespace Infrastructure.DTOs
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
    }

    public class UserList
    {
        public static List<User> users = new List<User>
        {
            new User
            {
                Name = "John Smith",
                Age = 35,
                Gender = "male"
            },
            new User
            {
                Name = "Emily Johnson",
                Age = 28,
                Gender = "female"
            },
            new User
            {
                Name = "Michael Brown",
                Age = 42,
                Gender = "male"
            },
            new User
            {
                Name = "Jessica Davis",
                Age = 31,
                Gender = "female"
            },
            new User
            {
                Name = "David Lee",
                Age = 40,
                Gender = "male"
            },
        };
    }
}

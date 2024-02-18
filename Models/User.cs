using System.Data;

namespace graduation.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}

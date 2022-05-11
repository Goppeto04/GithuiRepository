using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int ID { get; private set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string FamilyName { get; set; }

        [Required]
        [Range(18, 81)]
        public byte Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(70)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public IEnumerable<Interest> Interests { get; set; }

        private User()
        {

        }

        public User(string name, string familyName, byte age, string username, string password, string email)
        {
            Name = name;
            FamilyName = familyName;
            Age = age;
            Username = username;
            Password = password;
            Email = email;
        }
    }
}

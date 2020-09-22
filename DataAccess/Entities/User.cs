using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dyt_ecommerce.DataAccess.Entities;
using dytsenayasar.Types;

namespace dytsenayasar.DataAccess.Entities
{
    public enum GenderType { Male, Female }
    public enum UserType { User, Admin }

    public class Role
    {
        public const string USER = "User";
        public const string ADMIN = "Admin";
    }

    public class JWTUser
    {
        public const string ID = "id";
    }

    public class User : AEntity
    {
        public string PersonalId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public GenderType Gender { get; set; }
        public bool Active { get; set; }
        public UserType UserType { get; set; }
        public Guid? Image { get; set; }

        public UserClient Client { get; set; }
        public UserForm Form { get; set; }
        public ICollection<UserRequest> Requests { get; set; }
        public ICollection<UserContent> UserContents { get; set; }
    }
}
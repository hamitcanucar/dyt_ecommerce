using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dyt_ecommerce.DataAccess.Entities;
using dytsenayasar.Types;

namespace dytsenayasar.DataAccess.Entities
{
    public enum GenderType { Male, Female }
    public enum UserType { Client, Admin }

    public class Role
    {
        public const string CLIENT = "Client";
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
        public string Phone { get; set; }
        public GenderType Gender { get; set; }
        public bool Active { get; set; }
        public UserType UserType { get; set; }

        public UserClient Client { get; set; }
        public ICollection<UserRequest> Requests { get; set; }
    }
}
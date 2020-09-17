using System;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.Types;

namespace dytsenayasar.DataAccess.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
        public UserTypes? UserType { get; set; }
    }
}